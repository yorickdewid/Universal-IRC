using System;

namespace UniversalIRC.RelayChat.TerminalUI
{
    public interface IRouter
    {
        event EventHandler OnRoute;
        
        void Push(string name);
    }
}
