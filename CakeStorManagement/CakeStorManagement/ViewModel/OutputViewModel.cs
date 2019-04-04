using CakeStorManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeStorManagement.ViewModel
{
    public class OutputViewModel: BaseViewModel
    {

        private int _Id;
        public int IdOutput { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private int _UserNameOutput;
        public int UserNameOutput { get => _UserNameOutput; set { _UserNameOutput = value; OnPropertyChanged(); } }
        private DateTime? _DateOutput;
        public DateTime? DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }

        private ObservableCollection<Output> _List;
        public ObservableCollection<Output> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Output _SelectedItem;
        public Output SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();

                if (SelectedItem != null)
                {
                    IdOutput = SelectedItem.Id;

                    OutputDetailWindow outputDetail = new OutputDetailWindow();
                    outputDetail.DataContext = new OutputDetailViewModel();
                    ((OutputDetailViewModel)outputDetail.DataContext).IdOutput = SelectedItem.Id;
                    ((OutputDetailViewModel)outputDetail.DataContext).DateOutput = SelectedItem.DateOutput;
                    ((OutputDetailViewModel)outputDetail.DataContext).UserNameOutput = SelectedItem.User.DisplayName;
                    outputDetail.ShowDialog();
                }
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }

        public OutputViewModel()
        { 
            List = new ObservableCollection<Output>(DataProvider.Ins.DB.Outputs);
        }
    }
}

