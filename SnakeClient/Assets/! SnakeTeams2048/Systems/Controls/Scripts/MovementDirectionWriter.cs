using Assets.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Input
{
    internal class MovementDirectionWriter : IInputWriter
    {
        public float DirectionDelta { get; set; } = 0.1309f;
        private float CurrentAngle { get; set; } = 0;

        private JoystickBehaviour JoyStick;

        [Inject]
        public void Construct(JoystickBehaviour joystick)
        {
            JoyStick = joystick;
        }

        public void Write(BinaryWriter writer)
        {
            if (Math.Abs(CurrentAngle - JoyStick.Direction) > DirectionDelta)
            {
                writer.Write((byte)3);
                writer.Write(JoyStick.Direction);
                CurrentAngle = JoyStick.Direction;
            }
        }
    }
}
