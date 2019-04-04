using CakeStorManagement.Model;
using CakeStorManagement.MyUserControl;
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
    public class InputViewModel : BaseViewModel
    {
        private int _Id;
        public int IdInput {get => _Id;set{ _Id = value;OnPropertyChanged();}}
        private string _UserNameInput;
        public string UserNameInput { get => _UserNameInput; set { _UserNameInput = value; OnPropertyChanged();}}
        private DateTime? _DateInput;
        public DateTime? DateInput { get => _DateInput; set { _DateInput = value; OnPropertyChanged();}}

        private ObservableCollection<Input> _List;
        public ObservableCollection<Input> List { get => _List; set { _List = value; OnPropertyChanged();}}
        
        private Input _SelectedItem;
        public Input SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();

                if (SelectedItem != null)
                {
                    IdInput = SelectedItem.Id;

                    InputDetailWindow inputDetail = new InputDetailWindow();
                    inputDetail.DataContext = new InputDetailViewModel();
                    ((InputDetailViewModel)inputDetail.DataContext).IdInput = SelectedItem.Id;
                    ((InputDetailViewModel)inputDetail.DataContext).DateInput = SelectedItem.DateInput;
                    ((InputDetailViewModel)inputDetail.DataContext).UserNameInput = SelectedItem.User.DisplayName;
                    inputDetail.ShowDialog();               
                }
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set;}

        public InputViewModel()
        {
            
            List = new ObservableCollection<Input>(DataProvider.Ins.DB.Inputs);
        }
    }

}
