using Assets.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Script.Output.Implementations.CommandExecutors
{
    internal class NotifyPowerUpExecutor : ICommandExecutor
    {
        private BonusNotification Notification { get; set; }
        [Inject]
        public void Construct(BonusNotification notification)
        {
            Notification = notification;
        }
        public bool TryExecute(BinaryReader reader)
        {
            if (reader.ReadByte() != 9)
            {
                reader.BaseStream.Position -= 1;
                return false;
            }
            var delay = reader.ReadInt32();
            var name = reader.ReadString();
            Notification.Show(TimeSpan.FromSeconds(delay));
            return true;
        }
    }
}
