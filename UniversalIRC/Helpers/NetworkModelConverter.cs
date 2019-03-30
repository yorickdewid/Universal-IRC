using System;

using UniversalIRC.Core.Models;
using UniversalIRC.ViewModels;

namespace UniversalIRC.Helpers
{
    internal static class NetworkModelConverter
    {
        public static Network AsNetworkModel(ConnectDialogViewModel viewModel)
        {
            return new Network(viewModel.Host, viewModel.Host)
            {
                Ssl = viewModel.UseSsl,
                Port = viewModel.Port,
            };
        }
    }
}
