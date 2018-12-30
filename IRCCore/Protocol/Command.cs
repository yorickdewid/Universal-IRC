using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    public enum Command
    {
        UNKNOWN,
        USER,
        NICK,
        MODE,
        QUIT,
        JOIN,
        PART,
        TOPIC,
        INVITE,
        KICK,
        PRIVMSG,
        NOTICE,
        PING,
        PONG,
        ERROR,
        VERSION,
    }
}
