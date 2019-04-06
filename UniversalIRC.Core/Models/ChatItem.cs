using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalIRC.Core.Models
{
    public abstract class ChatItem
    {
        /// <summary>
        /// Name of the chat item.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Chat item symbol.
        /// </summary>
        public char Symbol { get; }

        /// <summary>
        /// Subscript to name, can be last message in chat.
        /// </summary>
        public string SubMessage { get; }

        /// <summary>
        /// History of chat messages.
        /// </summary>
        public abstract IEnumerable<ChatMessage> ChatHistory { get; }

        public abstract void AddChatMessage(ChatMessage message);

        public abstract void ClearChatHistory();

        public abstract event EventHandler<ChatMessage> OnIncommingMessage;

        protected ChatItem(string name)
        {
            Name = name;
        }

        protected ChatItem(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        protected ChatItem(string name, char symbol, string subMessage)
        {
            Name = name;
            Symbol = symbol;
            SubMessage = subMessage;
        }
    }
}
