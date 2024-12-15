using Assets.Script.Commands.Interfaces;
using Assets.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

public class MapCommandHandler
{
    [Inject]
    public void DeclareCommands(
        IMapLoader mapLoader, 
        IMapBuilder mapBuilder, 
        ICommandListenerFactory factory)
    {
        factory.ListenFor<string, float>("LoadMap").OnRecieved += (name, scale) => mapLoader.Load(name).BuildWith(mapBuilder, scale);
        factory.ListenFor<int[]>("BreakTiles").OnRecieved += mapBuilder.Break;
        factory.ListenFor<int[]>("RemoveTiles").OnRecieved += mapBuilder.Break;
    }
}