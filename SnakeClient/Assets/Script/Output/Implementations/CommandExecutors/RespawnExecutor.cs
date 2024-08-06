using Assets.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Output.Implementations.CommandExecutors
{
    public class RespawnExecutor : ICommandExecutor
    {
        private RespawnWindowDisplay Window;

        [Inject]
        public void Construct(RespawnWindowDisplay window)
        {
            Window = window;
        }
        public bool TryExecute(BinaryReader reader)
        {
            if (reader.ReadByte() != 6)
            {
                reader.BaseStream.Position -= 1;
                return false;
            }
            var buffer = new byte[4];
            reader.Read(buffer, 0, 4);
            var duration = BitConverter.ToSingle(buffer, 0);
            Window.Show();
            Window.SetTimer(duration);
            return true;
        }
    }
}
