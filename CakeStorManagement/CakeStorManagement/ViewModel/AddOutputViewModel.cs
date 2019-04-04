using CakeStorManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CakeStorManagement.ViewModel
{
    public class AddOutputViewModel : BaseViewModel
    {
        private int _Id;
        public int IdOutput { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private ObservableCollection<User> _UserList;
        public ObservableCollection<User> UserList { get => _UserList; set { _UserList = value; OnPropertyChanged(); } }
        private DateTime? _DateOutput;
        public DateTime? DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }
        private User _SelectedUser;
        public User SelectedUser { get => _SelectedUser; set { _SelectedUser = value; OnPropertyChanged(); } }
        public ICommand FinishCommand { get; set; }

        public AddOutputViewModel()
        {
            UserList = new ObservableCollection<User>(DataProvider.Ins.DB.Users);

            FinishCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedUser == null || DateOutput == null)
                    return false;
                return true;

            }, (p) =>
            {
                OutputCake outputCake = new OutputCake();
                outputCake.DataContext = new OutputCakeViewModel();
                Output output = new Output()
                {
                    IdUser = SelectedUser.Id,
                    DateOutput = DateOutput
                };

                DataProvider.Ins.DB.Outputs.Add(output);
                DataProvider.Ins.DB.SaveChanges();
                IdOutput = output.Id;
                ((OutputCakeViewModel)outputCake.DataContext).IdOutput = IdOutput;
                ((OutputCakeViewModel)outputCake.DataContext).DateOutput = DateOutput;
                ((OutputCakeViewModel)outputCake.DataContext).UserOutput = SelectedUser.DisplayName;

                if (p != null)
                {
                    p.Close();
                    outputCake.ShowDialog();
                }

            });

        }
    }
}