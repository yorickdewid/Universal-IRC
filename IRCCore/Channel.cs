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
    public class Channel : IChannel
    {
        public string Name { get; }

        public event EventHandler PrivMsg;
        public event EventHandler Join;
        public event EventHandler Part;
        public event EventHandler Quit;

        public Channel(string name)
        {
            Name = name;
        }
    }
}
