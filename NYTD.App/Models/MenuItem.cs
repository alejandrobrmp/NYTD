using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string ToolTip { get; set; }
        public PackIconKind Icon { get; set; }
        public Type Screen { get; set; }
        public IList<MenuItem> SubMenu { get; set; }
    }
}
