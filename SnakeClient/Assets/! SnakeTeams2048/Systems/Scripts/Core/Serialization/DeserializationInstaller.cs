using Assets.Script.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

public class DeserializationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<FloatDeserializer>().AsSingle();
        Container.BindInterfacesAndSelfTo<IntDeserializer>().AsSingle();
        Container.BindInterfacesAndSelfTo<StringDeserializer>().AsSingle();
        Container.BindInterfacesAndSelfTo<Vector2Deserializer>().AsSingle();
        Container.BindInterfacesAndSelfTo<IntArrayDeserializer>().AsSingle();
    }
}
