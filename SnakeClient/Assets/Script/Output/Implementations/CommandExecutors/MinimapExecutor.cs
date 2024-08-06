using Assets.Protocol.Implementations.CommandExecutors;
using Assets.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class MinimapExecutor : ItemSetExecutor
{
    private MinimapController Controller;

    public override byte SignatureByte => 3;

    [Inject]
    public void Construct(MinimapController controller)
    {
        Controller = controller;
    }

    public override void ParseItem(BinaryReader stream)
    {
        var id = stream.ReadInt32();
        Controller.Pin(id);
    }
}
