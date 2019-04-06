using System;

namespace UniversalIRC.Core.Models
{
    public class ChatMessage
    {
        /// <summary>
        /// Message context.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Message sender.
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Message receiving time.
        /// </summary>
        public DateTime Timestamp { get; set; }

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
