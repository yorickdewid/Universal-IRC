using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    [IRCCommand(Command.PRIVMSG)]
    public class PrivMsgMessage : AbstractMessage
    {
        protected override string Parameters { get => Target; }
        protected override string Trailing { get => TextMessage; }

        public string Target { get; }
        public string TextMessage { get; }

        public PrivMsgMessage(string target, string message)
        {
            Target = target;
            TextMessage = message;
        }

        public PrivMsgMessage(Message message)
        {
            Target = message.Parameters;
            TextMessage = message.Trailing;
        }
    }
}
