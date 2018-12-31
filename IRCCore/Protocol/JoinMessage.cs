using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    [IRCCommand(Command.JOIN)]
    public class JoinMessage : AbstractMessage
    {
        protected override string Parameters { get => Channel; }

        private string Channel { get; }

        public JoinMessage(string channel)
        {
            Channel = channel;
        }

        public JoinMessage(Message message)
        {
            Channel = message.Parameters;
        }
    }
}
