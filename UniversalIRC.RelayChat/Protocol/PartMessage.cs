using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.PART)]
    public class PartMessage : AbstractMessage
    {
        protected override string Parameters { get => Channel; }

        public string Channel { get; }

        public PartMessage(string channel)
        {
            Channel = channel;
        }

        public PartMessage(Message message)
        {
            Channel = message.Parameters;
        }
    }
}
