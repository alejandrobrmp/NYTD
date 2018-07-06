using Caliburn.Micro;
using NYTD.App.Models;
using NYTD.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Impl
{
    public class NavigationService : Interfaces.INavigationService
    {
        public event EventHandler ActivateScreen;
        public event EventHandler DeactivateScreen;

        private IScreen activeItem;
        private Queue<IScreen> History = new Queue<IScreen>();

        public void Load(Type screen)
        {
            DeactivateScreen?.Invoke(this, new ManageScreenRequest
            {
                Close = true,
                Screen = activeItem
            });
            IScreen newScreen = CreateInstance(screen);
            ActivateScreen?.Invoke(this, new ManageScreenRequest
            {
                Screen = newScreen
            });
            activeItem = newScreen;
            History.Clear();
        }

        public void NavigateTo(Type screen)
        {
            IScreen newScreen = CreateInstance(screen);

            ActivateScreen?.Invoke(this, new ManageScreenRequest
            {
                Screen = newScreen
            });
            if (activeItem != null)
            {
                History.Enqueue(activeItem);
            }
            activeItem = newScreen;
        }

        public void GoBack()
        {
            DeactivateScreen?.Invoke(this, new ManageScreenRequest
            {
                Close = true,
                Screen = activeItem
            });
            IScreen newScreen = History.Dequeue();
            ActivateScreen?.Invoke(this, new ManageScreenRequest
            {
                Screen = newScreen
            });
            activeItem = newScreen;
        }

        private IScreen CreateInstance(Type screenType)
        {
            IScreen screen = Activator.CreateInstance(screenType, new object[] { }) as IScreen;
            IoC.BuildUp(screen);
            return screen;
        }
    }
}
