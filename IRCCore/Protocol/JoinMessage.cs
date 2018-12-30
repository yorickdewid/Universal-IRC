﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    [IRCCommand(Command.JOIN)]
    class JoinMessage : AbstractMessage
    {
        protected override string Parameters { get => channel; }

        private readonly string channel;

        public JoinMessage(string channel)
        {
            this.channel = channel;
        }
    }
}
