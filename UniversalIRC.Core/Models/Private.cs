using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalIRC.Core.Models
{
    public class Private : Chat
    {
        /// <summary>
        /// Create new private chat instance.
        /// </summary>
        /// <param name="name">User name.</param>
        public Private(string name)
        {
            Symbol = (char)57661;
            Name = name;
        }
    }
}
