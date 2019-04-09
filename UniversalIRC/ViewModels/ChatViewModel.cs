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

        public ChatViewModel()
        {
            CommandParserService.SubscribeCommand("join", async (parameters) =>
            {
                if (parameters.Length < 2) { return; }
                await JoinChannelAsync(new Channel(parameters[1]));
            });
            CommandParserService.SubscribeCommand("part", async (parameters) =>
            {
                if (parameters.Length < 2) { return; }
                await PartChannelAsync(parameters[1]);
            });
        }

        /// <summary>
        /// Create a new chat service and hook the events.
        /// </summary>
        /// <returns>See <see cref="ChatService"/>.</returns>
        private ChatService InitializeChatService()
        {
            var service = new ChatService();
            service.OnRemoveChannel += ChannelRemoved;
            service.OnDisconnected += NetworkDisconnected;
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

        /// <summary>
        /// The channel was removed from the chat service and the view should
        /// reflect that.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="channel"></param>
        private void ChannelRemoved(object sender, Channel channel)
        {
            var _channel = ContactItems.FirstOrDefault(s => s.Name == channel.Name);
            if (_channel != null)
            {
                ContactItems.Remove(_channel);
            }
        }

        private void NetworkDisconnected(object sender, EventArgs e)
        {
            // TODO: Network should be known beforehand
            var _network = ContactItems.WhereAllAs<Network, ChatItem>().First();
            if (_network != null)
            {
                _network.AddChatMessage(new ChatMessage
                {
                    Message = "Network disconnected",
                });
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

            // TODO: Network should be known beforehand
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

        /// <summary>
        /// Part an network channel.
        /// </summary>
        /// <param name="channel">Channel name.</param>
        public async Task PartChannelAsync(string channelName)
        {
            var _channel = ContactItems.FirstOrDefault(s => s.Name == channelName);
            if (_channel == null) { return; }

            try
            {
                await _channel.Service.Part(_channel as Channel);
            }
            catch (Exception) { }
        }
    }
}
