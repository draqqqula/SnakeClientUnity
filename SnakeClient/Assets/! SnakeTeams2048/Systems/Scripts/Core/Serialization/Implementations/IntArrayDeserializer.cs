using Assets.Script.Core.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class IntArrayDeserializer : IBinaryDeserializer<int[]>
{
    public int[] Deserialize(BinaryReader reader)
    {
        var length = reader.ReadInt32();
        var result = new int[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = reader.ReadInt32();
        }
        return result;
    }
}