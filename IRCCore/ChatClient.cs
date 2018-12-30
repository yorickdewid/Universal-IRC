using System;
using System.Threading.Tasks;
using UniversalIRC.IRCCore.Connection;
using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore
{
    public class ChatClient : IIRCClient
    {
        private const string crlf = "\r\n";

        public IConnection Connection { get; internal set; }
        public bool IsConnected { get => Connection.IsConnected; }

        public event MessageEventHandler<PrivMsgMessage> OnPrivMsg;
        public event MessageEventHandler<NoticeMessage> OnNotice;
        public event MessageEventHandler<JoinMessage> OnJoin;
        public event MessageEventHandler<PartMessage> OnPart;
        public event MessageEventHandler<QuitMessage> OnQuit;

        // TODO: Maybe not
        public ChatClient()
        {
            Connection = new TcpClientConnection(
                new DataReceivedCallback(DataReceived));
        }

        public ChatClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Connects to the specified IRC server using network server.
        /// </summary>
        /// <param name="server">IRC Network.</param>
        public async Task ConnectAsync(Network server)
        {
            //Connection.DataReceived += Connection_DataReceived;
            await Connection.ConnectAsync(server.Host, server.Port)
                .ConfigureAwait(false);

            // TODO: Do the authentication if required

            await SendAsync(new UserMessage(server.User));
            await SendAsync(new NickMessage(server.User));
        }

        /// <summary>
        /// Connects to the specified IRC server using hostname.
        /// </summary>
        /// <param name="host">IRC server hostname.</param>
        public Task ConnectAsync(string host)
        {
            return ConnectAsync(new Network(host));
        }

        /// <summary>
        /// Connects to the specified IRC server using hostname and port.
        /// </summary>
        /// <param name="host">IRC server hostname.</param>
        /// <param name="port">IRC server port.</param>
        public Task ConnectAsync(string host, int port)
        {
            return ConnectAsync(new Network(host, port));
        }

        /// <summary>
        /// Parse the received data and create an response if
        /// required or wrap the message and trigger event for external
        /// processing.
        /// </summary>
        /// <param name="data">Raw message.</param>
        private async void DataReceived(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) { return; }

            // Convert data into IRC message and dispatch the message
            var message = Message.Parse(data);
            switch (message.Command)
            {
                // Respond with pong and keep the connection active
                case Command.PING:
                    SendAsync(new PongMessage(message.Trailing));
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

                    // FUTURE: Add new commands down here
            }
        }

        /// <summary>
        /// Send the message to the IRC network.
        /// </summary>
        /// <param name="message">An implementation of AbstractMessage.</param>
        protected async Task SendAsync(AbstractMessage messageObject)
        {
            var data = messageObject.Message.ToString();
            if (!data.EndsWith(crlf))
            {
                data += crlf;
            }

            await Connection.SendAsync(data);
        }

        /// <summary>
        /// Attempt to quit the network properly.
        /// </summary>
        private void TryGracefulQuit()
        {
            if (IsConnected)
            {
                // Try quit or bail if this takes too long
                Task.WaitAny(Quit(), Task.Delay(500));
            }
        }

        /// <summary>
        /// Join a channel.
        /// </summary>
        /// <param name="channel">Channel name.</param>
        /// <returns></returns>
        public async Task Join(string channel) => await SendAsync(new JoinMessage(channel));

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="target">Channel or user.</param>
        /// <param name="message">Message content.</param>
        /// <returns></returns>
        public async Task PrivMsg(string target, string message) => await SendAsync(new PrivMsgMessage(target, message));

        /// <summary>
        /// Disconnect from the server.
        /// </summary>
        /// <param name="message">Quit message.</param>
        /// <returns></returns>
        public async Task Quit(string message = null) => await SendAsync(new QuitMessage(message));

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
