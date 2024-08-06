using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Network.Party.Models
{
    public class PartyState
    {
        public List<MemberInfo> Members { get; set; }
    }

    public class MemberInfo
    {
        public PartyMember Member { get; set; }
        public bool IsReady { get; set; }
    }
}
