using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Microsoft.Toolkit.Uwp.UI.Animations;

using UniversalIRC.Core.Models;
using UniversalIRC.Core.Services;
using UniversalIRC.Helpers;
using UniversalIRC.Services;
using UniversalIRC.Views;

using Windows.UI.Xaml.Controls;

namespace UniversalIRC.ViewModels
{
    public class ExtendedSplashScreenViewModel : Observable
    {
        public ExtendedSplashScreenViewModel()
        {
        }

        private void OnsItemSelected(ItemClickEventArgs args)
        {
            //NavigationService.Navigate<ExtendedSplashScreenDetailPage>(selected.ID);
        }
    }
}
