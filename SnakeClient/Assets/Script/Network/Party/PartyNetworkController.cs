using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using System;
using Zenject.ReflectionBaking.Mono.Cecil.Cil;
using System.Linq;
using Assets.Script.Network.Party;
using System.Text;
using Assets.Script.Network.Party.Models;

public class PartyNetworkController : MonoBehaviour, IPartyController
{
    #region Dialogues
    class LeaderDialogue : PartyDialogue, ILeaderDialogue
    {
        public LeaderDialogue(WebSocket ws) : base(ws)
        {
            ws.OnMessage += HandleFirstMessage;
        }

        public event Action<string> OnCodeRecieved;

        private void HandleFirstMessage(byte[] data)
        {
            OnCodeRecieved?.Invoke(Encoding.UTF8.GetString(data));
            StartHandlingStatusMessages();
            _ws.OnMessage -= HandleFirstMessage;
        }

        public override void Dispose()
        {
            base.Dispose();
            _ws.OnMessage -= HandleFirstMessage;
        }
    }

    class MemberDialogue : PartyDialogue, IMemberDialogue
    {
        private const int IdLength = 16;
        private const int DataLength = 1;
        public MemberDialogue(WebSocket ws) : base(ws)
        {
            ws.OnMessage += HandleFirstMessage;
        }

        public event Action<PartyState> OnStateRecieved;

        private void HandleFirstMessage(byte[] data)
        {
            OnStateRecieved?.Invoke(Decode(data));
            StartHandlingStatusMessages();
            _ws.OnMessage -= HandleFirstMessage;
        }

        private PartyState Decode(byte[] bytes)
        {
            var result = new PartyState()
            {
                Members = new List<MemberInfo>()
            };
            for (var i = 0; i < bytes.Length; i += (IdLength + DataLength))
            {
                var member = new PartyMember()
                {
                    Id = GuidFromData(bytes.AsSpan(i + DataLength, IdLength).ToArray())
                };
                var info = new MemberInfo()
                {
                    Member = member,
                    IsReady = Convert.ToBoolean(bytes[i])
                };
                result.Members.Add(info);
            }
            return result;
        }

        public override void Dispose()
        {
            base.Dispose();
            _ws.OnMessage -= HandleFirstMessage;
        }
    }
    #endregion

    #region Responses
    abstract class Response<T> : IRequestEventSource<T> where T : IPartyEventSource
    {
        protected readonly WebSocket _ws;
        public Response(WebSocket ws)
        {
            _ws = ws;
            _ws.OnOpen += HandleSuccess;
            _ws.OnOpen += () => _ws.OnError -= HandleFailure;
            _ws.OnError += HandleFailure;
        }

        public abstract event Action<T> OnSuccess;
        public event Action OnFailure;

        protected abstract void HandleSuccess();

        private void HandleFailure(string error)
        {
            OnFailure?.Invoke();
        }

        public void Dispose()
        {
            _ws.OnOpen -= HandleSuccess;
            _ws.OnError -= HandleFailure;
        }
    }

    class LeaderResponse : Response<ILeaderDialogue>
    {
        public LeaderResponse(WebSocket ws) : base(ws)
        {
        }

        public override event Action<ILeaderDialogue> OnSuccess;

        protected override void HandleSuccess()
        {
            OnSuccess?.Invoke(new LeaderDialogue(_ws));
        }
    }

    class MemberResponse : Response<IMemberDialogue>
    {
        public MemberResponse(WebSocket ws) : base(ws)
        {
        }

        public override event Action<IMemberDialogue> OnSuccess;

        protected override void HandleSuccess()
        {
            OnSuccess?.Invoke(new MemberDialogue(_ws));
        }
    }
    #endregion

    [field: SerializeField]
    private string Authority {  get; set; }

    [field: SerializeField]
    private string CreatePartyRoute { get; set; }

    [field: SerializeField]
    private string JoinPartyRoute { get; set; }

    public string UrlJoin(Guid myId, string code) => $"wss://{Authority}/{JoinPartyRoute}?clientId={myId}&code={code}";
    public string UrlCreate(Guid myId) => $"wss://{Authority}/{CreatePartyRoute}?clientId={myId}";

    private WebSocket Active { get; set; }

    public IRequestEventSource<ILeaderDialogue> RequestCreateParty(Guid id)
    {
        var ws = new WebSocket(UrlCreate(id));
        ws.Connect();
        Active = ws;
        return new LeaderResponse(ws);
    }

    public IRequestEventSource<IMemberDialogue> RequestJoinParty(Guid id, string code)
    {
        var ws = new WebSocket(UrlJoin(id, code));
        ws.Connect();
        Active = ws;
        return new MemberResponse(ws);
    }

    public void LeaveParty()
    {
        if (Active.State == WebSocketState.Open)
        {
            Active.CancelConnection();
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        Active?.DispatchMessageQueue();
    }
#endif
}
