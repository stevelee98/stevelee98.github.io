using CakeStorManagement.Model;
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
    public class LoginViewModel : BaseViewModel
    {
        public bool Islogin { get; set; }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _PassWord;
        public string PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand PassWordChangedCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public LoginViewModel()
        {
            Islogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, p => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, p => { p.Close(); });
            PassWordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, p => { PassWord = p.Password; });
            void Login(Window p)
            {
                if (p == null)
                    return;
                var account = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName && x.PassWords == PassWord).Count();
                if (account > 0)
                {
                    Islogin = true;
                    var TempId = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName);
                    foreach (var item in TempId)
                    {
                        Id = item.Id;
                        break;

                    }
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.DataContext = new MainViewModel();
                    ((MainViewModel)mainWindow.DataContext).UserName = UserName;
                    ((MainViewModel)mainWindow.DataContext).Id = Id;                   

                    p.Close();
                }
                else
                {
                    Islogin = false;
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu !");
                }
            }
        }
    }
}
