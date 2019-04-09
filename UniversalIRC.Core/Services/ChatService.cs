using System;
using System.Threading.Tasks;

using UniversalIRC.Core.Models;
using UniversalIRC.RelayChat;
using UniversalIRC.RelayChat.Client;

namespace UniversalIRC.Core.Services
{
    /// <summary>
    /// The chat service mediates between the relay implementation
    /// and the core models used in the depending projects.
    /// </summary>
    /// <remarks>
    /// A chat service always takes care of one network object at
    /// a time.
    /// </remarks>
    public class ChatService
    {
        private readonly ChatManager chatManager;

        public event EventHandler OnDisconnected;
        public event EventHandler<Channel> OnAddChannel;
        public event EventHandler<Channel> OnRemoveChannel;

        public Network CurrentNetwork { get; private set; }

        public ChatService()
            : this(new ChatClient())
        {
        }

        public ChatService(IIRCClient client)
        {
            chatManager = new ChatManager(client);
            RegisterHandlers();
        }

        /// <summary>
        /// Register callbacks to process incomming data.
        /// </summary>
        private void RegisterHandlers()
        {
            chatManager.Client.Connection.Connected += Connection_Connected;
            chatManager.Client.Connection.Disconnected += Connection_Disconnected;
            chatManager.EnlistChannel += ChatManager_EnlistChannel;
            chatManager.RemoveChannel += ChatManager_RemoveChannel;
        }

        private void Connection_Disconnected(object sender, EventArgs e)
        {
            OnDisconnected?.Invoke(this, EventArgs.Empty);
        }

        private void Connection_Connected(object sender, EventArgs e)
        {
            //
        }

        private void ChatManager_RemoveChannel(object sender, IChannel e)
        {
            OnRemoveChannel?.Invoke(this, new Channel(e.Name));
        }

        private void ChatManager_EnlistChannel(object sender, IChannel e)
        {
            OnAddChannel?.Invoke(this, new Channel(e.Name));
        }

        // HACK
        public void Close() => chatManager.Dispose();

        /// <summary>
        /// Connect to the provided IRC network.
        /// </summary>
        /// <param name="network">Network settings.</param>
        public Task ConnectAsync(Network network)
        {
            CurrentNetwork = network;
            return chatManager.ConnectAsync(network.RelayModel);
        }

        /// <summary>
        /// Join a channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        public Task Join(Channel channel)
        {
            return chatManager.Join(channel.RelayModel);
        }

        /// <summary>
        /// Part a channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        public Task Part(Channel channel)
        {
            return chatManager.Part(channel.RelayModel);
        }

        /// <summary>
        /// Send message to channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        /// <param name="message">Message content.</param>
        public Task PrivMsg(Channel channel, string message)
        {
            return chatManager.PrivMsg(channel.RelayModel, message);
        }
    }
}
