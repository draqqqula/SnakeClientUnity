using Assets.Script.Core.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Core.Serialization
{
    internal class FloatDeserializer : IBinaryDeserializer<float>
    {
        public float Deserialize(BinaryReader reader)
        {
            return reader.ReadSingle();
        }
    }

    internal class IntDeserializer : IBinaryDeserializer<int>
    {
        public int Deserialize(BinaryReader reader)
        {
            return reader.ReadInt32();
        }
    }

    internal class StringDeserializer : IBinaryDeserializer<string>
    {
        public string Deserialize(BinaryReader reader)
        {
            return reader.ReadString();
        }
    }

    internal class Vector2Deserializer : IBinaryDeserializer<Vector2>
    {
        public Vector2 Deserialize(BinaryReader reader)
        {
            return new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }
    }
}
