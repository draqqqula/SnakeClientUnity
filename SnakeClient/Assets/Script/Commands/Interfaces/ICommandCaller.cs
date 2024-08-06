using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Commands.Interfaces
{
    internal interface ICommandCaller
    {
        public void Declare(string name, byte commandId);
        public void Execute(byte commandId, BinaryReader reader);
    }
}
