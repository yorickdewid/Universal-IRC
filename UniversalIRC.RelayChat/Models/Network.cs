using System;
using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat.Models
{
    public class Network : INetwork
    {
        /// <summary>
        /// Network name.
        /// </summary>
        public string Name { get; set; }

        // TODO: Should be a list
        /// <summary>
        /// Network server host.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Network sever port.
        /// </summary>
        public int Port { get; set; } = 6667;

        /// <summary>
        /// Use secure or plain connection to network.
        /// </summary>
        public bool UseSSL { get; set; } = true;

        /// <summary>
        /// Indicates if network certificate can be trusted.
        /// </summary>
        public bool AcceptInvalidCertificate { get; set; } = false;

        /// <summary>
        /// Authentication object.
        /// </summary>
        public IAuthenticate Principal { get; set; }

        /// <summary>
        /// Indicates whether authentication principal is set.
        /// </summary>
        public bool IsAnonymous { get => Principal == null; }

        public bool HasUser { get => Principal is IUser; }

        public IUser User { get => Principal as IUser; }

        /// <summary>
        /// Keep the connection alive.
        /// </summary>
        public bool KeepAlive { get; set; } = true;

        public event MessageEventHandler<NoticeMessage> Notice;

        public void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args) => Notice?.Invoke(args);

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
            Principal = user;
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
            Principal = user;
            Name = name;
        }
    }
}
