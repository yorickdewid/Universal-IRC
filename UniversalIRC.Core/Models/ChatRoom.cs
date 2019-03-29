using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UniversalIRC.Core.Models
{
    public class ChatRoom
    {
        public string Name { get; set; }

        public string LastMessage { get => "kaas"; }

        public char Symbol { get; set; }

        public IEnumerable<ChatMessage> ChatHistory { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
