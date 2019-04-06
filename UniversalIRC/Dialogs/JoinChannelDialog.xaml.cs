using System;

using UniversalIRC.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalIRC.Dialogs
{
    public sealed partial class JoinChannelDialog : ContentDialog
    {
        public JoinChannelDialogViewModel ViewModel { get; } = new JoinChannelDialogViewModel();

        public JoinChannelDialog()
        {
            RequestedTheme = (Window.Current.Content as FrameworkElement).RequestedTheme;
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
