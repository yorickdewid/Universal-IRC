using System;

namespace UniversalIRC.Core.Models
{
    public class Channel : Chat
    {
        /// <summary>
        /// Create new channel instance.
        /// </summary>
        /// <param name="name">Channel name.</param>
        public Channel(string name)
        {
            Symbol = (char)59158;
            Name = name;
        }
    }
}
