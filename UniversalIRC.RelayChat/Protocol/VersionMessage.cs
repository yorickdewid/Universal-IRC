using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.VERSION)]
    class VersionMessage : AbstractMessage
    {
        protected override string Trailing { get => target; }

        private readonly string target;

        public VersionMessage(string target = null)
        {
            this.target = target;
        }
    }
}
