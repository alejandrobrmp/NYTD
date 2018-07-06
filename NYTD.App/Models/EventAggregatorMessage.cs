using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Models
{
    public enum EventAggregatorMessageKind
    {
        TitleChangeRequest,
    }

    public class EventAggregatorMessage<T> where T : IScreen
    {
        public EventAggregatorMessageKind Kind { get; set; }
        public object Message { get; set; }
    }
}
