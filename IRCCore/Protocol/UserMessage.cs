using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    [IRCCommand(Command.USER)]
    class UserMessage : AbstractMessage
    {
        private const int mode = 0;
        private const string hostname = "*";

        protected override string Parameters { get => $"{user.UserName} {mode} {hostname}"; }
        protected override string Trailing { get => user.RealName; }

        private readonly User user;

        public UserMessage(User user)
        {
            this.user = user;
        }
    }
}
