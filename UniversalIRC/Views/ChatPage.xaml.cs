using System;

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
            Loaded += MasterDetailPage_Loaded;
        }

        private async void MasterDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
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
