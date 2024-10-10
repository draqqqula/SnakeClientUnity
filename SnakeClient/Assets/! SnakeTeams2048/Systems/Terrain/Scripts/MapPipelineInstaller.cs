using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MapPipelineInstaller : MonoInstaller
{
    [SerializeField] private MapLoader _mapLoader;
    [SerializeField] private MapBuilder _mapBuilder;

    public override void InstallBindings()
    {
        Container.BindInstance<IMapLoader>(_mapLoader).AsSingle();
        Container.BindInstance<IMapBuilder>(_mapBuilder).AsSingle();
        Container.Bind<MapCommandHandler>().AsSingle().NonLazy();
    }
}
