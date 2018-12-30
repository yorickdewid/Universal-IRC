using System;
using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore
{
    public class MessageReceivedEventArgs<T> : EventArgs
         where T : AbstractMessage
    {
        /// <summary>
        /// Data received from the connection
        /// </summary>
        public T Message { get; }

        public MessageReceivedEventArgs(Message message)
        {
            Message = Activator.CreateInstance(typeof(T), message) as T;
        }
    }
}
