using Assets.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Assets.Input
{
    internal class AbilityInputWriter : IInputWriter
    {
        [Inject]
        public void Construct(AbilityButtonController controller)
        {
            controller.Button.onClick.AddListener(() => IsButtonPressed = true);
        }

        const string ButtonName = "Jump";

        public bool IsButtonPressed { get; set; } = false;

        public void Write(BinaryWriter writer)
        {
            if (UnityEngine.Input.GetButtonDown(ButtonName) || IsButtonPressed)
            {
                writer.Write((byte)2);
                writer.Write(true);
            }
            if (UnityEngine.Input.GetButtonUp(ButtonName) || IsButtonPressed)
            {
                writer.Write((byte)2);
                writer.Write(false);
            }
            IsButtonPressed = false;
        }
    }
}
