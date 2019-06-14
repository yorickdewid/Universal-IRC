using System;
using System.Threading.Tasks;
using UniversalIRC.RelayChat.Client;
using UniversalIRC.RelayChat.Protocol;
using Xunit;

namespace UniversalIRC.RelayChat.Test
{
    public class ChatClientTest
    {
        [Fact]
        public void CanCreateChatClient()
        {
            // Act
            IIRCClient client = new ChatClient();

            // Assert
            Assert.False(client.IsConnected);
            client.Dispose();
        }

        [Fact]
        public async Task CanChatClientConnect()
        {
            // Act
            var connection = new MockConnection();
            IIRCClient client = new ChatClient(connection);
            await client.ConnectAsync("localhost", 1234);

            // Assert
            Assert.Equal(client.Connection, connection);
            Assert.True(client.IsConnected);
            client.Dispose();
        }

        [Fact]
        public async Task CanChatClientSendMessage()
        {
            // Act
            IIRCClient client = new ChatClient(new MockConnection());
            await client.ConnectAsync("localhost", 1234);
            await client.SendAsync(new PrivMsgMessage("target", "message"));

            // Assert
            //Assert.True(client.IsConnected);
            client.Dispose();
        }
    }
}
