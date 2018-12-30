using System;
using System.Threading.Tasks;
using UniversalIRC.IRCCore.Client;
using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore
{
    public class ChatManager : IDisposable
    {
        private readonly IIRCClient client;

        public ChatManager(IIRCClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Connect to the IRC network.
        /// </summary>
        /// <param name="network">Network settings.</param>
        public Task ConnectAsync(Network network)
        {
            return network.IsAnonymous
                ? client.ConnectAsync(network.Host, network.Port)
                : client.ConnectAsync(network.Host, network.Port, network.User.NickName, network.User.UserName);
        }

        /// <summary>
        /// Join a channel.
        /// </summary>
        /// <param name="channel">Channel name.</param>
        public async Task Join(string channel) => await client.SendAsync(new JoinMessage(channel));

        /// <summary>
        /// Leave a channel.
        /// </summary>
        /// <param name="channel">Channel name.</param>
        public async Task Part(string channel) => await client.SendAsync(new PartMessage(channel));

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="target">Channel or user.</param>
        /// <param name="message">Message content.</param>
        public async Task PrivMsg(string target, string message) => await client.SendAsync(new PrivMsgMessage(target, message));

        /// <summary>
        /// Disconnect from the server.
        /// </summary>
        /// <param name="message">Quit message.</param>
        public async Task Quit(string message = null) => await client.SendAsync(new QuitMessage(message));

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.Dispose();
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
