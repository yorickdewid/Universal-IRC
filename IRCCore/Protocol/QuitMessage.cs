using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    [IRCCommand(Command.QUIT)]
    public class QuitMessage : AbstractMessage
    {
        protected override string Trailing { get => quitMessage; }

        private readonly string quitMessage;

        public QuitMessage()
        {
        }

        public QuitMessage(string quitMessage)
        {
            this.quitMessage = quitMessage;
        }

        public QuitMessage(Message message)
        {
            quitMessage = message.Trailing;
        }
    }
}
