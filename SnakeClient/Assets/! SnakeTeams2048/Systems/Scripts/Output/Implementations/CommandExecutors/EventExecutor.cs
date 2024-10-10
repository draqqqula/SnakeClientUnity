using FlatBuffers;
using MessageSchemes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.State.Executors
{
    internal class EventExecutor : ICommandExecutor
    {
        private FrameDisplay Display;
        private JoystickBehaviour Joystick;

        [Inject]
        public void Construct(FrameDisplay display, JoystickBehaviour joystick)
        {
            Display = display;
            Joystick = joystick;
        }

        public bool TryExecute(BinaryReader reader)
        {
            if (reader.ReadByte() != 0)
            {
                reader.BaseStream.Position -= 1;
                return false;
            }
            var lengthBuffer = new byte[4];
            reader.Read(lengthBuffer);
            var messageLength = BitConverter.ToUInt32(lengthBuffer);
            
            var messageBuffer = new byte[messageLength];
            reader.Read(messageBuffer);
            var loader = new ByteBuffer(messageBuffer);
            var message = EventMessage.GetRootAsEventMessage(loader);
            Display.Synchronize(message);
            Joystick.UpdateGUI();
            return true;
        }
    }
}
