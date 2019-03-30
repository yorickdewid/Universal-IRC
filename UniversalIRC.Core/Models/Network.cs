using System;

namespace UniversalIRC.Core.Models
{
    public class Network : Chat
    {
        /// <summary>
        /// List of network hostnames.
        /// </summary>
        public string[] Host { get; set; }

        /// <summary>
        /// Endpoint port.
        /// </summary>
        public short Port { get; set; } = 6667;

        /// <summary>
        /// Use SSL tunneling.
        /// </summary>
        public bool Ssl { get; set; } = false;

        /// <summary>
        /// Create new network instance.
        /// </summary>
        /// <param name="name">Network name.</param>
        /// <param name="host">List of hostnames.</param>
        public Network(string name, params string[] host)
        {
            Symbol = (char)0xe968;
            Name = name;
            Host = host;
        }
    }
}
