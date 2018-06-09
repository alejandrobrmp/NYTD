using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Interfaces
{
    public interface IMenuService
    {
        IList<MenuItem> GetMenu();
    }
}
