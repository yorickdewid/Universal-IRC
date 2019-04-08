using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UniversalIRC.Core.Models
{
    public class Private : ChatItem
    {
        private Collection<ChatMessage> _messageScrollback = new Collection<ChatMessage>();

        public override IEnumerable<ChatMessage> ChatHistory { get => _messageScrollback; }

        public override void ClearChatHistory() => _messageScrollback.Clear();

        public override event EventHandler<ChatMessage> OnIncommingMessage;

        /// <summary>
        /// Create new private chat instance.
        /// </summary>
        /// <param name="name">User name.</param>
        public Private(string name)
            : base(name, (char)57661)
        {
        }

        /// <summary>
        /// Create new private chat instance.
        /// </summary>
        /// <param name="name">User name.</param>
        /// <param name="chatMessages">Initialize chat with messages.</param>
        public Private(string name, IEnumerable<ChatMessage> chatMessages)
            : this(name: name)
        {
            _messageScrollback = new Collection<ChatMessage>(chatMessages.ToList());
        }

        public override void AddChatMessage(ChatMessage message)
        {
            OnIncommingMessage?.Invoke(this, message);
            _messageScrollback.Add(message);
        }
    }
}
