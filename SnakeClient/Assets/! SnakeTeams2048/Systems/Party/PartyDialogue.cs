using Assets.Script.Network.Party.Models;
using NativeWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Network.Party
{
    class PartyDialogue : IPartyEventSource, IPartyOperator
    {
        protected readonly WebSocket _ws;
        public PartyDialogue(WebSocket ws)
        {
            _ws = ws;
        }

        public void StartHandlingStatusMessages()
        {
            _ws.OnMessage += HandleMessage;
            _ws.OnError += HandleError;
        }

        public bool TryToggleReady()
        {
            if (_ws.State == WebSocketState.Open)
            {
                _ws.Send(new byte[1] { 1 });
                return true;
            }
            return false;
        }

        private void HandleMessage(byte[] data)
        {
            var signature = data[0];
            var body = data.Skip(1).ToArray();

            switch (signature)
            {
                case 1:
                    OnJoined?.Invoke(MemberFromData(body));
                    break;
                case 2:
                    OnLeft?.Invoke(MemberFromData(body));
                    break;
                case 3:
                    OnLeader?.Invoke(MemberFromData(body));
                    break;
                case 4:
                    OnToggleReady?.Invoke(MemberFromData(body));
                    break;
            }
        }

        private void HandleError(string error)
        {
            OnError?.Invoke(error);
        }

        public event Action<PartyMember> OnJoined;
        public event Action<PartyMember> OnLeft;
        public event Action<PartyMember> OnLeader;
        public event Action<PartyMember> OnToggleReady;
        public event Action OnMatchMaking;
        public event Action<Guid> OnSessionFound;
        public event Action<string> OnError;

        protected PartyMember MemberFromData(byte[] bytes)
        {
            return new PartyMember()
            {
                Id = GuidFromData(bytes)
            };
        }

        protected Guid GuidFromData(byte[] bytes)
        {
            return new Guid(bytes);
        }

        public virtual void Dispose()
        {
            _ws.OnMessage -= HandleMessage;
        }
    }
}
