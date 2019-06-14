using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UniversalIRC.RelayChat.Connection
{
    public delegate void DataReceivedCallback(string data);

    /// <summary>
    /// Creates TCP connection via the TcpClient interface.
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

        /// <summary>
        /// Is connected to endpoint.
        /// </summary>
        public bool IsConnected { get => tcpClient.Connected; }

        public event EventHandler Connected;
        public event EventHandler Disconnected;

        /// <summary>
        /// Invoke the callback when data streams in.
        /// </summary>
        private readonly DataReceivedCallback dataReceivedCallback;

        /// <summary>
        /// Create new instance.
        /// </summary>
        public TcpClientConnection() { }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="dataReceivedCallback">Registered callback for incomming data.</param>
        public TcpClientConnection(DataReceivedCallback dataReceivedCallback)
        {
            this.dataReceivedCallback = dataReceivedCallback;
        }

        /// <summary>
        /// Connect to the remote endpoint.
        /// </summary>
        /// <param name="address">Remote address.</param>
        /// <param name="port">Remote port.</param>
        public async Task ConnectAsync(string address, int port)
        {
            await tcpClient.ConnectAsync(address, port);

            streamReader = new StreamReader(tcpClient.GetStream());
            streamWriter = new StreamWriter(tcpClient.GetStream());

            // Fire connected event
            Connected?.Invoke(this, EventArgs.Empty);

            // Launch receiving loop
            ReceiveData();
        }

        /// <summary>
        /// Connect to the remote endpoint.
        /// </summary>
        /// <param name="address">Remote address.</param>
        /// <param name="port">Remote port.</param>
        public void Connect(string address, int port)
        {
            Task.Run(() => ConnectAsync(address, port));
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
        /// Sends raw data to the remote endpoint.
        /// </summary>
        /// <param name="data">Message.</param>
        public void Send(string data)
        {
            Task.Run(() => SendAsync(data));
        }

        // TODO: HACK: The streamreader 'hangs' on the await while the flow continues
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
                    // Call reader callback
                    dataReceivedCallback?.Invoke(line);
                }
            }
            catch (Exception) { } // TODO : Prevent Disposable exceptions
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
