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

    public bool TryExecute(Stream stream)
    {
        if (stream.ReadByte() != 7)
        {
            stream.Position -= 1;
            return false;
        }
        var buffer = new byte[4];
        stream.Read(buffer, 0, 4);
        Controller.SetCooldown(BitConverter.ToSingle(buffer));
        return true;
    }
}
