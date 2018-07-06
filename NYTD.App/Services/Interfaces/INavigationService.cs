using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Interfaces
{
    public interface INavigationService
    {
        event EventHandler ActivateScreen;
        event EventHandler DeactivateScreen;
        void NavigateTo(Type screen);
        void Load(Type screen);
        void GoBack();
    }
}
