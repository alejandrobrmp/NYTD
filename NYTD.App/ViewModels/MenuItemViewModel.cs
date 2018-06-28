using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NYTD.App.ViewModels
{
    public class MenuItemViewModel : TreeViewItem
    {
        public new object Header { get => base.Header; set => base.Header = value; }
        public PackIconKind Icon { get; set; }
        public Type Screen { get; set; }
    }
}
