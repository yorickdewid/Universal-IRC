using System;

using UniversalIRC.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniversalIRC.Views
{
    public sealed partial class ExtendedSplashScreenPage : Page
    {
        public ExtendedSplashScreenViewModel ViewModel { get; } = new ExtendedSplashScreenViewModel();

        public ExtendedSplashScreenPage()
        {
            InitializeComponent();
        }
    }
}
