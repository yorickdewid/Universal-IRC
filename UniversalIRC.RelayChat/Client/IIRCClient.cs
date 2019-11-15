using System;
using System.Threading.Tasks;

using UniversalIRC.RelayChat.Connection;
using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat.Client
{
    /// <summary>
    /// IRC application interface.
    /// </summary>
    public interface IIRCClient : IDisposable
    {
        /// <summary>
        /// Get current connection.
        /// </summary>
        IConnection Connection { get; }

        /// <summary>
        /// Is client connected to the network.
        /// </summary>
        bool IsConnected { get; }

        event MessageEventHandler<PrivMsgMessage> OnPrivMsg;
        event MessageEventHandler<NoticeMessage> OnNotice;
        event MessageEventHandler<JoinMessage> OnJoin;
        event MessageEventHandler<PartMessage> OnPart;
        event MessageEventHandler<QuitMessage> OnQuit;
        event MessageEventHandler<QuitMessage> OnError; // TODO

        /// <summary>
        /// Connects to the specified IRC server using hostname and port.
        /// </summary>
        /// <param name="host">IRC server hostname.</param>
        /// <param name="port">IRC server port.</param>
        Task ConnectAsync(string host, int port);

        /// <summary>
        /// Connects to the specified IRC server using hostname and port.
        /// </summary>
        /// <param name="host">IRC server hostname.</param>
        /// <param name="port">IRC server port.</param>
        /// <param name="nickName">Network nickname.</param>
        /// <param name="userName">Network username.</param>
        Task ConnectAsync(string host, int port, string nickName, string userName = null);

        /// <summary>
        /// Send a message to IRC network.
        /// </summary>
        /// <param name="message">An implementation of AbstractMessage.</param>
        Task SendAsync(AbstractMessage messageObject);
    }
}
