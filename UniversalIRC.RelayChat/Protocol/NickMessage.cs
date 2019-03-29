using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.NICK)]
    class NickMessage : AbstractMessage
    {
        protected override string Parameters { get => NickName; }

        public string NickName { get; }

        public NickMessage(string nickName)
        {
            NickName = nickName;
        }
    }
}
