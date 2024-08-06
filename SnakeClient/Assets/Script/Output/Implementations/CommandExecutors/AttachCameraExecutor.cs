using ModestTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.State.Executors
{
    internal class AttachCameraExecutor : ICommandExecutor
    {
        private CameraBinder Binder;

        [Inject]
        public void Construct(CameraBinder binder)
        {
            Binder = binder;
        }
        public bool TryExecute(BinaryReader reader)
        {
            if (reader.ReadByte() != 1)
            {
                reader.BaseStream.Position -= 1;
                return false;
            }
            var buffer = new byte[4];
            reader.Read(buffer);
            var id = BitConverter.ToInt32(buffer);
            Binder.TargetId = id;
            return true;
        }
    }
}
