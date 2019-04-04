using CakeStorManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CakeStorManagement.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseLeftButtonDownWindowCommand { get; set; }
        #endregion
        private Window _windowParent;
        public Window windowParent { get => _windowParent; set {
                UserControl p = new UserControl();
                FrameworkElement window_Parent = GetWindowParent(p);
                _windowParent = window_Parent as Window;
            } }

        public ControlBarViewModel()
        {
            AboutCommand = new RelayCommand<UserControl>((p) => { return true; }, p =>
            {
                MessageBox.Show("Phần mềm quản lý cửa hàng bánh - đồ án cuối kì \n Người thực hiện: Lê Hoàng Việt,  MSSV: 1612808", "About us");

            }
            );
            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, p =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            }
            );
            MaximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, p =>
            {
                FrameworkElement window = GetWindowParent(p);
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
            }
            );
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, p =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.WindowState = WindowState.Minimized;
                }
            }
           );
            MouseLeftButtonDownWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, p =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            }
        );
        }

        private FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
