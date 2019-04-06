using System;

using UniversalIRC.Core.Models;
using UniversalIRC.Helpers;

namespace UniversalIRC.ViewModels
{
    public class JoinChannelDialogViewModel : Observable
    {
        private string _channel;

        public string Channel
        {
            get => _channel;
            set => Set(ref _channel, value);
        }

        public Channel ChannelModel => new Channel(Channel.Trim());
    }
}
