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
        protected override string Parameters { get => user.NickName; }

        private User user;

        public NickMessage(User user)
        {
            this.user = user;
        }
    }
}
