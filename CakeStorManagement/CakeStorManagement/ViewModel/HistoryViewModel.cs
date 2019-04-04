using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CakeStorManagement.ViewModel
{
    public class HistoryViewModel: BaseViewModel
    {
        public ICommand InputHistoryCommand { get; set; }
        public ICommand OutputHistoryCommand { get; set; }
        public HistoryViewModel()
        {
            InputHistoryCommand = new RelayCommand<object>((p) => { return true; }, p => { InputWindow wd = new InputWindow(); wd.ShowDialog(); });
            OutputHistoryCommand = new RelayCommand<object>((p) => { return true; }, p => { OutputWindow wd = new OutputWindow(); wd.ShowDialog(); });
        }


    }
}
