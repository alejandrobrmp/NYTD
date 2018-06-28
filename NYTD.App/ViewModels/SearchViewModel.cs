using Caliburn.Micro;
using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NYTD.App.ViewModels
{
    public class SearchViewModel : ContentViewModelBase<SearchViewModel>
    {
        private string searchBox;
        public string SearchBox { get => searchBox; set => searchBox = value; }


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
    }
}
