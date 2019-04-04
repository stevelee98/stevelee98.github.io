using CakeStorManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeStorManagement.ViewModel
{
    public class OutputDetailViewModel : BaseViewModel
    {
        private ObservableCollection<OutputInfor> _List;
        public ObservableCollection<OutputInfor> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private int _IdOutput;
        public int IdOutput { get => _IdOutput; set { _IdOutput = value; OnPropertyChanged(); } }
        private string _UserNameOutput;
        public string UserNameOutput { get => _UserNameOutput; set { _UserNameOutput = value; OnPropertyChanged(); } }
        private DateTime? _DateOutput;
        public DateTime? DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }
        private int _TotalCount;
        public int TotalCount { get => _TotalCount; set { _TotalCount = value; OnPropertyChanged(); } }

        public OutputDetailViewModel()
        {
            OutputWindow OutputWindow = new OutputWindow();
            var outputVM = OutputWindow.DataContext as OutputViewModel;

            IdOutput = outputVM.IdOutput;
            TotalCount = TotalCoutCake();

            List = new ObservableCollection<OutputInfor>(DataProvider.Ins.DB.OutputInfors.Where(x => x.IdOutput == IdOutput));

        }
        public int TotalCoutCake()
        {
            var outputList = DataProvider.Ins.DB.OutputInfors.Where(x => x.IdOutput == IdOutput);

            int sumOutput = 0;
            if (outputList != null && outputList.Count() > 0)
            {
                sumOutput = (int)outputList.Sum(p => p.Count);
            }

            return sumOutput;
        }
    }
}
