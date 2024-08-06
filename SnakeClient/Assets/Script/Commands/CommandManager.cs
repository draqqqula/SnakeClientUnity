using Assets.Script.Core.Serialization;
using Assets.Script.Core.Serialization.Interfaces;
using Assets.Script.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using System.IO;

namespace Assets.Script.Commands
{
    internal class CommandManager : ICommandListenerFactory, ICommandCaller
    {
        private readonly Dictionary<byte, string> _idExchanger = new ();
        private readonly Dictionary<string, ICommandDeserializer> _listeners = new ();
        [Inject]
        public void Construct(DiContainer container)
        {
            Container = container;
            Container.InstallDeserializers();
        }
        private DiContainer Container { get; set; }

        public void Declare(string name, byte variableId)
        {
            _idExchanger.Add(variableId, name);
        }

        public void Execute(byte variableId, BinaryReader reader)
        {
            if (_idExchanger.TryGetValue(variableId, out var name))
            {
                if (_listeners.TryGetValue(name, out var listener))
                {
                    listener.Read(reader);
                }
            }
        }

        public ICommandListener<T> ListenFor<T>(string name)
        {
            var deserializer = Container.Resolve<IBinaryDeserializer<T>>();
            var listener = new CommandListener<T>(deserializer);
            _listeners.Add(name, listener);
            return listener;
        }

        public ICommandListener<T1, T2> ListenFor<T1, T2>(string name)
        {
            var deserializer1 = Container.Resolve<IBinaryDeserializer<T1>>();
            var deserializer2 = Container.Resolve<IBinaryDeserializer<T2>>();
            var listener = new CommandListener<T1, T2>(deserializer1, deserializer2);
            _listeners.Add(name, listener);
            return listener;
        }

        public ICommandListener<T1, T2, T3> ListenFor<T1, T2, T3>(string name)
        {
            var deserializer1 = Container.Resolve<IBinaryDeserializer<T1>>();
            var deserializer2 = Container.Resolve<IBinaryDeserializer<T2>>();
            var deserializer3 = Container.Resolve<IBinaryDeserializer<T3>>();
            var listener = new CommandListener<T1, T2, T3>(deserializer1, deserializer2, deserializer3);
            _listeners.Add(name, listener);
            return listener;
        }

        public ICommandListener<T1, T2, T3, T4> ListenFor<T1, T2, T3, T4>(string name)
        {
            var deserializer1 = Container.Resolve<IBinaryDeserializer<T1>>();
            var deserializer2 = Container.Resolve<IBinaryDeserializer<T2>>();
            var deserializer3 = Container.Resolve<IBinaryDeserializer<T3>>();
            var deserializer4 = Container.Resolve<IBinaryDeserializer<T4>>();
            var listener = new CommandListener<T1, T2, T3, T4>(deserializer1, deserializer2, deserializer3, deserializer4);
            _listeners.Add(name, listener);
            return listener;
        }

        public ICommandListener<T1, T2, T3, T4, T5> ListenFor<T1, T2, T3, T4, T5>(string name)
        {
            var deserializer1 = Container.Resolve<IBinaryDeserializer<T1>>();
            var deserializer2 = Container.Resolve<IBinaryDeserializer<T2>>();
            var deserializer3 = Container.Resolve<IBinaryDeserializer<T3>>();
            var deserializer4 = Container.Resolve<IBinaryDeserializer<T4>>();
            var deserializer5 = Container.Resolve<IBinaryDeserializer<T5>>();
            var listener = new CommandListener<T1, T2, T3, T4, T5>(deserializer1, deserializer2, deserializer3, deserializer4, deserializer5);
            _listeners.Add(name, listener);
            return listener;
        }

        public ICommandListener<T1, T2, T3, T4, T5, T6> ListenFor<T1, T2, T3, T4, T5, T6>(string name)
        {
            var deserializer1 = Container.Resolve<IBinaryDeserializer<T1>>();
            var deserializer2 = Container.Resolve<IBinaryDeserializer<T2>>();
            var deserializer3 = Container.Resolve<IBinaryDeserializer<T3>>();
            var deserializer4 = Container.Resolve<IBinaryDeserializer<T4>>();
            var deserializer5 = Container.Resolve<IBinaryDeserializer<T5>>();
            var deserializer6 = Container.Resolve<IBinaryDeserializer<T6>>();
            var listener = new CommandListener<T1, T2, T3, T4, T5, T6>(deserializer1, deserializer2, deserializer3, deserializer4, deserializer5, deserializer6);
            _listeners.Add(name, listener);
            return listener;
        }
    }
}
