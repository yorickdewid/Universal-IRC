namespace UniversalIRC.RelayChat.Protocol
{
    public class Ctcp
    {
        public CtcpCommand Command { get; set; }
        public string Message { get; set; }

        public Ctcp(CtcpCommand command)
        {
            Command = command;
        }

        public Ctcp(CtcpCommand command, string message)
        {
            Command = command;
            Message = message;
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Message)
                ? $"\u0001{Command} {Message}\u0001"
                : $"\u0001{Command}\u0001";
        }
    }
}
