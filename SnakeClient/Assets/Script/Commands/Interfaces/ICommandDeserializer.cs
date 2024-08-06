using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Commands.Interfaces
{
    internal interface ICommandDeserializer
    {
        public void Read(BinaryReader reader);
    }
}
