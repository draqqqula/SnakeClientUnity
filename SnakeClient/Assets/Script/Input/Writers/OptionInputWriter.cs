using Assets.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Input.Writers
{
    public class OptionInputWriter : IInputWriter
    {
        private int? Index = null;
        public void SetOption(int index)
        {
            Index = index;
        }
        public void Write(BinaryWriter writer)
        {
            if (Index is null)
            {
                return;
            }
            writer.Write((byte)4);
            writer.Write(Index.Value);
            Index = null;
        }
    }
}
