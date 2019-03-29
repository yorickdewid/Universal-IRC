using System;

using UniversalIRC.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalIRC.Views
{
    public sealed partial class MasterDetailDetailControl : UserControl
    {
        public ChatRoom MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as ChatRoom; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(ChatRoom), typeof(MasterDetailDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public MasterDetailDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MasterDetailDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
