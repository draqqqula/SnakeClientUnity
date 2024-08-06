using Assets.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class AbilityCooldownExecutor : ICommandExecutor
{
    public AbilityButtonController Controller;

    [Inject]
    public void Construct(AbilityButtonController controller)
    {
        Controller = controller;
    }

    public bool TryExecute(BinaryReader reader)
    {
        if (reader.ReadByte() != 7)
        {
            reader.BaseStream.Position -= 1;
            return false;
        }
        var buffer = new byte[4];
        reader.Read(buffer, 0, 4);
        Controller.SetCooldown(BitConverter.ToSingle(buffer));
        return true;
    }
}
