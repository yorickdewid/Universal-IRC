using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;

using UniversalIRC.Core.Models;
using UniversalIRC.Core.Services;
using UniversalIRC.Helpers;

namespace UniversalIRC.ViewModels
{
    public class MasterDetailViewModel : Observable
    {
        private ChatRoom _selected;

        public ChatRoom Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<ChatRoom> ContactItems { get; private set; } = new ObservableCollection<ChatRoom>();

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            ContactItems.Clear();

            var data = await SampleDataService.GetSampleModelDataAsync();

            foreach (var item in data)
            {
                ContactItems.Add(item);
            }

            if (viewState == MasterDetailsViewState.Both)
            {
                Selected = ContactItems.First();
            }
        }
    }
}
