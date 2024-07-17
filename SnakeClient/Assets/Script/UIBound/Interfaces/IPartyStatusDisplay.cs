using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.UIBound.Interfaces
{
    internal interface IPartyStatusDisplay
    {
        public void AddPlayer(Guid id);
        public void RemovePlayer(Guid id);
        public void SetNickname(Guid id, string name);
        public void SetLeader(Guid id);
        public void SetOwn(Guid id);
    }
}
