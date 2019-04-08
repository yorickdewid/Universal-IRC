using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using UniversalIRC.Core.Helpers;
using UniversalIRC.RelayChat;

namespace UniversalIRC.Core.Models
{
    public class Channel : ChatItem, IRelayModelProxy<IChannel>
    {
        public override IEnumerable<ChatMessage> ChatHistory { get => _messageScrollback; }

        private Collection<ChatMessage> _messageScrollback = new Collection<ChatMessage>();

        public override event EventHandler<ChatMessage> OnIncommingMessage;

        public override void ClearChatHistory() => _messageScrollback.Clear();

        public IChannel RelayModel { get; }

        /// <summary>
        /// Create new channel instance.
        /// </summary>
        /// <param name="name">Channel name.</param>
        public Channel(string name)
            : base(name, (char)59158)
        {
            RelayModel = new RelayChat.Models.Channel(name);
            RelayModel.PrivMsg += RelayModelPrivMsg;
            RelayModel.Notice += RelayModelNotice;
            RelayModel.Join += RelayModelJoin;
            RelayModel.Quit += RelayModelQuit;
        }

        /// <summary>
        /// Create new channel instance.
        /// </summary>
        /// <param name="name">Channel name.</param>
        /// <param name="chatMessages">Initialize chat with messages.</param>
        public Channel(string name, IEnumerable<ChatMessage> chatMessages)
            : this(name: name)
        {
            _messageScrollback = new Collection<ChatMessage>(chatMessages.ToList());
        }

        public override void AddChatMessage(ChatMessage message)
        {
            OnIncommingMessage?.Invoke(this, message);
            _messageScrollback.Add(message);
        }

        private void RelayModelPrivMsg(MessageReceivedEventArgs<RelayChat.Protocol.PrivMsgMessage> e)
        {
            AddChatMessage(new ChatMessage
            {
                Sender = e.Source.Name,
                Message = e.Message.TextMessage,
            });
        }

        private void RelayModelNotice(MessageReceivedEventArgs<RelayChat.Protocol.NoticeMessage> e)
        {
            AddChatMessage(new ChatMessage
            {
                Sender = e.Source.Name,
                Message = e.Message.TextMessage,
            });
        }

        private void RelayModelJoin(MessageReceivedEventArgs<RelayChat.Protocol.JoinMessage> e)
        {
            AddChatMessage(new ChatMessage
            {
                Message = $"{e.Source.Name} has joined",
            });
        }

        private void RelayModelQuit(MessageReceivedEventArgs<RelayChat.Protocol.QuitMessage> e)
        {
            var chatMessage = new ChatMessage
            {
                Message = $"{e.Source.Name} has left ",
            };

            if (!string.IsNullOrEmpty(e.Message.OptionalQuitMessage))
            {
                chatMessage.Message += $"({e.Message.OptionalQuitMessage})";
            }

            OnIncommingMessage?.Invoke(this, chatMessage);
            _messageScrollback.Add(chatMessage);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
