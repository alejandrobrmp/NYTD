using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Models
{
    public class ManageScreenRequest : EventArgs
    {
        public IScreen Screen { get; set; }
        public bool Close { get; set; }
    }
}
