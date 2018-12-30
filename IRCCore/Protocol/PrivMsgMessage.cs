using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
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
