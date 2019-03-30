﻿using System;

using UniversalIRC.Core.Models;
using UniversalIRC.Dialogs;
using UniversalIRC.Helpers;
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
            Loaded += MasterDetailPage_Loaded;
        }

        private async void MasterDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new ConnectDialog();
            dialog.ConnectClick += Dialog_ConnectClick;

            // Present connect dialog to user
            await dialog.ShowAsync();

            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
        }

        private void Dialog_ConnectClick(ConnectDialog sender, ConnectDialogViewModel viewModel)
        {
            ChatPage_ConnectLoader.IsLoading = true;
            var dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2),
            };
            dispatcherTimer.Tick += (object s, object e) =>
            {
                dispatcherTimer.Stop();

                var network = NetworkModelConverter.AsNetworkModel(viewModel);
                ViewModel.ContactItems.Add(network);

                ChatPage_ConnectLoader.IsLoading = false;
            };
            dispatcherTimer.Start();
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
        }
    }
}
