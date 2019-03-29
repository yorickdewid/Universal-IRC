using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.PONG)]
    class PongMessage : AbstractMessage
    {
        protected override string Trailing { get => target; }

        private readonly string target;

        public PongMessage(string target = null)
        {
            this.target = target ?? "*";
        }
    }
}
