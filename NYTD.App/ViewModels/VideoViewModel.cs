using Caliburn.Micro;
using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.ViewModels
{
    public class VideoViewModel :
        ContentViewModelBase<VideoViewModel>.Navigable<VideoViewModel>,
        IHandle<EventAggregatorMessage<IScreen>>
    {
        public override Services.Interfaces.INavigationService _navigationService { get; set; }

        public void Handle(EventAggregatorMessage<IScreen> message)
        {
            throw new NotImplementedException();
        }

        public override void Handle(EventAggregatorMessage<VideoViewModel> message)
        {
            throw new NotImplementedException();
        }
    }
}
