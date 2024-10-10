using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Core.Serialization.Interfaces
{
    internal interface IBinaryDeserializer<T>
    {
        public T Deserialize(BinaryReader reader);
    }
}
