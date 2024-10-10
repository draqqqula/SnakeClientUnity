using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Commands.Interfaces
{
    public interface ICommandListener<T>
    {
        public event Action<T> OnRecieved;
    }

    public interface ICommandListener<T1, T2>
    {
        public event Action<T1, T2> OnRecieved;
    }

    public interface ICommandListener<T1, T2, T3>
    {
        public event Action<T1, T2, T3> OnRecieved;
    }

    public interface ICommandListener<T1, T2, T3, T4>
    {
        public event Action<T1, T2, T3, T4> OnRecieved;
    }

    public interface ICommandListener<T1, T2, T3, T4, T5>
    {
        public event Action<T1, T2, T3, T4, T5> OnRecieved;
    }

    public interface ICommandListener<T1, T2, T3, T4, T5, T6>
    {
        public event Action<T1, T2, T3, T4, T5, T6> OnRecieved;
    }
}
