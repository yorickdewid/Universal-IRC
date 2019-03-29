using System;
using System.Collections.ObjectModel;

using UniversalIRC.Core.Models;
using UniversalIRC.ViewModels;

using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UniversalIRC.Views
{
    public sealed partial class ChatControl : UserControl
    {
        public ChatControlViewModel ViewModel { get; } = new ChatControlViewModel();

        public ChatRoom ChatRoomItem
        {
            get => GetValue(ChatRoomItemProperty) as ChatRoom;
            set => SetValue(ChatRoomItemProperty, value);
        }

        public static readonly DependencyProperty ChatRoomItemProperty = DependencyProperty.Register("ChatRoomItem", typeof(ChatRoom), typeof(ChatControl), new PropertyMetadata(null, OnChatRoomItemPropertyChanged));

        public ChatControl()
        {
            InitializeComponent();
        }

        private static void OnChatRoomItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChatControl;

            control.ViewModel.Initialize();

            control.ViewModel.MessageHistory.Clear();
            foreach (var item in control.ChatRoomItem.ChatHistory)
            {
                control.ViewModel.MessageHistory.Add(item);
            }
        }

        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(ViewModel.MessageText))
            {
                ViewModel.MessageHistory.Add(new ChatMessage
                {
                    Sender = "Me",
                    Message = ViewModel.MessageText,
                });
                ViewModel.MessageText = null;
            }
        }

        /// <summary>
        /// Trigger send message.
        /// </summary>
        private void Send_btn_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        /// <summary>
        /// Trigger send message.
        /// </summary>
        private void Message_tbx_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }
    }
}
