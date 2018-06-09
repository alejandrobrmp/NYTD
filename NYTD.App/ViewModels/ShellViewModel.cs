using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using NYTD.App.Models;
using NYTD.App.Services.Interfaces;
using NYTD.App.ViewModels;
using NYTD.App.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace NYTD.App.ViewModels {
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        #region Services

        private readonly IEventAggregator _eventAggregator;
        private readonly IMenuService _menuService;

        #endregion

        #region Const

        private readonly string TITLE = "NYTD (New YouTube Downloader)";

        #endregion

        #region Private members

        private string title;
        private OptionMenuViewModel fixedOptions;
        private IScreen activeScreen;
        private OptionMenuViewModel currentPageOptions;

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

        #endregion

        #region Constructor

        public ShellViewModel(IEventAggregator eventAggregator, IMenuService menuService)
        {
            _eventAggregator = eventAggregator;
            _menuService = menuService;
        }

        #endregion

        #region Life cycle

        protected override void OnActivate()
        {
        }

        #endregion

        #region Helpers


        #endregion

    }
}