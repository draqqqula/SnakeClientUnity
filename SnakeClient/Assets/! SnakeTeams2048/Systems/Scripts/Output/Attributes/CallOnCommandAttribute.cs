using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Output
{
    internal class CallOnCommandAttribute : Attribute
    {
        public byte Signature { get; private set; }
        public CallOnCommandAttribute(byte signature)
        {
            Signature = signature;
        }
    }
}
