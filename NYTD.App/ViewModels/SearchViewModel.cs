using Caliburn.Micro;
using NYTD.App.Models;
using NYTD.App.Services.Impl;
using NYTD.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace NYTD.App.ViewModels
{
    public class SearchViewModel :
        ContentViewModelBase<SearchViewModel>,
        IHandle<EventAggregatorMessage<IScreen>>
    {
        public ISearchService _searchService { get; set; }

        private string searchBox;
        public string SearchBox { get => searchBox;
            set
            {
                searchBox = value;
                NotifyOfPropertyChange();
            }
        }

        private bool isSearching;
        public bool IsSearching { get => isSearching;
            set
            {
                isSearching = value;
                NotifyOfPropertyChange();
            }
        }

        private SearchResponse AllResults = new SearchResponse();
        public ObservableCollection<object> ResultList { get; set; } = new ObservableCollection<object>();

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.PublishOnUIThread(new EventAggregatorMessage<ShellViewModel>()
            {
                Kind = EventAggregatorMessageKind.TitleChangeRequest,
                Message = "Search"
            });
        }

        public override void Handle(EventAggregatorMessage<SearchViewModel> message)
        {
            
        }

        public void Handle(EventAggregatorMessage<IScreen> message)
        {

        }

        public void SearchBoxKeyPressed(KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                Search();
            }
        }

        public void Scroll(ScrollChangedEventArgs e)
        {
            var scrollViewer = e.OriginalSource as ScrollViewer;
            if (scrollViewer != null &&
                scrollViewer.ScrollableHeight > 0 &&
                scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                if (!IsSearching)
                    LoadItems();
            }
        }

        public void Search()
        {
            _searchService.Reset();
            AllResults.Clear();
            ResultList.Clear();
            _searchService.SetSearchQuery(searchBox);
            _eventAggregator.PublishOnUIThread(new EventAggregatorMessage<ShellViewModel>()
            {
                Kind = EventAggregatorMessageKind.TitleChangeRequest,
                Message = searchBox
            });
            LoadItems();
        }

        private async void LoadItems()
        {
            IsSearching = true;
            SearchResponse response = await _searchService.GetResults();
            IsSearching = false;

            Filter(response);
        }

        public void Filter(SearchResponse response)
        {

            AllResults += response;
            // Aplicar filtro
            SearchResponse filtroAplicado = response;

            ResultList.AddAll(filtroAplicado.Channels);
            ResultList.AddAll(filtroAplicado.Playlists);
            ResultList.AddAll(filtroAplicado.Videos);
        }
    }
}
