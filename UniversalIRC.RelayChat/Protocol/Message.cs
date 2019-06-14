using System;

namespace UniversalIRC.RelayChat.Protocol
{
    /// <summary>
    /// An IRC message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The prefix of the message.
        /// </summary>
        public Prefix Prefix { get; set; }

        /// <summary>
        /// IRC command.
        /// </summary>
        public Command Command { get; set; } = Command.UNKNOWN;

        /// <summary>
        /// Command parameters.
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Last parameters in the message.
        /// </summary>
        public string Trailing { get; set; }

        /// <summary>
        /// IRC Numeric command.
        /// </summary>
        public NumericCommand NumericCommand { get; set; } = NumericCommand.UNKNOWN;

        /// <summary>
        /// IRC CTCP command.
        /// </summary>
        public CtcpCommand CtcpCommand { get; set; } = CtcpCommand.UNKNOWN;

        /// <summary>
        /// Indicates if the message is a valid IRC message.
        /// </summary>
        public bool IsValid
        {
            get => Command != Command.UNKNOWN ||
                NumericCommand != NumericCommand.UNKNOWN;
        }

        /// <summary>
        /// Convert message object into command string.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            string message;
            if (Command != Command.UNKNOWN)
            {
                message = Command.ToString();
            }
            else if (NumericCommand != NumericCommand.UNKNOWN)
            {
                message = NumericCommand.ToString();
            }
            else
            {
                message = "INVALID";
            }

            message += " " + Parameters;

            if (Prefix != null) // TODO:
            {
                message = Prefix.ToString() + message;
            }

            if (!string.IsNullOrWhiteSpace(Trailing))
            {
                message = $"{message} :{Trailing}";
            }

            return message;
        }

        /// <summary>
        /// Parse the string as Message object.
        /// </summary>
        /// <param name="data">Raw data.</param>
        /// <returns>Message object.</returns>
        public static Message Parse(string data)
        {
            var message = new Message();

            // Strip the prefix
            if (data.StartsWith(":"))
            {
                int indexOfNextSpace = data.IndexOf(' ');
                var prefixData = data.Substring(1, indexOfNextSpace - 1);
                message.Prefix = Prefix.Parse(prefixData);
                data = data.Substring(indexOfNextSpace + 1);
            }

            // Strip the trailing message
            var indexOfTrailingStart = data.IndexOf(" :");
            if (indexOfTrailingStart > -1)
            {
                message.Trailing = data.Substring(indexOfTrailingStart + 2);
                data = data.Substring(0, indexOfTrailingStart);
            }

            // Strip trailing if this is a CTCP command
            if (!string.IsNullOrEmpty(message.Trailing) && message.Trailing.StartsWith("\u0001") && message.Trailing.EndsWith("\u0001"))
            {
                var trailing = message.Trailing.Substring(1, message.Trailing.Length - 2);
                if (Enum.TryParse(trailing, out CtcpCommand _ctcpCommand))
                {
                    message.CtcpCommand = _ctcpCommand;
                    message.Trailing = string.Empty;
                }
            }

            // Parse the string as command
            void ParseCommand(string _data)
            {
                if (string.IsNullOrEmpty(_data))
                {
                    throw new ArgumentNullException();
                }

                if (int.TryParse(_data, out int _))
                {
                    if (Enum.TryParse(_data, out NumericCommand _numericCommand))
                    {
                        message.NumericCommand = _numericCommand;
                    }
                }
                else if (Enum.TryParse(_data, out Command _command))
                {
                    message.Command = _command;
                }
                else
                {
                    // TODO: Throw a parse exception
                    throw new Exception();
                }
            }

            // Message command
            if (!data.Contains(" "))
            {
                ParseCommand(data);
            }
            else
            {
                // Message command with parameters
                int indexOfNextSpace = data.IndexOf(' ');
                ParseCommand(data.Remove(indexOfNextSpace));
                message.Parameters = data.Substring(indexOfNextSpace + 1);
            }

            return message;
        }
    }
}
