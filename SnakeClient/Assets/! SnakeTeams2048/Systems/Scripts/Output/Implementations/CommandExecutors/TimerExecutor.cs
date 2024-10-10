using Assets.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using Zenject;

public class TimerExecutor : ICommandExecutor
{
    private TimerController Controller;

    [Inject]
    public void Construct(TimerController timer)
    {
        Controller = timer;
    }

    public bool TryExecute(BinaryReader reader)
    {
        if (reader.ReadByte() != 4)
        {
            reader.BaseStream.Position -= 1;
            return false;
        }
        var buffer = new byte[8];
        reader.Read(buffer, 0, 8);
        Controller.TimerExpires = TimeSpan.FromSeconds(Time.realtimeSinceStartup + BitConverter.ToDouble(buffer));
        return true;
    }
}
