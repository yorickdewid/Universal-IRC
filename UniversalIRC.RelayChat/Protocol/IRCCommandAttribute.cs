using System;

namespace UniversalIRC.RelayChat.Protocol
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed internal class IRCCommandAttribute : Attribute
    {
        public Command Command { get; }

        public IRCCommandAttribute(Command command)
        {
            Command = command;
        }
    }

    class MissingAttributeException : Exception
    {
    }
}
