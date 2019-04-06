using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniversalIRC.RelayChat.Client;
using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat
{
    public class ChatManager : IDisposable
    {
        // List of channels joined by the client
        private readonly List<IChannel> channels = new List<IChannel>();
        // List of user query conversations
        //private readonly List<IUser> queryUsers = new List<IUser>();

        /// <summary>
        /// Connection client.
        /// </summary>
        public IIRCClient Client { get; }

        /// <summary>
        /// Current configured network.
        /// </summary>
        public INetwork Network { get; private set; }

        /// <summary>
        /// Active channels.
        /// </summary>
        public IEnumerable<IChannel> Channels { get => channels; }

        public event EventHandler<IChannel> EnlistChannel;
        public event EventHandler<IChannel> RemoveChannel;

        /// <summary>
        /// Create new chat manager instance.
        /// </summary>
        /// <param name="client">See <see cref="IIRCClient"/>.</param>
        public ChatManager(IIRCClient client)
        {
            Client = client;
            RegisterMessageHandlers();
        }

        /// <summary>
        /// Create new chat manager instance with network.
        /// </summary>
        /// <param name="client">See <see cref="IIRCClient"/>.</param>
        /// <param name="network">See <see cref="INetwork"/>.</param>
        public ChatManager(IIRCClient client, INetwork network)
        {
            Client = client;
            Network = network;
            RegisterMessageHandlers();
        }

        /// <summary>
        /// Register callbacks to process incomming data.
        /// </summary>
        private void RegisterMessageHandlers()
        {
            Client.OnPrivMsg += OnPrivMsg;
            Client.OnNotice += OnNotice;
            Client.OnJoin += OnJoin;
            Client.OnPart += OnPart;
            Client.OnQuit += OnQuit;
        }

        protected IChannel FindChannel(string target)
            => channels.Single(s => string.Compare(s.Name, target, true) == 0);

        protected IChannel FindChannelOrDefault(string target)
            => channels.SingleOrDefault(s => string.Compare(s.Name, target, true) == 0);

        /// <summary>
        /// Find channel/user an raise event. If no channel/user can be found the the 
        /// incomming private message methode is called.
        /// </summary>
        protected virtual void OnPrivMsg(MessageReceivedEventArgs<PrivMsgMessage> args)
        {
            var channel = FindChannelOrDefault(args.Message.NickNameOrChannel);
            if (channel != null)
            {
                channel.TriggerPrivMsg(args);
            }
            else
            {
                OnIncommingUserMessage(args);
            }
        }

        /// <summary>
        /// Find channel/user an raise event.
        /// </summary>
        protected virtual void OnNotice(MessageReceivedEventArgs<NoticeMessage> args)
        {
            var channel = FindChannelOrDefault(args.Message.NickNameOrChannel);
            if (channel != null)
            {
                channel.TriggerNotice(args);
            }
            else
            {
                Network.TriggerNotice(args);
            }
        }

        /// <summary>
        /// Find channel an raise event.
        /// </summary>
        protected virtual void OnJoin(MessageReceivedEventArgs<JoinMessage> args)
            => FindChannel(args.Message.Channel).TriggerJoin(args);

        /// <summary>
        /// Find channel an raise event.
        /// </summary>
        protected virtual void OnPart(MessageReceivedEventArgs<PartMessage> args)
            => FindChannel(args.Message.Channel).TriggerPart(args);

        /// <summary>
        /// Find channel/user an raise event.
        /// </summary>
        protected virtual void OnQuit(MessageReceivedEventArgs<QuitMessage> args)
            => channels.ForEach(c => c.TriggerQuit(args));

        /// <summary>
        /// Override to handle incomming private user messages.
        /// </summary>
        protected virtual void OnIncommingUserMessage(MessageReceivedEventArgs<PrivMsgMessage> args) { }

        /// <summary>
        /// Connect to the provided IRC network.
        /// </summary>
        /// <param name="network">Network settings.</param>
        public Task ConnectAsync(INetwork network)
        {
            Network = network;
            return !network.HasUser
                ? Client.ConnectAsync(network.Host, network.Port)
                : Client.ConnectAsync(network.Host, network.Port, network.User.NickName, network.User.UserName);
        }

        /// <summary>
        /// Connect to IRC network.
        /// </summary>
        public Task ConnectAsync()
        {
            if (Network == null) { throw new ArgumentNullException(nameof(Network)); }
            return !Network.HasUser
                ? Client.ConnectAsync(Network.Host, Network.Port)
                : Client.ConnectAsync(Network.Host, Network.Port, Network.User.NickName, Network.User.UserName);
        }

        /// <summary>
        /// Join a channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        public virtual async Task Join(IChannel channel)
        {
            await Client.SendAsync(new JoinMessage(channel.Name));
            channels.Add(channel);
            EnlistChannel?.Invoke(this, channel);
        }

        /// <summary>
        /// Leave a channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        public async Task Part(IChannel channel)
        {
            await Client.SendAsync(new PartMessage(channel.Name));
            channels.Remove(channel);
            RemoveChannel?.Invoke(this, channel);
        }

        /// <summary>
        /// Send message to channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        /// <param name="message">Message content.</param>
        public Task PrivMsg(IChannel channel, string message) => Client.SendAsync(new PrivMsgMessage(channel.Name, message));

        /// <summary>
        /// Send message to user.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <param name="message">Message content.</param>
        public Task PrivMsg(IUser user, string message) => Client.SendAsync(new PrivMsgMessage(user.NickName, message));

        /// <summary>
        /// Disconnect from the server.
        /// </summary>
        /// <param name="message">Quit message.</param>
        public Task Quit(string message = null) => Client.SendAsync(new QuitMessage(message));

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
