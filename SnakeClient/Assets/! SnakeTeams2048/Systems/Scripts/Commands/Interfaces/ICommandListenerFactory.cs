using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Commands.Interfaces
{
    public interface ICommandListenerFactory
    {
        public ICommandListener<T> ListenFor<T>(string name);
        public ICommandListener<T1, T2> ListenFor<T1, T2>(string name);
        public ICommandListener<T1, T2, T3> ListenFor<T1, T2, T3>(string name);
        public ICommandListener<T1, T2, T3, T4> ListenFor<T1, T2, T3, T4>(string name);
        public ICommandListener<T1, T2, T3, T4, T5> ListenFor<T1, T2, T3, T4, T5>(string name);
        public ICommandListener<T1, T2, T3, T4, T5, T6> ListenFor<T1, T2, T3, T4, T5, T6>(string name);
    }
}
