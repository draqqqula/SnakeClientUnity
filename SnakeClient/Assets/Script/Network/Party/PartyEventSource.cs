using Assets.Script.Network.Party.Models;
using NativeWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Network.Party
{
    class PartyEventSource : IPartyEventSource
    {
        protected readonly WebSocket _ws;
        public PartyEventSource(WebSocket ws)
        {
            _ws = ws;
        }

        public void StartHandlingStatusMessages()
        {
            _ws.OnMessage += HandleMessage;
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
            }
        }

        public event Action<PartyMember> OnJoined;
        public event Action<PartyMember> OnLeft;
        public event Action<PartyMember> OnLeader;
        public event Action OnMatchMaking;
        public event Action<Guid> OnSessionFound;

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
