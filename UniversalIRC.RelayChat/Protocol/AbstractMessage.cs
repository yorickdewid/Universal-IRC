using System.Reflection;

namespace UniversalIRC.RelayChat.Protocol
{
    /// <summary>
    /// IRC Command message.
    /// </summary>
    public abstract class AbstractMessage
    {
        /// <summary>
        /// Command parameters.
        /// </summary>
        protected virtual string Parameters { get; }

        /// <summary>
        /// Command trailing data.
        /// </summary>
        protected virtual string Trailing { get; }

        /// <summary>
        /// Construct an IRC <see cref="Message"/> from the command message.
        /// </summary>
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
