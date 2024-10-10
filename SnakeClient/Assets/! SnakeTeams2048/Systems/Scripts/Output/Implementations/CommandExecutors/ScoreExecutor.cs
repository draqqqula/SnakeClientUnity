using Assets.Protocol.Implementations.CommandExecutors;
using Assets.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class ScoreExecutor : ItemSetExecutor
{
    private ScoreController Controller;

    public override byte SignatureByte => 2;

    [Inject]
    public void Construct(ScoreController controller)
    {
        Controller = controller;
    }

    public override void ParseItem(BinaryReader reader)
    {
        var team = (TeamColor)reader.ReadByte();
        var score = reader.ReadInt32();
        Controller.ChangeValue(team, score);
    }
}
