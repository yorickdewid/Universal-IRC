using System;
using System.Threading.Tasks;
using UniversalIRC.IRCCore.Client;
using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore
{
    public class ChatManager : IDisposable
    {
        public IIRCClient Client { get; }

        public ChatManager(IIRCClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Connect to the IRC network.
        /// </summary>
        /// <param name="network">Network settings.</param>
        public Task ConnectAsync(Network network)
        {
            return network.IsAnonymous
                ? Client.ConnectAsync(network.Host, network.Port)
                : Client.ConnectAsync(network.Host, network.Port, network.User.NickName, (network.User as IUser).UserName);
        }

        /// <summary>
        /// Join a channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        public async Task Join(IChannel channel) => await Client.SendAsync(new JoinMessage(channel.Name));

        /// <summary>
        /// Leave a channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        public async Task Part(IChannel channel) => await Client.SendAsync(new PartMessage(channel.Name));

        /// <summary>
        /// Send message to channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        /// <param name="message">Message content.</param>
        public async Task PrivMsg(IChannel channel, string message) => await Client.SendAsync(new PrivMsgMessage(channel.Name, message));

        /// <summary>
        /// Send message to user.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <param name="message">Message content.</param>
        public async Task PrivMsg(IUser user, string message) => await Client.SendAsync(new PrivMsgMessage(user.NickName, message));

        /// <summary>
        /// Disconnect from the server.
        /// </summary>
        /// <param name="message">Quit message.</param>
        public async Task Quit(string message = null) => await Client.SendAsync(new QuitMessage(message));

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Client.Dispose();
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
