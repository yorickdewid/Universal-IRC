using System;
using UniversalIRC.RelayChat.Client;
using Xunit;

namespace UniversalIRC.RelayChat.Test
{
    public class ChatManagerTest
    {
        [Fact]
        public void CanCreateChatManager()
        {
            // Act
            var client = new ChatClient();
            var chatManager = new ChatManager(client);

            // Assert
            Assert.Equal(chatManager.Client, client);
        }
    }
}
