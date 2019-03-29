using System;

namespace UniversalIRC.Core.Models
{
    public class ChatMessage
    {
        public string Message { get; set; }

        public string Sender { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Sender))
            {
                return $"{Sender}: {Message}";
            }
            return $"{Message}";
        }
    }
}
