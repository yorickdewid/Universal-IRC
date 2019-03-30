using System;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.Helpers;

using UniversalIRC.Dialogs;

namespace UniversalIRC.Services
{
    internal class FirstRunDisplayService
    {
        private static bool shown = false;

        internal static async Task ShowIfAppropriateAsync()
        {
            //if (SystemInformation.IsFirstRun && !shown)
            if (!shown)
            {
                shown = true;
                var dialog = new ConnectDialog();
                await dialog.ShowAsync();
            }
        }
    }
}
