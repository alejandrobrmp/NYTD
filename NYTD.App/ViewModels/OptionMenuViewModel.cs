using Caliburn.Micro;
using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.ViewModels
{
    public class OptionMenuViewModel : List<OptionViewModel>
    {
        public void CanExecuteChanged()
        {
            ForEach(e => e.Refresh());
        }
    }
}
