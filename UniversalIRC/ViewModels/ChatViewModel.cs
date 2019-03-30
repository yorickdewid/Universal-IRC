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
    public class ChatViewModel : Observable
    {
        private Chat _selected;

        public Chat Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<Chat> ContactItems { get; private set; } = new ObservableCollection<Chat>();

        public async Task LoadDataAsync(MasterDetailsViewState viewState)
        {
            ContactItems.Clear();

            await Task.CompletedTask;

            //var data = await SampleDataService.GetSampleModelDataAsync();

            //foreach (var item in data)
            //{
            //    ContactItems.Add(item);
            //}

            //if (viewState == MasterDetailsViewState.Both)
            //{
            //    Selected = ContactItems.First();
            //}
        }
    }
}
