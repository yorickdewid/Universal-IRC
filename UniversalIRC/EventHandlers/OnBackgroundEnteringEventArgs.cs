using System;
using UniversalIRC.Models;

namespace UniversalIRC.EventHandlers
{
    public class OnBackgroundEnteringEventArgs : EventArgs
    {
        public SuspensionState SuspensionState { get; set; }

        public Type Target { get; private set; }

        public OnBackgroundEnteringEventArgs(SuspensionState suspensionState, Type target)
        {
            SuspensionState = suspensionState;
            Target = target;
        }
    }
}
