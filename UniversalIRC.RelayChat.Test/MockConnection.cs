using System;
using System.Threading.Tasks;
using UniversalIRC.RelayChat.Connection;

namespace UniversalIRC.RelayChat.Test
{
    public class MockConnection : IConnection
    {
        public bool IsConnected { get; private set; } = false;

        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public Task ConnectAsync(string address, int port)
        {
            Connected?.Invoke(this, EventArgs.Empty);
            IsConnected = true;
            return Task.CompletedTask;
        }

        public void Dispose() { }

        public Task SendAsync(string data)
        {
            return Task.CompletedTask;
        }
    }
}
