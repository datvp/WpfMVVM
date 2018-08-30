using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace zSpaceWinApp.ViewModel
{
    public class ControlBarViewModel: BaseViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        public ICommand MouseDoubleClickWindowCommand { get; set; }
        private bool _IsMaximizeWindow;

        public bool IsMaximizeWindow
        {
            get { return _IsMaximizeWindow; }
            set { _IsMaximizeWindow = value; OnPropertyChanged(); }
        }
        
        public ControlBarViewModel()
        {
            MinimizeWindowCommand = new RelayCommand<UserControl>(p => { return true; }, p => {
                var window = GetParentWindow(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                    {
                        w.WindowState = WindowState.Minimized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Maximized;
                    }
                }
            });
            MaximizeWindowCommand = new RelayCommand<UserControl>(p => { return true; }, p => {
                var window = GetParentWindow(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Maximized)
                    {
                        IsMaximizeWindow = true;
                        w.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        IsMaximizeWindow = false;
                        w.WindowState = WindowState.Normal;
                    }
                }
            });
            MouseDoubleClickWindowCommand = new RelayCommand<UserControl>(p => { return true; }, p => {
                var window = GetParentWindow(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Maximized)
                    {
                        w.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Normal;
                    }
                }
            });
            CloseWindowCommand = new RelayCommand<UserControl>(p => { return true; }, p => {
                var window = GetParentWindow(p);
                var w = window as Window;
                if (w != null) w.Close();
            });
            MouseMoveWindowCommand = new RelayCommand<UserControl>(p => { return true; }, p => {
                var window = GetParentWindow(p);
                var w = window as Window;
                if (w != null) w.DragMove();
            });
        }

        private FrameworkElement GetParentWindow(UserControl uc)
        {
            FrameworkElement parent = uc;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
