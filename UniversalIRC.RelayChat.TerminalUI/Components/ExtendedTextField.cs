using System;
using Terminal.Gui;

namespace UniversalIRC.RelayChat.TerminalUI.Components
{
    /// <summary>
    /// Text data entry widget with exteded control.
    /// </summary>
    public class ExtendedTextField : TextField
    {
        public event EventHandler OnSubmit;

        /// <summary>
        /// Create new instance.
        /// </summary>
        public ExtendedTextField(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Create new instance.
        /// </summary>
        public ExtendedTextField(int x, int y, int w, string text)
            : base(x, y, w, text)
        {
        }

        public override bool ProcessKey(KeyEvent kb)
        {
            // Fire event on enter control.
            if (kb.Key == Key.ControlJ || kb.KeyValue == 10)
            {
                OnSubmit?.Invoke(this, EventArgs.Empty);
            }

            return base.ProcessKey(kb);
        }
    }
}
