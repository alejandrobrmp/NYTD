using Caliburn.Micro;
using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace NYTD.App.ViewModels
{
    public class SearchViewModel :
        ContentViewModelBase<SearchViewModel>,
        IHandle<EventAggregatorMessage<IScreen>>
    {
        private string searchBox;
        public string SearchBox { get => searchBox; set => searchBox = value; }

        private bool isSearching;
        public bool IsSearching { get => isSearching;
            set
            {
                isSearching = value;
                NotifyOfPropertyChange();
            }
        }


        public SearchViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

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

        public void Search()
        {
            IsSearching = true;
            new DispatcherTimer(
                TimeSpan.FromMilliseconds(2000),
                DispatcherPriority.Normal,
                new EventHandler((o, e) =>
                {
                    IsSearching = false;
                    ((DispatcherTimer)o).Stop();
                }), Dispatcher.CurrentDispatcher);
        }
    }
}
