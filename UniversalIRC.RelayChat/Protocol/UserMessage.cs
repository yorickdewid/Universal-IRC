using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.USER)]
    class UserMessage : AbstractMessage
    {
        private const int mode = 0;
        private const string hostname = "*";

        protected override string Parameters { get => $"{UserName} {mode} {hostname}"; }
        protected override string Trailing { get => RealName; }

        public string UserName { get; }
        public string RealName { get; }

        public UserMessage(string userName)
        {
            UserName = userName;
            RealName = userName;
        }

        public UserMessage(string userName, string realName)
        {
            UserName = userName;
            RealName = realName;
        }
    }
}
