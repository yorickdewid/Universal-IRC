using System;

using Windows.UI.Xaml.Controls;

namespace UniversalIRC.Dialogs
{
    public sealed partial class ConnectDialog : ContentDialog
    {
        public ConnectDialog()
        {
            InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
