using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public class Prefix
    {
        public string From { get; }
        
        /// <summary>
        /// User source.
        /// </summary>
        public string User { get; }
        
        /// <summary>
        /// Vendor host.
        /// </summary>
        public string Host { get; }

        public Prefix(string data)
        {
            From = data;

            // Process tag
            if (data.Contains("@"))
            {
                var splitedPrefix = data.Split('@');
                From = splitedPrefix[0];
                Host = splitedPrefix[1];
            }

            if (From.Contains("!"))
            {
                var splittedFrom = From.Split('!');
                From = splittedFrom[0];
                User = splittedFrom[1];
            }
        }
    }
}
