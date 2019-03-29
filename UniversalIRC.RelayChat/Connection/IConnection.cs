using System;
using System.Threading.Tasks;

namespace UniversalIRC.RelayChat.Connection
{
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Connect to the remote endpoint.
        /// </summary>
        /// <param name="address">Remote address.</param>
        /// <param name="port">Remote port.</param>
        Task ConnectAsync(string address, int port);

        /// <summary>
        /// Sends raw data to the remote endpoint.
        /// </summary>
        /// <param name="data">Message.</param>
        Task SendAsync(string data);

        /// <summary>
        /// Is connected to endpoint.
        /// </summary>
        bool IsConnected { get; }

        event EventHandler Connected;
        event EventHandler Disconnected;
    }
}
