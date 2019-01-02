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
        protected override string Trailing { get => OptionalQuitMessage; }

        public string OptionalQuitMessage { get; }

        public QuitMessage()
        {
        }

        public QuitMessage(string quitMessage)
        {
            OptionalQuitMessage = quitMessage;
        }

        public QuitMessage(Message message)
        {
            OptionalQuitMessage = message.Trailing;
        }
    }
}
