using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
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
