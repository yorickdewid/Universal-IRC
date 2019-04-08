using System;

using UniversalIRC.Core.Helpers;
using UniversalIRC.Dialogs;
using UniversalIRC.Services;
using UniversalIRC.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalIRC.Views
{
    public sealed partial class ChatPage : Page
    {
        public ChatViewModel ViewModel { get; } = new ChatViewModel();

        public ChatPage()
        {
            InitializeComponent();
            Loaded += ChatPage_Loaded;
        }

        private async void ChatPage_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new ConnectDialog();
            dialog.ConnectClick += Dialog_ConnectClick;

            // Register a detach handler for all operations on suspension
            Singleton<SuspendAndResumeService>.Instance.OnSuspending += OnSuspending;

            await dialog.ShowAsync();
        }

        private void OnSuspending(object sender, EventArgs e)
        {
            ViewModel.DisconnectAllNetworks();
        }

        private async void Dialog_ConnectClick(ConnectDialog sender, ConnectDialogViewModel viewModel)
        {
            ChatPage_ConnectLoader.IsLoading = true;

            try
            {
                await ViewModel.ConnectNetworkAsync(viewModel.NetworkModel);
            }
            finally
            {
                ChatPage_ConnectLoader.IsLoading = false;
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MasterDetailsViewControl.DetailsTemplate = SettingsPane;
        }

        private void MasterDetailsViewControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterDetailsViewControl.DetailsTemplate = DetailsTemplate;
        }

        private async void Join_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new JoinChannelDialog();
            await dialog.ShowAsync();

            if (!string.IsNullOrEmpty(dialog.ViewModel.Channel))
            {
                await ViewModel.JoinChannelAsync(dialog.ViewModel.ChannelModel);
            }
        }
    }
}
