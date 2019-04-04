using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeStorManagement.ViewModel
{
    public class ViewStatisticViewModel : BaseViewModel
    {
        public ICommand StatisticCommand { get; set; }

        private DateTime? _DateStart;
        public DateTime? DateStart { get => _DateStart; set { _DateStart = value; OnPropertyChanged(); } }
        private DateTime? _DateEnd;
        public DateTime? DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }

        public ViewStatisticViewModel()
        {
            StatisticCommand = new RelayCommand<object>((p) =>
            {
                if (DateStart == null || DateEnd == null)
                    return false;
                return true;
            }, p =>
            {
                StatisticWindow statisticWindow = new StatisticWindow();
                statisticWindow.DataContext = new StatictisViewModel();
                ((StatictisViewModel)statisticWindow.DataContext).DateStart = DateStart;
                ((StatictisViewModel)statisticWindow.DataContext).DateEnd = DateEnd;
                statisticWindow.ShowDialog();
            }
           );
        }
    }
}
