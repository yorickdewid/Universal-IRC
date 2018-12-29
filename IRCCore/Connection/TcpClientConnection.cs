using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Connection
{
    /// <summary>
    /// Created a TCP connection via the TcpClient interface.
    /// </summary>
    public class TcpClientConnection : IConnection
    {
        private readonly TcpClient tcpClient = new TcpClient
        {
            ReceiveTimeout = 30000,
            SendTimeout = 3000,
            NoDelay = true,
        };

        private StreamReader streamReader;
        private StreamWriter streamWriter;

        public bool IsConnected { get => tcpClient.Connected; }

        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public async Task ConnectAsync(string address, int port)
        {
            await tcpClient.ConnectAsync(address, port);

            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream());

            // Fire connected event
            Connected?.Invoke(this, EventArgs.Empty);

            ReceiveData();
        }

        /// <summary>
        /// Sends raw data to the remote endpoint.
        /// </summary>
        /// <param name="data">Message.</param>
        public async Task SendAsync(string data)
        {
            await streamWriter?.WriteAsync(data);
            await streamWriter?.FlushAsync();
        }

        /// <summary>
        /// Read from the incomming datastream and fire an event when data is received.
        /// </summary>
        private async void ReceiveData()
        {
            try
            {
                string line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    // Fire data received event
                    DataReceived?.Invoke(this, new DataReceivedEventArgs(line));
                }
            }
            finally
            {
                // Fire disconnected event
                Disconnected?.Invoke(this, EventArgs.Empty);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    streamReader?.Dispose();
                    streamWriter?.Dispose();
                    tcpClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
