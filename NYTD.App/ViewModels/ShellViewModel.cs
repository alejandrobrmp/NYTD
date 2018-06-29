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
        IHandle<EventAggregatorMessage<ShellViewModel>>,
        IHandle<EventAggregatorMessage<IScreen>>
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
        private IScreen activeScreen;
        private MenuItemViewModel activeMenu;
        private IScreen homeScreen;
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
        public IScreen ActiveScreen { get => activeScreen; set => activeScreen = value; }
        public List<MenuItemViewModel> MainMenu { get => mainMenu; set => mainMenu = value; }

        #endregion

        #region Constructor

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            homeScreen = new HomeViewModel(eventAggregator);
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
                    Header = "Downloads",
                    Icon = PackIconKind.Download,
                    Screen = null,
                },
                new MenuItemViewModel
                {
                    Header = "Management",
                    Icon = PackIconKind.FolderOutline,
                    Screen = null,
                    ItemsSource = new List<MenuItemViewModel>
                    {
                        new MenuItemViewModel
                        {
                            Header = "Settings",
                            Icon = PackIconKind.Settings,
                            Screen = null
                        }
                    }
                }
            };
            //mainMenu[0].IsSelected = true;
            //SelectedMenuChanged(this, new RoutedPropertyChangedEventArgs<object>(null, mainMenu[0]));
            ActivateItem(homeScreen);
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

        public void Handle(EventAggregatorMessage<IScreen> message)
        {
        }

        #endregion

        #region Helpers

        public void MenuClick(object sender, MouseButtonEventArgs e)
        {
            MenuItemViewModel item = sender as MenuItemViewModel;
            if (item != null)
            {
                ManageSelection(item);
            }
        }
        
        public void PreviewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        public void ManageSelection(MenuItemViewModel item)
        {
            if (item.HasItems)
            {
                item.IsSelected = false;
                if (activeMenu != null)
                    activeMenu.IsSelected = true;
            }
            else
            {
                if (item.Screen != null && activeMenu != item)
                {
                    activeMenu = item;
                    DeactivateItem(ActiveItem, false);
                    ActivateItem(Activator.CreateInstance(item.Screen, new object[] { _eventAggregator }) as IScreen);
                }
            }
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

        public void GoHome()
        {
            if (!ActiveItem.Equals(homeScreen))
            {
                activeMenu.IsSelected = false;
                activeMenu = null;
                DeactivateItem(ActiveItem, false);
                ActivateItem(homeScreen);
            }
        }

        #endregion

    }
}