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
    public class MainViewModel : BaseViewModel
    {
        bool Isloaded = false;

        private string _UserName;
        public string  UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        public static string User;
        public static int IdUser;


        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand CakeCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand StatisticCommand { get; set; }
        public ICommand ViewStatisticCommand { get; set; }

        private ObservableCollection<CakeCountRevenue> _CakeCountRevenue;
        public ObservableCollection<CakeCountRevenue> CakeCountRevenue { get => _CakeCountRevenue; set{_CakeCountRevenue = value; OnPropertyChanged();}}

        private ObservableCollection<Input_Output> _InputOutput;
        public ObservableCollection<Input_Output> InputOutput { get => _InputOutput; set { _InputOutput = value; OnPropertyChanged(); } }

        private ObservableCollection<CakeListRevenue> _CakeListRevenue;
        public ObservableCollection<CakeListRevenue> CakeListRevenue { get => _CakeListRevenue; set { _CakeListRevenue = value; OnPropertyChanged(); } }

        private ObservableCollection<CakeListRevenue> _RevenueStore;
        public ObservableCollection<CakeListRevenue> RevenueStore { get => _RevenueStore; set { _RevenueStore = value; OnPropertyChanged(); } }


        private Cake _cake;
        public Cake cake { get => _cake; set { _cake = value; OnPropertyChanged(); } }
        private int _CountCakeMax;
        public int CountCakeMax { get => _CountCakeMax; set { _CountCakeMax = value; OnPropertyChanged(); } }
        private string _CountMaxDisplayName;
        public string CountMaxDisplayName { get => _CountMaxDisplayName; set { _CountMaxDisplayName = value; OnPropertyChanged(); } }
        private int _CountCakeMin;
        public int CountCakeMin { get => _CountCakeMin; set { _CountCakeMin = value; OnPropertyChanged(); } }
        private string _CountMinDisplayName;
        public string CountMinDisplayName { get => _CountMinDisplayName; set { _CountMinDisplayName = value; OnPropertyChanged(); } }
        private double _RevenueInMonth;
        public double RevenueInMonth { get => _RevenueInMonth; set {_RevenueInMonth = value;OnPropertyChanged();} }
        
        private DateTime? _DateStart;
        public DateTime? DateStart { get => _DateStart; set { _DateStart = value; OnPropertyChanged(); } }
        private DateTime? _DateEnd;
        public DateTime? DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }

        // ********************************Contrustor**********************************************//

        public MainViewModel()
        {
            int coutInput = CountInput();
            int coutOutput = CountOutput();
            Input_Output input_Output1 = new Input_Output() { Title = "Số lượng nhập", Count = coutInput };
            Input_Output input_Output2 = new Input_Output() { Title = "Số lượng xuất", Count = coutOutput };

            InputOutput = new ObservableCollection<Input_Output>();
            InputOutput.Add(input_Output1);
            InputOutput.Add(input_Output2);

            CakeListRevenue = new ObservableCollection<CakeListRevenue>();
            CakeCountRevenue = new ObservableCollection<CakeCountRevenue>();
            RevenueStore = new ObservableCollection<CakeListRevenue>();
            var CakeList = DataProvider.Ins.DB.Cakes;
            foreach(var item in CakeList)
            {
                CakeCountRevenue ItemRevenue = new CakeCountRevenue();
                ItemRevenue = itemRevenue(item);
                CakeCountRevenue.Add(ItemRevenue);
                CakeListRevenue.Add(new CakeListRevenue() { Title = ItemRevenue.cake.DisplayName, Revenue = ItemRevenue.revenue });
            }
            CountCakeMax = findCakeCoutMax(CakeCountRevenue).countOutput;
            CountMaxDisplayName = findCakeCoutMax(CakeCountRevenue).cake.DisplayName;
            CountCakeMin = findCakeCoutMin(CakeCountRevenue).countOutput;
            CountMinDisplayName = findCakeCoutMin(CakeCountRevenue).cake.DisplayName;
            RevenueInMonth = CalculateRevenueInMonth();

            int Day = 7;
            double revenueInDay = 0;
            DateTime Now = DateTime.Now;
            for (int i = Day; i >0; i--)
            {
                CakeListRevenue revenue = new ViewModel.CakeListRevenue();
                revenueInDay = CalculateRevenueInDay(Now.Subtract(new TimeSpan(i, 0, 0, 0)));

                    revenue = new CakeListRevenue() { Title = (Now.Subtract(new TimeSpan(i, 0, 0, 0))).ToString(), Revenue = revenueInDay };
                    RevenueStore.Add(revenue);
             
            }


            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, p =>
            {
                Isloaded = true;
                
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                //p.Show();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.Islogin)
                {
                    Id = loginVM.Id;
                    UserName = loginVM.UserName;

                    User = UserName;
                    IdUser = Id;
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            }
          );
            
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
            SuplierCommand = new RelayCommand<object>((p) => { if (Id != 1) { return false; } return true; }, p => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });
            CustomerCommand = new RelayCommand<object>((p) => { if (Id != 1) { return false; } return true; }, p => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            CakeCommand = new RelayCommand<object>((p) => { if (Id != 1) { return false; } return true; }, p => { CakeWindow wd = new CakeWindow(); wd.ShowDialog(); });
            UserCommand = new RelayCommand<object>((p) => { if (Id != 1) { return false; } return true; }, p => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            InputCommand = new RelayCommand<object>((p) => { return true; }, p => { InputCake wd = new InputCake(); wd.ShowDialog(); });
            OutputCommand = new RelayCommand<object>((p) => { return true; }, p => { OutputCake wd = new OutputCake(); wd.ShowDialog(); });
            HistoryCommand = new RelayCommand<object>((p) => { return true; }, p => { HistoryWindow wd = new HistoryWindow(); wd.ShowDialog(); });
            ViewStatisticCommand = new RelayCommand<object>((p) => { if (Id != 1) { return false; } return true; }, p => { ViewStatisticWindow wd = new ViewStatisticWindow(); wd.ShowDialog(); });

        }

        public double CalculateRevenueInDay(DateTime date)
        {
            double RevenueInDay = 0;

            var outputList = DataProvider.Ins.DB.OutputInfors.Where(x => x.Output.DateOutput.Value.Day == date.Day && x.Output.DateOutput.Value.Month == date.Month && x.Output.DateOutput.Value.Year == date.Year);

            foreach (var item in outputList)
            {
                RevenueInDay = RevenueInDay + item.Count * item.OutputPrice;
            }
            return RevenueInDay;
        }

        public double CalculateRevenueInMonth()
        {
            double RevenueInMonth = 0;

            DateTime dateTime;
            dateTime = DateTime.Today;
            int thisMonth = dateTime.Month;

            var outputList = DataProvider.Ins.DB.OutputInfors.Where(x=> x.Output.DateOutput.Value.Month == thisMonth);
            

            foreach(var item in outputList)
            {
                RevenueInMonth = RevenueInMonth + item.Count * item.OutputPrice;
            }


            return RevenueInMonth;
        }

        public CakeCountRevenue findCakeCoutMax(ObservableCollection<CakeCountRevenue> list)
        {
            CakeCountRevenue max = new CakeCountRevenue();
            max = list[0];

            foreach (var item in list)
            {
                if(item.countOutput > max.countOutput)
                {
                    max = item;
                }
            }
            return max;
        }
        public CakeCountRevenue findCakeCoutMin(ObservableCollection<CakeCountRevenue> list)
        {
            CakeCountRevenue min = new CakeCountRevenue();
            min = list[0];

            foreach (var item in list)
            {
                if (item.countOutput < min.countOutput)
                {
                    min = item;
                }
            }
            return min;
        }
        //Chuyển đổi Cake thành CakeCountRevenue
        public CakeCountRevenue itemRevenue(Cake cake)
        {
            CakeCountRevenue temp = new CakeCountRevenue();
            var inputInforList = DataProvider.Ins.DB.InputInfors.Where(x => x.IdCake == cake.Id);
            var outputInforList = DataProvider.Ins.DB.OutputInfors.Where(x => x.IdCake == cake.Id);

            double priceInput = 0;
            double priceOutput = 0;
            int sumInput = 0;
            int sumOutput = 0;
            int inventory = 0;
            double revenue = 0;
            double profit = 0;

            if (inputInforList != null && inputInforList.Count() > 0)
            {
                sumInput = (int)inputInforList.Sum(p => p.Count);
            }
            if (outputInforList != null && outputInforList.Count() > 0)
            {
                sumOutput = (int)outputInforList.Sum(p => p.Count);
            }
            inventory = sumInput - sumOutput;
            foreach(var item in inputInforList)
            {
                priceInput = priceInput +item.Count * item.InputPrice;
            }
            foreach (var item in outputInforList)
            {
                priceOutput = priceOutput + item.Count * item.OutputPrice;
            }
            profit = priceOutput - priceInput;
            revenue = priceOutput;

            temp.cake = cake;
            temp.countOutput = sumOutput;
            temp.countInput = sumInput;
            temp.inventory = inventory;
            temp.revenue = revenue;
            temp.profit = profit;

            return temp;
        }
        public int CountInput()
        {
            int Count = 0;
            var inputList = DataProvider.Ins.DB.InputInfors;

            if(inputList != null && inputList.Count() >0)
            {
                Count = (int)inputList.Sum(x => x.Count);
            }
            return Count;
        }
        public int CountOutput()
        {
            int Count = 0;
            var outputList = DataProvider.Ins.DB.OutputInfors;

            if (outputList != null && outputList.Count() > 0)
            {
                Count = (int)outputList.Sum(x => x.Count);
            }
            return Count;
        }
    }
    public class Input_Output : BaseViewModel
    {
        public string Title { get ; set; }
        public int Count { get; set; }
    }
    public class CakeListRevenue : BaseViewModel
    {
        public string Title { get; set; }
        public double Revenue { get; set; }
    }
}
