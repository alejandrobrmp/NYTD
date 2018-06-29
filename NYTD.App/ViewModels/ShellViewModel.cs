using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using NYTD.App.Models;
using NYTD.App.ViewModels;
using NYTD.App.Views;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Management;
using System.Windows.Interop;
using System.Security.Principal;
using Microsoft.Win32;
using System;

namespace NYTD.App.ViewModels {
    public class ShellViewModel :
        Conductor<IScreen>.Collection.OneActive,
        IShell,
        IHandle<EventAggregatorMessage<ShellViewModel>>,
        IHandle<EventAggregatorMessage<IScreen>>,
        IViewAware
    {
        #region DLLImports

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }
            return (IntPtr)0;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.X = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public static readonly RECT Empty = new RECT();
            public int Width { get { return Math.Abs(right - left); } }
            public int Height { get { return bottom - top; } }
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }
            public bool IsEmpty { get { return left >= right || top >= bottom; } }
            public override string ToString()
            {
                if (this == Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }
            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode() => left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2) { return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom); }
            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2) { return !(rect1 == rect2); }
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        #endregion

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

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            window = view as Window;
        }
        private Window window;
        protected override void OnActivate()
        {
            base.OnActivate();
            IntPtr handle = new WindowInteropHelper(window).EnsureHandle();
            HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
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

        public void Move(Window window, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
            else
            {
                if (WindowState == WindowState.Maximized)
                {
                    double percentHorizontal = e.GetPosition(window).X / window.ActualWidth;
                    double targetHorizontal = window.RestoreBounds.Width * percentHorizontal;

                    double percentVertical = e.GetPosition(window).Y / window.ActualHeight;
                    double targetVertical = window.RestoreBounds.Height * percentVertical;

                    WindowState = WindowState.Normal;

                    POINT lMousePosition;
                    GetCursorPos(out lMousePosition);

                    window.Left = lMousePosition.X - targetHorizontal;
                    window.Top = lMousePosition.Y - targetVertical;
                }
                window.DragMove();
            }
        }

        public void MenuClick(object sender, MouseButtonEventArgs e)
        {
            MenuItemViewModel item = sender as MenuItemViewModel;
            if (item != null)
            {
                ManageSelection(item);
            }
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