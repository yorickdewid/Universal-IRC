using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalIRC.Core.Models
{
    public class ChatMessage
    {
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{Message}";
        }
    }
}
