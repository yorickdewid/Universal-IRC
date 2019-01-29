using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversalIRC.IRCCore.Client;
using UniversalIRC.IRCCore.Connection;
using UniversalIRC.IRCCore.Protocol;

namespace IRCCoreTest
{
    internal class MockConnection : IConnection
    {
        public bool IsConnected { get; private set; } = false;
        public string LastDataMessage { get; private set; }

        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public Task ConnectAsync(string address, int port)
        {
            IsConnected = true;
            if (Connected == null)
            {
                return Task.FromResult(0);
            }
            return Task.Factory.FromAsync((asyncCallback, _) =>
            {
                return Connected.BeginInvoke(this, EventArgs.Empty, asyncCallback, null);
            }, Connected.EndInvoke, null);
        }

        public Task SendAsync(string data)
        {
            LastDataMessage = data;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
        }
    }

    [TestClass]
    public class ChatClientTest
    {
        [TestMethod]
        public void CreateChatClient()
        {
            var mock = new MockConnection();
            using (IIRCClient client = new ChatClient(mock))
            {
                Assert.AreEqual(client.Connection, mock);
                Assert.IsFalse(client.IsConnected);
            }
        }

        [TestMethod]
        public async Task ConnectChatClient()
        {
            var mock = new MockConnection();
            using (IIRCClient client = new ChatClient(mock))
            {
                await client.ConnectAsync("somehost", 1234);
                Assert.IsTrue(client.IsConnected);
                await client.SendAsync(new PrivMsgMessage("channel", "message"));
                Assert.AreEqual(mock.LastDataMessage, "PRIVMSG channel :message\r\n");
            }
        }
    }
}
