using System;

namespace UniversalIRC.RelayChat.TerminalUI
{
    internal class Router : IRouter
    {
        public event EventHandler OnRoute;

        public void Push(string name)
        {
            OnRoute?.Invoke(this, EventArgs.Empty);
        }
    }
}
