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
    public class AddInputViewModel:BaseViewModel
    {
        private int _Id;
        public int IdInput { get => _Id; set { _Id = value; OnPropertyChanged(); } }
  
        private ObservableCollection<User> _UserList;
        public ObservableCollection<User> UserList { get => _UserList; set { _UserList = value; OnPropertyChanged(); } }
        private DateTime? _DateInput;
        public DateTime? DateInput { get => _DateInput; set { _DateInput = value; OnPropertyChanged(); } }
        private User _SelectedUser;
        public User SelectedUser { get => _SelectedUser; set { _SelectedUser = value; OnPropertyChanged(); } }
        public ICommand FinishCommand { get; set; }
       
        public AddInputViewModel()
        {
            UserList = new ObservableCollection<User>(DataProvider.Ins.DB.Users);

            FinishCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedUser == null || DateInput == null)
                    return false;
                return true;

            }, (p) =>
            {
                InputCake inputCake = new InputCake();
                inputCake.DataContext = new InputCakeViewModel();
                Input input = new Input()
                {
                    IdUser = SelectedUser.Id,
                    DateInput = DateInput
                };

                DataProvider.Ins.DB.Inputs.Add(input);
                DataProvider.Ins.DB.SaveChanges();
                IdInput = input.Id;
                ((InputCakeViewModel)inputCake.DataContext).IdInput = IdInput;
                ((InputCakeViewModel)inputCake.DataContext).DateInput = DateInput;
                ((InputCakeViewModel)inputCake.DataContext).UserInput = SelectedUser.DisplayName;

                if (p != null)
                {
                    p.Close();
                    inputCake.ShowDialog();
                   
                }
            });

        }
    }
}
