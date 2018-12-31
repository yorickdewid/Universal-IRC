using System;
using System.Threading.Tasks;
using UniversalIRC.IRCCore.Connection;
using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore.Client
{
    public delegate void MessageEventHandler<T>(MessageReceivedEventArgs<T> e)
        where T : AbstractMessage;

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

        Task ConnectAsync(string host, int port);
        Task ConnectAsync(string host, int port, string nickName, string userName = null);
        Task SendAsync(AbstractMessage messageObject);
    }
}
