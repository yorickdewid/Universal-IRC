using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using UniversalIRC.Helpers;
using UniversalIRC.Core.Models;
using UniversalIRC.Core.Services;
using UniversalIRC.Extensions;

namespace UniversalIRC.ViewModels
{
    public class ChatViewModel : Observable
    {
        private ChatItem _selected;

        public ChatItem Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        public ObservableCollection<ChatItem> ContactItems { get; private set; } = new ObservableCollection<ChatItem>();

        public void AddChatItem(ChatItem chat)
        {
            ContactItems.Add(chat);
        }

        /// <summary>
        /// Create a new chat service and hook the events.
        /// </summary>
        /// <returns>See <see cref="ChatService"/>.</returns>
        private ChatService InitializeChatService()
        {
            var service = new ChatService();
            service.OnRemoveChannel += RemoveChannelNotification;
            return service;
        }

        /// <summary>
        /// Prepare the network object and then connect to the network.
        /// </summary>
        /// <param name="network">Network object containing connection properties.</param>
        public async Task ConnectNetworkAsync(Network network)
        {
            ContactItems.Add(network);
            Selected = network;

            try
            {
                network.ClearChatHistory();
                network.Service = InitializeChatService();
                await network.Service.ConnectAsync(network);
            }
            catch (Exception e)
            {
                network.AddChatMessage(new ChatMessage
                {
                    Message = e.Message
                });
            }
        }

        public void DisconnectAllNetworks()
        {
            foreach (var network in ContactItems.WhereAllAs<Network, ChatItem>())
            {
                network.Service.Close();
            }
        }

        private void RemoveChannelNotification(object sender, Channel channel)
        {
            var _channel = ContactItems.FirstOrDefault(s => s.Name == channel.Name);
            if (_channel != null)
            {
                ContactItems.Remove(_channel);
            }
        }

        /// <summary>
        /// Join an network channel.
        /// </summary>
        /// <param name="channel">Channel object.</param>
        public async Task JoinChannelAsync(Channel channel)
        {
            ContactItems.Add(channel);
            Selected = channel;

            var _network = ContactItems.WhereAllAs<Network, ChatItem>().First();
            if (_network != null)
            {
                try
                {
                    channel.Service = _network.Service;
                    await channel.Service.Join(channel);
                }
                catch (Exception e)
                {
                    channel.AddChatMessage(new ChatMessage
                    {
                        Message = e.Message
                    });
                }
            }
        }
    }
}
