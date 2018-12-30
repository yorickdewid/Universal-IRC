using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    [IRCCommand(Command.PART)]
    public class PartMessage : AbstractMessage
    {
        protected override string Parameters { get => channel; }

        private readonly string channel;

        public PartMessage(string channel)
        {
            this.channel = channel;
        }

        public PartMessage(Message message)
        {
            channel = message.Parameters;
        }
    }
}
