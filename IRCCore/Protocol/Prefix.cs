using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Protocol
{
    public class Prefix
    {
        /// <summary>
        /// Either servername or nickname.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// User source.
        /// </summary>
        public string User { get; }

        /// <summary>
        /// User host.
        /// </summary>
        public string Host { get; }

        public Prefix(string data)
        {
            // Process user source
            if (data.Contains("@"))
            {
                var splitedPrefix = data.Split('@');
                Name = splitedPrefix[0];
                Host = splitedPrefix[1];

                if (Name.Contains("!"))
                {
                    var splittedFrom = Name.Split('!');
                    Name = splittedFrom[0];
                    User = splittedFrom[1];
                }
            }
            // Process host source
            else
            {
                Name = data;
            }
        }

        public static Prefix Parse(string data)
        {
            return new Prefix(data);
        }
    }
}
