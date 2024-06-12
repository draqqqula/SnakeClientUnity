using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Network
{
    public interface IInputWriter
    {
        public void Write(BinaryWriter writer);
    }
}
