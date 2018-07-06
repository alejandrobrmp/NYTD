﻿using Caliburn.Micro;
using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.ViewModels
{
    public abstract class ContentViewModelBase<T> :
        Screen,
        IHandle<EventAggregatorMessage<T>> where T : IScreen
    {
        public IEventAggregator _eventAggregator { get; set; }

        public abstract void Handle(EventAggregatorMessage<T> message);

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
