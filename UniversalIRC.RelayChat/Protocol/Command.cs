using System;

namespace UniversalIRC.RelayChat.Protocol
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
