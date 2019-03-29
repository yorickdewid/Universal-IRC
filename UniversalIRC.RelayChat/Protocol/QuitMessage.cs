using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [IRCCommand(Command.QUIT)]
    public class QuitMessage : AbstractMessage
    {
        protected override string Trailing { get => OptionalQuitMessage; }

        public string OptionalQuitMessage { get; }

        public QuitMessage()
        {
        }

        public QuitMessage(string quitMessage)
        {
            OptionalQuitMessage = quitMessage;
        }

        public QuitMessage(Message message)
        {
            OptionalQuitMessage = message.Trailing;
        }
    }
}
