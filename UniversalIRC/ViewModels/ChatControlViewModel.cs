using System;
using System.Collections.ObjectModel;

using UniversalIRC.Core.Models;
using UniversalIRC.Helpers;

namespace UniversalIRC.ViewModels
{
    public class ChatControlViewModel : Observable
    {
        private string _messageText;

        public string MessageText
        {
            get => _messageText;
            set => Set(ref _messageText, value);
        }

        public ObservableCollection<ChatMessage> MessageHistory { get; private set; } = new ObservableCollection<ChatMessage>();

        public void Initialize()
        {
            //
        }
    }
}
