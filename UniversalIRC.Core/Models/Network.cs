using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using UniversalIRC.Core.Helpers;
using UniversalIRC.Core.Services;
using UniversalIRC.RelayChat;

namespace UniversalIRC.Core.Models
{
    public class Network : ChatItem, IRelayModelProxy<INetwork>
    {
        private Collection<ChatMessage> _noticeScrollback = new Collection<ChatMessage>();

        public override IEnumerable<ChatMessage> ChatHistory { get => _noticeScrollback; }

        public override void ClearChatHistory() => _noticeScrollback.Clear();

        public INetwork RelayModel { get; }

        public override event EventHandler<ChatMessage> OnIncommingMessage;

        public ChatUserAccount Account { get; }

        /// <summary>
        /// Create new network instance.
        /// </summary>
        /// <param name="name">Network name.</param>
        /// <param name="host">Network server host.</param>
        /// <param name="account">Network user.</param>
        public Network(string name, string host, ChatUserAccount account)
            : base(name, (char)0xe968)
        {
            Account = account;
            RelayModel = new RelayChat.Models.Network(name: name, port: 6667, host: host, user: account);
            RelayModel.Notice += RelayModelNotice;
        }

        /// <summary>
        /// Create new network instance.
        /// </summary>
        /// <param name="name">Network name.</param>
        /// <param name="host">Network server host.</param>
        /// <param name="account">Network user.</param>
        /// <param name="chatMessages">Initialize chat with messages.</param>
        public Network(string name, string host, ChatUserAccount account, IEnumerable<ChatMessage> chatMessages)
            : this(name: name, host: host, account: account)
        {
            _noticeScrollback = new Collection<ChatMessage>(chatMessages.ToList());
        }

        public override void AddChatMessage(ChatMessage message)
        {
            OnIncommingMessage?.Invoke(this, message);
            _noticeScrollback.Add(message);
        }

        private void RelayModelNotice(MessageReceivedEventArgs<RelayChat.Protocol.NoticeMessage> e)
        {
            AddChatMessage(new ChatMessage
            {
                Sender = e.Source.Name,
                Message = e.Message.TextMessage,
            });
        }

        public override string ToString()
        {
            if ((RelayModel as RelayChat.Models.Network).IsAnonymous)
            {
                return $"{Name}:{RelayModel.Port}";
            }

            return $"{(RelayModel as RelayChat.Models.Network).Principal.NickName}@{Name}:{RelayModel.Port}";
        }
    }
}
