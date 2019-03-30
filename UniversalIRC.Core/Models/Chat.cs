using System;
using System.Collections.Generic;

namespace UniversalIRC.Core.Models
{
    public abstract class Chat
    {
        public string Name { get; set; }

        public string LastMessage { get => string.Empty; }

        public char Symbol { get; protected set; }

        public IEnumerable<ChatMessage> ChatHistory { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
