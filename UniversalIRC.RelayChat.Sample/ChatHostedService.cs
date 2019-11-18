using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniversalIRC.RelayChat.Client;
using UniversalIRC.RelayChat.Models;

namespace UniversalIRC.RelayChat.Sample
{
    internal class ChatHostedService : IHostedService, IDisposable
    {
        private readonly ChatManager chatManager = new ChatManager(new ChatClient());
        private readonly ILogger _logger;

        public ChatHostedService(ILogger<ChatHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.Write("Host: ");
            var networkHost = Console.ReadLine();
            Console.Write("Nickname: ");
            var nickname = Console.ReadLine();

            var network = new Network(networkHost)
            {
                Principal = new UserAccount(nickname)
            };

            network.Notice += NetworkMessage;

            chatManager.EnlistChannel += EnlistChannel;
            chatManager.RemoveChannel += RemoveChannel;

            return ConnectNetwork(network);
        }

        private void RemoveChannel(object sender, IChannel e)
        {
            _logger.LogInformation($"left channel {e.Name}");
        }

        private void EnlistChannel(object sender, IChannel e)
        {
            _logger.LogInformation($"Joined channel {e.Name}");
        }

        private Task ConnectNetwork(INetwork network)
        {
            Console.WriteLine("Connecting to network...");
            return chatManager.ConnectAsync(network);
        }

        private void NetworkMessage(MessageReceivedEventArgs<Protocol.NoticeMessage> e)
        {
            Console.WriteLine(e.Message.TextMessage);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            chatManager?.Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            chatManager?.Dispose();
        }
    }
}
