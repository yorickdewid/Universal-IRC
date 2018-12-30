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
        IConnection Connection { get; }

        bool IsConnected { get; }

        Task ConnectAsync(string host, int port);
        Task ConnectAsync(string host, int port, string nickName, string userName = null);
        Task SendAsync(AbstractMessage messageObject);
    }
}
