using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.JOIN)]
    public class JoinMessage : AbstractMessage
    {
        protected override string Parameters { get => Channel; }

        public string Channel { get; }

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
