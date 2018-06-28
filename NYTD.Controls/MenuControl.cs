using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NYTD.Controls
{
    public class MenuControl : Control
    {
        static MenuControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuControl), new FrameworkPropertyMetadata(typeof(MenuControl)));
        }
    }
}
