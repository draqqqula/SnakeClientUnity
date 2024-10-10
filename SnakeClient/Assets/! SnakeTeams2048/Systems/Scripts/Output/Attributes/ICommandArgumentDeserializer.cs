using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Output.Attributes
{
    internal interface ICommandArgumentDeserializer
    {
        public object Deserialize(BinaryReader reader);
    }
}
