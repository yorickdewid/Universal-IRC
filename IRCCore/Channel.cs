using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    /// <summary>
    /// IRC channel.
    /// </summary>
    public class Channel
    {
        public string Name { get; }

        public Channel(string name)
        {
            Name = name;
        }
    }
}
