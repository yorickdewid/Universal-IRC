using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
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
