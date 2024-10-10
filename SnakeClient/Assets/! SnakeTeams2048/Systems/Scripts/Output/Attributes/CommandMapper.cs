using ModestTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Output.Attributes
{
    internal class CommandMapper
    {
        class MappedMethod
        {
            private readonly object _target;
            private readonly MethodInfo _method;
            private readonly IEnumerable<ICommandArgumentDeserializer> _deserializers; 
            public MappedMethod(object target, MethodInfo method, IEnumerable<ICommandArgumentDeserializer> deserializers)
            {
                _target = target;
                _method = method;
                _deserializers = deserializers;
            }
            public void Invoke(BinaryReader reader)
            {
                var arguments = _deserializers.Select(it => it.Deserialize(reader));
                _method.InvokeOptimized(arguments, arguments.ToArray());
            }
        }

        private Dictionary<byte, MappedMethod> _methods;
        private Dictionary<Guid, ICommandArgumentDeserializer> _deserializers;
        public void Map(object obj)
        {
            var methods = obj.GetType().GetRuntimeMethods();
            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<CallOnCommandAttribute>();
                if (attr is not null)
                {
                    Register(obj, method, attr.Signature);
                }
            }
        }

        private void Register(object target, MethodInfo method, byte signature)
        {
            var deserializers = method
                .GetParameters()
                .Select(it => _deserializers[it.ParameterType.GUID]);
            _methods.Add(signature, new MappedMethod(target, method, deserializers));
        }

        public void Call(byte signature, BinaryReader reader)
        {
            if (_methods.TryGetValue(signature, out var method))
            {
                method.Invoke(reader);
            }
        }
    }
}
