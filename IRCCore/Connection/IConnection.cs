using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Connection
{
    public interface IConnection : IDisposable
    {
        Task ConnectAsync(string address, int port);
        Task SendAsync(string data);

        bool IsConnected { get; }

        event EventHandler<DataReceivedEventArgs> DataReceived;
        event EventHandler Connected;
        event EventHandler Disconnected;
    }
}
