using System;

using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat
{
    public delegate void MessageEventHandler<T>(MessageReceivedEventArgs<T> e)
        where T : AbstractMessage;

    public class MessageReceivedEventArgs<T> : EventArgs
         where T : AbstractMessage
    {
        /// <summary>
        /// Data received from the connection
        /// </summary>
        public T Message { get; }

        /// <summary>
        /// Message source.
        /// </summary>
        public Prefix Source { get; }

        public MessageReceivedEventArgs(Message message)
        {
            Message = Activator.CreateInstance(typeof(T), message) as T;
            Source = message.Prefix;
        }
    }
}
