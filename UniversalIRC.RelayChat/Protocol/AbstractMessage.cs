using System;
using System.Reflection;

namespace UniversalIRC.RelayChat.Protocol
{
    public abstract class AbstractMessage
    {
        protected virtual string Parameters { get; }
        protected virtual string Trailing { get; }

        public Message Message
        {
            get
            {
                var attr = GetType()
                    .GetTypeInfo()
                    .GetCustomAttribute<IRCCommandAttribute>();
                if (attr == null)
                {
                    throw new MissingAttributeException();
                }
                return new Message
                {
                    Command = attr.Command,
                    Parameters = Parameters,
                    Trailing = Trailing,
                };
            }
        }
    }
}
