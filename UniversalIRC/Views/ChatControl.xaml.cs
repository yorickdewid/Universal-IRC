using System;
using System.Threading.Tasks;

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

        public ChatItem ChatItem
        {
            get => GetValue(ChatItemProperty) as ChatItem;
            set => SetValue(ChatItemProperty, value);
        }

        public static readonly DependencyProperty ChatItemProperty = DependencyProperty.Register("ChatItem", typeof(ChatItem), typeof(ChatControl), new PropertyMetadata(null, OnChatRoomItemPropertyChanged));

        public ChatControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Copy messages from chat item into the view model.
        /// </summary>
        private void InitializeChatControl(ChatItem oldItem, ChatItem newItem)
        {
            if (oldItem != null)
            {
                oldItem.OnIncommingMessage -= OnIncommingMessage;
            }

            ViewModel.MessageHistory.Clear();
            foreach (var item in newItem.ChatHistory)
            {
                ViewModel.MessageHistory.Add(item);
            }

            // Subscribe to new incomming messages
            newItem.OnIncommingMessage += OnIncommingMessage;
        }

        private static void OnChatRoomItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChatControl;

            control.InitializeChatControl(e.OldValue as ChatItem, e.NewValue as ChatItem);
        }

        private void OnIncommingMessage(object s, ChatMessage message)
        {
            ViewModel.MessageHistory.Add(message);
        }

        // TODO: Move into VM
        private async Task SendMessage()
        {
            if (!string.IsNullOrEmpty(ViewModel.MessageText))
            {
                var message = ViewModel.MessageText.Trim();

                // TODO: Send message
                await ChatItem.Service.PrivMsg(ChatItem as Channel, message);

                ChatItem.AddChatMessage(new ChatMessage
                {
                    Sender = ChatItem.Service.CurrentNetwork.Account.NickName,
                    Message = message,
                });

                ViewModel.MessageText = null;
            }
        }

        /// <summary>
        /// Trigger send message.
        /// </summary>
        private async void Send_btn_Click(object sender, RoutedEventArgs e)
        {
            await SendMessage();
        }

        /// <summary>
        /// Trigger send message.
        /// </summary>
        private async void Message_tbx_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await SendMessage();
                e.Handled = true;
            }
        }
    }
}
