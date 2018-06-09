using NYTD.App.Models;
using NYTD.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Impl
{
    public class MenuService : IMenuService
    {
        private IList<MenuItem> menu = new List<MenuItem>();
        public IList<MenuItem> GetMenu()
        {
            return menu;
        }
    }
}
