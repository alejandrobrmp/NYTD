using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using NYTD.App.Models;
using NYTD.App.ViewModels;
using NYTD.App.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace NYTD.App.ViewModels {
    public class ShellViewModel :
        Conductor<IScreen>.Collection.OneActive,
        IShell,
        IHandle<EventAggregatorMessage<ShellViewModel>>
    {
        #region Services

        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Const

        private readonly string TITLE = "New YouTube Downloader";

        #endregion

        #region Private members

        private string title;
        private WindowState windowState = WindowState.Normal;
        private bool leftPanelToggle;
        private OptionMenuViewModel fixedOptions;
        private IScreen activeScreen;
        private OptionMenuViewModel currentPageOptions;
        private List<MenuItemViewModel> mainMenu;

        #endregion
        
        #region Public members

        public string Title
        {
            get => $"{TITLE}{(string.IsNullOrEmpty(title) ? "" : $" - {title}")}";
            set
            {
                title = value;
                NotifyOfPropertyChange();
            }
        }
        public WindowState WindowState
        {
            get => windowState;
            set
            {
                windowState = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(WindowMaximizeIcon));
            }
        }
        public PackIconKind WindowMaximizeIcon
        {
            get => WindowState == WindowState.Maximized ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize;
        }
        public OptionMenuViewModel FixedOptions { get => fixedOptions;
            set
            {
                fixedOptions = value;
                NotifyOfPropertyChange();
            }
        }
        public OptionMenuViewModel CurrentPageOptions { get => currentPageOptions;
            set
            {
                currentPageOptions = value;
                NotifyOfPropertyChange();
            }
        }
        public IScreen ActiveScreen { get => activeScreen; set => activeScreen = value; }
        public List<MenuItemViewModel> MainMenu { get => mainMenu; set => mainMenu = value; }
        public bool LeftPanelToggle
        {
            get => leftPanelToggle;
            set
            {
                leftPanelToggle = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region Constructor

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        #endregion

        #region Life cycle

        protected override void OnActivate()
        {
            base.OnActivate();
            mainMenu = new List<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Header = "Search",
                    Icon = PackIconKind.Magnify,
                    Screen = typeof(SearchViewModel),
                },
                new MenuItemViewModel
                {
                    Header = "Search",
                    Icon = PackIconKind.Download,
                    Screen = null,
                }
            };
            mainMenu[0].IsSelected = true;
            SelectedMenuChanged(this, new RoutedPropertyChangedEventArgs<object>(null, mainMenu[0]));
        }

        #endregion

        #region Handlers 
        public void Handle(EventAggregatorMessage<ShellViewModel> message)
        {
            switch (message.Kind)
            {
                case EventAggregatorMessageKind.TitleChangeRequest:
                    string newTitle = message.Message as string;
                    Title = newTitle;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Helpers

        public void SelectedMenuChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MenuItemViewModel newItem = e.NewValue as MenuItemViewModel;
            if (newItem.Items?.Count > 0 || newItem.Screen is null)
            {
                return;
            }
            DeactivateItem(ActiveItem, true);
            ActivateItem(Activator.CreateInstance(newItem.Screen, new object[] { _eventAggregator }) as IScreen);
            LeftPanelToggle = false;
        }

        public void MinimizeButton()
        {
            WindowState = WindowState.Minimized;
        }

        public void MaximizeButton()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        public void CloseButton(Window window)
        {
            window.Close();
        }

        #endregion

    }
}