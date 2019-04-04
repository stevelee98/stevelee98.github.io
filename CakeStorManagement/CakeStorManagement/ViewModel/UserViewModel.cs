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
    public class UserViewModel : BaseViewModel
    {
        private ObservableCollection<User> _List;
        public ObservableCollection<User> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<UserRole> _UserRoleList;
        public ObservableCollection<UserRole> UserRoleList { get => _UserRoleList; set { _UserRoleList = value; OnPropertyChanged(); } }
        private UserRole _SelectedUserRole;
        public UserRole SelectedUserRole { get => _SelectedUserRole; set { _SelectedUserRole =value;  OnPropertyChanged(); } }
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _PassWord;
        public string PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }
        private User _SelectedItem;
        public User SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); if (SelectedItem != null)
                {
                    Id = SelectedItem.Id.ToString();
                    DisplayName = SelectedItem.DisplayName;
                    UserName = SelectedItem.UserName;
                    SelectedUserRole = SelectedItem.UserRole;
                    PassWord = SelectedItem.PassWords;
                } } }
        private string _UserName;
        public string UserName { get =>_UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }


        public UserViewModel()
        {
            List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
            UserRoleList = new ObservableCollection<UserRole>(DataProvider.Ins.DB.UserRoles);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(UserName) != true && string.IsNullOrEmpty(DisplayName) != true && string.IsNullOrEmpty(PassWord) != true)
                {
                    return true;
                }                    
      
                return false;
            }, p => {
                var user = new User() {

                    DisplayName = DisplayName,
                    UserName = UserName,
                    PassWords = PassWord,
                    UserRole = SelectedUserRole,
                };
                DataProvider.Ins.DB.Users.Add(user);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(user);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(UserName) != true && string.IsNullOrEmpty(DisplayName) != true && string.IsNullOrEmpty(PassWord) != true)
                {
                    return true;
                }
                int temp = 0;
                Int32.TryParse(Id, out temp);
                var displayList = DataProvider.Ins.DB.Users.Where(x => x.Id == temp);
                if (displayList != null || displayList.Count() != 0)
                    return true;
                return false;
            }, p => {
                var user = DataProvider.Ins.DB.Users.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                user.DisplayName = DisplayName;
                user.PassWords = PassWord;
                user.UserName = UserName;
                user.IdRole = SelectedUserRole.Id;

                DataProvider.Ins.DB.SaveChanges();
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Id == SelectedItem.Id)
                    {
                        List[i] = new User() { Id = SelectedItem.Id, DisplayName = DisplayName, PassWords = PassWord, UserName = UserName, IdRole = SelectedUserRole.Id, UserRole = SelectedUserRole};
                        break;
                    }
                }
            });
        }
    }
}
