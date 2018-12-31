using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public class Network
    {
        public string Name { get; set; }
        public string Host { get; }
        public int Port { get; set; } = 6667;
        public bool UseSSL { get; set; } = true;
        public bool AcceptInvalidCertificate { get; set; } = false;
        public IAuthenticate User { get; set; }
        public bool IsAnonymous { get => User == null; }
        public bool KeepAlive { get; set; } = true;

        /// <summary>
        /// Create a remote network instance.
        /// </summary>
        /// <param name="host">Server hostname.</param>
        public Network(string host)
        {
            Host = host;
        }

        /// <summary>
        /// Create a remote network instance.
        /// </summary>
        /// <param name="host">Server hostname.</param>
        /// <param name="port">Server port.</param>
        public Network(string host, int port)
        {
            Host = host;
            Port = port;
        }

        /// <summary>
        /// Create a remote network instance.
        /// </summary>
        /// <param name="host">Server hostname.</param>
        /// <param name="port">Server port.</param>
        /// <param name="name">Network name.</param>
        public Network(string host, int port, string name)
        {
            Host = host;
            Port = port;
            Name = name;
        }

        /// <summary>
        /// Create a remote network instance.
        /// </summary>
        /// <param name="host">Server hostname.</param>
        /// <param name="port">Server port.</param>
        /// <param name="user">Authentication object.</param>
        public Network(string host, int port, IAuthenticate user)
        {
            Host = host;
            Port = port;
            User = user;
        }

        /// <summary>
        /// Create a remote network instance.
        /// </summary>
        /// <param name="host">Server hostname.</param>
        /// <param name="port">Server port.</param>
        /// <param name="user">Authentication object.</param>
        /// <param name="name">Network name.</param>
        public Network(string host, int port, IAuthenticate user, string name)
        {
            Host = host;
            Port = port;
            User = user;
            Name = name;
        }
    }
}
