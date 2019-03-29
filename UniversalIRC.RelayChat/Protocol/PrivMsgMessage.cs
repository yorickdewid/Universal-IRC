using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.PRIVMSG)]
    public class PrivMsgMessage : NoticeMessage
    {
        public PrivMsgMessage(string target, string message)
            : base(target, message)
        {
        }

        public PrivMsgMessage(Message message)
            : base(message)
        {
        }
    }
}
