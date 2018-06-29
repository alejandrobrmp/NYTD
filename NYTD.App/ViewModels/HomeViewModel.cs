using Caliburn.Micro;
using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.ViewModels
{
    public class HomeViewModel :
        ContentViewModelBase<HomeViewModel>,
        IHandle<EventAggregatorMessage<IScreen>>
    {
        public HomeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        public void Handle(EventAggregatorMessage<IScreen> message)
        {
            throw new NotImplementedException();
        }

        public override void Handle(EventAggregatorMessage<HomeViewModel> message)
        {
            throw new NotImplementedException();
        }
    }
}
