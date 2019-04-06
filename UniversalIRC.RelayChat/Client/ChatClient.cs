using System.Threading.Tasks;

using UniversalIRC.RelayChat.Connection;
using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat.Client
{
    public class ChatClient : IIRCClient
    {
        private const string crlf = "\r\n";

        /// <summary>
        /// Get current connection.
        /// </summary>
        public IConnection Connection { get; internal set; }

        /// <summary>
        /// Is client connected to the network.
        /// </summary>
        public bool IsConnected { get => Connection.IsConnected; }

        public event MessageEventHandler<PrivMsgMessage> OnPrivMsg;
        public event MessageEventHandler<NoticeMessage> OnNotice;
        public event MessageEventHandler<JoinMessage> OnJoin;
        public event MessageEventHandler<PartMessage> OnPart;
        public event MessageEventHandler<QuitMessage> OnQuit;

        /// <summary>
        /// Create a chat client object.
        /// </summary>
        public ChatClient()
        {
            Connection = new TcpClientConnection(new DataReceivedCallback(DataReceived));
        }

        /// <summary>
        /// Create a chat client object with provided connection.
        /// </summary>
        /// <param name="connection">Network connection.</param>
        public ChatClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Connects to the specified IRC server using hostname and port.
        /// </summary>
        /// <param name="host">IRC server hostname.</param>
        /// <param name="port">IRC server port.</param>
        public async Task ConnectAsync(string host, int port)
        {
            await Connection.ConnectAsync(host, port);

            // TODO: Do the authentication if required
        }

        /// <summary>
        /// Connects to the specified IRC server using hostname and port.
        /// </summary>
        /// <param name="host">IRC server hostname.</param>
        /// <param name="port">IRC server port.</param>
        /// <param name="nickName">Network nickname.</param>
        /// <param name="userName">Network username.</param>
        public async Task ConnectAsync(string host, int port, string nickName, string userName)
        {
            await ConnectAsync(host, port);

            userName = userName ?? nickName;

            await SendAsync(new UserMessage(userName));
            await SendAsync(new NickMessage(nickName));
        }

        /// <summary>
        /// Parse the received data and create an response if
        /// required or wrap the message and trigger event for external
        /// processing.
        /// </summary>
        /// <param name="data">Raw message.</param>
        private void DataReceived(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) { return; }

            // Convert data into IRC message and dispatch the message
            var message = Message.Parse(data);
            switch (message.Command)
            {
                // Respond with pong and keep the connection active
                case Command.PING:
                    SendAsync(new PongMessage(message.Trailing)).Wait();
                    break;
                // Ignore pong
                case Command.PONG:
                    break;
                // Fire incomming private message
                case Command.PRIVMSG:
                    OnPrivMsg?.Invoke(new MessageReceivedEventArgs<PrivMsgMessage>(message));
                    break;
                // Fire incomming notice message
                case Command.NOTICE:
                    OnNotice?.Invoke(new MessageReceivedEventArgs<NoticeMessage>(message));
                    break;
                // Fire join event
                case Command.JOIN:
                    OnJoin?.Invoke(new MessageReceivedEventArgs<JoinMessage>(message));
                    break;
                // Fire part event
                case Command.PART:
                    OnPart?.Invoke(new MessageReceivedEventArgs<PartMessage>(message));
                    break;
                // Fire quit event
                case Command.QUIT:
                    OnQuit?.Invoke(new MessageReceivedEventArgs<QuitMessage>(message));
                    break;
                case Command.NICK:
                    break;
                case Command.MODE:
                    break;

                    // FUTURE: Add new commands down here
            }
        }

        /// <summary>
        /// Send a message to IRC network.
        /// </summary>
        /// <param name="message">An implementation of AbstractMessage.</param>
        public Task SendAsync(AbstractMessage messageObject)
        {
            var data = messageObject.Message.ToString();
            if (!data.EndsWith(crlf))
            {
                data += crlf;
            }

            return Connection.SendAsync(data);
        }

        /// <summary>
        /// Attempt to quit the network properly.
        /// </summary>
        private void TryGracefulQuit()
        {
            if (IsConnected)
            {
                // Try quit or bail if this takes too long
                Task.WaitAny(SendAsync(new QuitMessage()), Task.Delay(500));
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
                    TryGracefulQuit();
                    Connection.Dispose();
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
