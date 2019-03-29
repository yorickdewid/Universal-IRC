using System;
using System.Collections.ObjectModel;

using UniversalIRC.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalIRC.Views
{
    public sealed partial class ChatControl : UserControl
    {
        public ChatRoom ChatRoomItem
        {
            get { return GetValue(ChatRoomItemProperty) as ChatRoom; }
            set { SetValue(ChatRoomItemProperty, value); }
        }

        public static readonly DependencyProperty ChatRoomItemProperty = DependencyProperty.Register("ChatRoomItem", typeof(ChatRoom), typeof(ChatControl), new PropertyMetadata(null, OnChatRoomItemPropertyChanged));

        public ObservableCollection<ChatMessage> MessageHistory { get; private set; } = new ObservableCollection<ChatMessage>();

        public ChatControl()
        {
            InitializeComponent();
        }

        private static void OnChatRoomItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ChatControl;

            control.MessageHistory.Clear();
            foreach (var item in control.ChatRoomItem.ChatHistory)
            {
                control.MessageHistory.Add(item);
            }
        }
    }
}
