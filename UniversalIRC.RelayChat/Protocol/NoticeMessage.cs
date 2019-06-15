using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.NOTICE)]
    public class NoticeMessage : AbstractMessage
    {
        protected override string Parameters { get => NickNameOrChannel; }
        protected override string Trailing { get => TextMessage; }

        public string NickNameOrChannel { get; }
        public string TextMessage { get; }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="target">Message recipient.</param>
        /// <param name="message">Notice message.</param>
        public NoticeMessage(string target, string message)
        {
            NickNameOrChannel = target;
            TextMessage = message;
        }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="message">Notice message.</param>
        public NoticeMessage(Message message)
        {
            NickNameOrChannel = message.Parameters;
            TextMessage = message.Trailing;
        }
    }
}
