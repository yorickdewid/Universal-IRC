using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    [IRCCommand(Command.NOTICE)]
    public class NoticeMessage : AbstractMessage
    {
        protected override string Parameters { get => Target; }
        protected override string Trailing { get => TextMessage; }

        public string Target { get; }
        public string TextMessage { get; }

        public NoticeMessage(string target, string message)
        {
            Target = target;
            TextMessage = message;
        }

        public NoticeMessage(Message message)
        {
            Target = message.Parameters;
            TextMessage = message.Trailing;
        }
    }
}
