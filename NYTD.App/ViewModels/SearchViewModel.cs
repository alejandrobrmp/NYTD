using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.ViewModels
{
    public class SearchViewModel : Screen, IHandle<object>
    {

        private readonly IEventAggregator _eventAggregator;

        public SearchViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void Handle(object message)
        {
            
        }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
    }
}
