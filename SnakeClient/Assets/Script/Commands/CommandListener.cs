using Assets.Script.Core.Serialization.Interfaces;
using Assets.Script.Commands.Interfaces;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Script.Commands
{
    internal class CommandListener<T> : ICommandListener<T>, ICommandDeserializer
    {
        public event Action<T> OnRecieved;

        private readonly IBinaryDeserializer<T> _binaryDeserializer;
        public CommandListener(IBinaryDeserializer<T> deserializer)
        {
            _binaryDeserializer = deserializer;
        }

        public void Read(BinaryReader reader)
        {
            var value = _binaryDeserializer.Deserialize(reader);
            OnRecieved?.Invoke(value);
        }
    }

    internal class CommandListener<T1, T2> : ICommandListener<T1, T2>, ICommandDeserializer
    {
        public event Action<T1, T2> OnRecieved;

        private readonly IBinaryDeserializer<T1> _binaryDeserializer1;
        private readonly IBinaryDeserializer<T2> _binaryDeserializer2;
        public CommandListener(IBinaryDeserializer<T1> deserializer1, IBinaryDeserializer<T2> deserializer2)
        {
            _binaryDeserializer1 = deserializer1;
            _binaryDeserializer2 = deserializer2;
        }

        public void Read(BinaryReader reader)
        {
            var value1 = _binaryDeserializer1.Deserialize(reader);
            var value2 = _binaryDeserializer2.Deserialize(reader);
            OnRecieved?.Invoke(value1, value2);
        }
    }

    internal class CommandListener<T1, T2, T3> : ICommandListener<T1, T2, T3>, ICommandDeserializer
    {
        public event Action<T1, T2, T3> OnRecieved;

        private readonly IBinaryDeserializer<T1> _binaryDeserializer1;
        private readonly IBinaryDeserializer<T2> _binaryDeserializer2;
        private readonly IBinaryDeserializer<T3> _binaryDeserializer3;
        public CommandListener(IBinaryDeserializer<T1> deserializer1, IBinaryDeserializer<T2> deserializer2, 
            IBinaryDeserializer<T3> deserializer3)
        {
            _binaryDeserializer1 = deserializer1;
            _binaryDeserializer2 = deserializer2;
            _binaryDeserializer3 = deserializer3;
        }

        public void Read(BinaryReader reader)
        {
            var value1 = _binaryDeserializer1.Deserialize(reader);
            var value2 = _binaryDeserializer2.Deserialize(reader);
            var value3 = _binaryDeserializer3.Deserialize(reader);
            OnRecieved?.Invoke(value1, value2, value3);
        }
    }

    internal class CommandListener<T1, T2, T3, T4> : ICommandListener<T1, T2, T3, T4>, ICommandDeserializer
    {
        public event Action<T1, T2, T3, T4> OnRecieved;

        private readonly IBinaryDeserializer<T1> _binaryDeserializer1;
        private readonly IBinaryDeserializer<T2> _binaryDeserializer2;
        private readonly IBinaryDeserializer<T3> _binaryDeserializer3;
        private readonly IBinaryDeserializer<T4> _binaryDeserializer4;
        public CommandListener(IBinaryDeserializer<T1> deserializer1, IBinaryDeserializer<T2> deserializer2,
            IBinaryDeserializer<T3> deserializer3, IBinaryDeserializer<T4> deserializer4)
        {
            _binaryDeserializer1 = deserializer1;
            _binaryDeserializer2 = deserializer2;
            _binaryDeserializer3 = deserializer3;
            _binaryDeserializer4 = deserializer4;
        }

        public void Read(BinaryReader reader)
        {
            var value1 = _binaryDeserializer1.Deserialize(reader);
            var value2 = _binaryDeserializer2.Deserialize(reader);
            var value3 = _binaryDeserializer3.Deserialize(reader);
            var value4 = _binaryDeserializer4.Deserialize(reader);
            OnRecieved?.Invoke(value1, value2, value3, value4);
        }
    }

    internal class CommandListener<T1, T2, T3, T4, T5> : ICommandListener<T1, T2, T3, T4, T5>, ICommandDeserializer
    {
        public event Action<T1, T2, T3, T4, T5> OnRecieved;

        private readonly IBinaryDeserializer<T1> _binaryDeserializer1;
        private readonly IBinaryDeserializer<T2> _binaryDeserializer2;
        private readonly IBinaryDeserializer<T3> _binaryDeserializer3;
        private readonly IBinaryDeserializer<T4> _binaryDeserializer4;
        private readonly IBinaryDeserializer<T5> _binaryDeserializer5;
        public CommandListener(IBinaryDeserializer<T1> deserializer1, IBinaryDeserializer<T2> deserializer2,
            IBinaryDeserializer<T3> deserializer3, IBinaryDeserializer<T4> deserializer4, IBinaryDeserializer<T5> deserializer5)
        {
            _binaryDeserializer1 = deserializer1;
            _binaryDeserializer2 = deserializer2;
            _binaryDeserializer3 = deserializer3;
            _binaryDeserializer4 = deserializer4;
            _binaryDeserializer5 = deserializer5;
        }

        public void Read(BinaryReader reader)
        {
            var value1 = _binaryDeserializer1.Deserialize(reader);
            var value2 = _binaryDeserializer2.Deserialize(reader);
            var value3 = _binaryDeserializer3.Deserialize(reader);
            var value4 = _binaryDeserializer4.Deserialize(reader);
            var value5 = _binaryDeserializer5.Deserialize(reader);
            OnRecieved?.Invoke(value1, value2, value3, value4, value5);
        }
    }

    internal class CommandListener<T1, T2, T3, T4, T5, T6> : ICommandListener<T1, T2, T3, T4, T5, T6>, ICommandDeserializer
    {
        public event Action<T1, T2, T3, T4, T5, T6> OnRecieved;

        private readonly IBinaryDeserializer<T1> _binaryDeserializer1;
        private readonly IBinaryDeserializer<T2> _binaryDeserializer2;
        private readonly IBinaryDeserializer<T3> _binaryDeserializer3;
        private readonly IBinaryDeserializer<T4> _binaryDeserializer4;
        private readonly IBinaryDeserializer<T5> _binaryDeserializer5;
        private readonly IBinaryDeserializer<T6> _binaryDeserializer6;
        public CommandListener(IBinaryDeserializer<T1> deserializer1, IBinaryDeserializer<T2> deserializer2,
            IBinaryDeserializer<T3> deserializer3, IBinaryDeserializer<T4> deserializer4, IBinaryDeserializer<T5> deserializer5, 
            IBinaryDeserializer<T6> deserializer6)
        {
            _binaryDeserializer1 = deserializer1;
            _binaryDeserializer2 = deserializer2;
            _binaryDeserializer3 = deserializer3;
            _binaryDeserializer4 = deserializer4;
            _binaryDeserializer5 = deserializer5;
            _binaryDeserializer6 = deserializer6;
        }

        public void Read(BinaryReader reader)
        {
            var value1 = _binaryDeserializer1.Deserialize(reader);
            var value2 = _binaryDeserializer2.Deserialize(reader);
            var value3 = _binaryDeserializer3.Deserialize(reader);
            var value4 = _binaryDeserializer4.Deserialize(reader);
            var value5 = _binaryDeserializer5.Deserialize(reader);
            var value6 = _binaryDeserializer6.Deserialize(reader);
            OnRecieved?.Invoke(value1, value2, value3, value4, value5, value6);
        }
    }
}
