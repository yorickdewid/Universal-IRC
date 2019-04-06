using System;

using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat
{
    public delegate void MessageEventHandler<TMessage>(MessageReceivedEventArgs<TMessage> e)
        where TMessage : AbstractMessage;

    public class MessageReceivedEventArgs<TMessage> : EventArgs
         where TMessage : AbstractMessage
    {
        /// <summary>
        /// Data received from the connection
        /// </summary>
        public TMessage Message { get; }

        /// <summary>
        /// Message source.
        /// </summary>
        public Prefix Source { get; }

        public MessageReceivedEventArgs(Message message)
        {
            Message = Activator.CreateInstance(typeof(TMessage), message) as TMessage;
            Source = message.Prefix;
        }
    }
}
