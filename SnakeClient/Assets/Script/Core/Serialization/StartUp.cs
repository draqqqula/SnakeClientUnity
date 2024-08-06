using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Script.Core.Serialization
{
    internal static class StartUp
    {
        public static void InstallDeserializers(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<FloatDeserializer>().AsSingle();
            container.BindInterfacesAndSelfTo<IntDeserializer>().AsSingle();
            container.BindInterfacesAndSelfTo<StringDeserializer>().AsSingle();
            container.BindInterfacesAndSelfTo<Vector2Deserializer>().AsSingle();
        }
    }
}
