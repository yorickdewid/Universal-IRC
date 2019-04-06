using System;

using UniversalIRC.ViewModels;

using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalIRC.Dialogs
{
    public sealed partial class ConnectDialog : ContentDialog
    {
        public event TypedEventHandler<ConnectDialog, ConnectDialogViewModel> ConnectClick;

        public ConnectDialogViewModel ViewModel { get; } = new ConnectDialogViewModel();

        public ConnectDialog()
        {
            RequestedTheme = (Window.Current.Content as FrameworkElement).RequestedTheme;
            InitializeComponent();
        }

        private void ContentDialog_ConnectClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ConnectClick?.Invoke(this, ViewModel);
        }
    }
}
