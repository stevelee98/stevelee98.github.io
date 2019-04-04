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
    public class StatictisViewModel : BaseViewModel
    {
        bool Isloaded = false;

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        public static string User;
        public static int IdUser;

        private ObservableCollection<CakeCountRevenue> _CakeCountRevenue;
        public ObservableCollection<CakeCountRevenue> CakeCountRevenue { get => _CakeCountRevenue; set { _CakeCountRevenue = value; OnPropertyChanged(); } }

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
        private double _TotalRevenue;
        public double TotalRevenue { get => _TotalRevenue; set { _TotalRevenue = value; OnPropertyChanged(); } }
        private DateTime? _DateStart;
        public DateTime? DateStart { get => _DateStart; set { _DateStart = value; OnPropertyChanged(); } }
        private DateTime? _DateEnd;
        public DateTime? DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }

        // ********************************Contrustor**********************************************//
        public StatictisViewModel()
        {
            MainWindow mainWindow = new MainWindow();
            var mainVM = mainWindow.DataContext as MainViewModel;
            DateStart = mainVM.DateStart;
            DateEnd = mainVM.DateEnd;
            if(DateStart == null || DateEnd == null)
            {
                ViewStatisticWindow viewStatistic = new ViewStatisticWindow();
                var viewStatisticVM = viewStatistic.DataContext as ViewStatisticViewModel;
                DateStart = viewStatisticVM.DateStart;
                DateEnd = viewStatisticVM.DateEnd;

            }


            InputOutput = new ObservableCollection<Input_Output>(); //danh sách để hiển thị tỉ lệ nhập -xuất
            CakeListRevenue = new ObservableCollection<CakeListRevenue>();// danh sách để hiển thị doanh thu từng loại sản phẩm
            CakeCountRevenue = new ObservableCollection<CakeCountRevenue>();//danh sách các đối tượng CakeCountRevenue
            RevenueStore = new ObservableCollection<CakeListRevenue>(); //danh sách để hiển thị doanh thu

            TimeSpan daySpan = DateEnd.Value.Subtract(DateStart.Value);
            TimeSpan timeSpan = new TimeSpan(1, 0, 0, 0);
            int CountDay = daySpan.Days+1;
            int countInput = 0;
            int countOutput = 0;
            double revenueInDay = 0;
            Input_Output inputCount = new Input_Output();// đối tượng đếm số lượng nhập
            Input_Output outputCount = new Input_Output();//đối tượng đếm số lượng xuất
            var CakeList = DataProvider.Ins.DB.Cakes;

            for (int i=0; i<CountDay; i++)
            {
                CakeListRevenue revenue = new ViewModel.CakeListRevenue();
                revenueInDay = CalculateRevenueInDay(DateStart.Value.Add(new TimeSpan(i,0,0,0)));

                if (revenueInDay != 0)
                {
                    revenue = new CakeListRevenue() { Title = (DateStart.Value.Add(new TimeSpan(i, 0, 0, 0))).ToString(), Revenue = revenueInDay };
                    RevenueStore.Add(revenue);
                }
                countInput = countInput + CountInput(DateStart.Value.Add(new TimeSpan(i, 0, 0, 0)));
                countOutput = countOutput + CountOutput(DateStart.Value.Add(new TimeSpan(i, 0, 0, 0)));
            }

            foreach (var item in CakeList)
            {
                CakeCountRevenue ItemRevenue = new CakeCountRevenue();
                ItemRevenue = itemRevenueFromTo(item, DateStart.Value, DateEnd.Value);
                CakeCountRevenue.Add(ItemRevenue);

                double revenue = TotalRevenueOneCakeFromStarttoEnd(item, DateStart.Value, DateEnd.Value);
                CakeListRevenue.Add(new CakeListRevenue() { Title = item.DisplayName, Revenue = revenue });
            }

            Input_Output input_Output1 = new Input_Output() { Title = "Số lượng nhập", Count = countInput };
            Input_Output input_Output2 = new Input_Output() { Title = "Số lượng xuất", Count = countOutput };
            InputOutput.Add(input_Output1);
            InputOutput.Add(input_Output2);

            CountCakeMax = findCakeCoutMax(CakeCountRevenue).countOutput;
            CountMaxDisplayName = findCakeCoutMax(CakeCountRevenue).cake.DisplayName;
            CountCakeMin = findCakeCoutMin(CakeCountRevenue).countOutput;
            CountMinDisplayName = findCakeCoutMin(CakeCountRevenue).cake.DisplayName;
            TotalRevenue = 0;
            foreach (var item in RevenueStore)
            {
                TotalRevenue = TotalRevenue + item.Revenue;
            }
        }

        //doanh thu của 1 sản phẩm từ ngày bắt đầu đến ngày kết thúc
        double TotalRevenueOneCakeFromStarttoEnd(Cake cake, DateTime start, DateTime end)
        {
            double TotalRevenueFromTo = 0;


            TimeSpan daySpan = DateEnd.Value.Subtract(DateStart.Value);
            TimeSpan timeSpan = new TimeSpan(1, 0, 0, 0);
            int CountDay = daySpan.Days + 1;

            for (int i = 0; i < CountDay; i++)
            {
                TotalRevenueFromTo = TotalRevenueFromTo + CalculateRevenueOneCakeInDay(cake, DateStart.Value.Add(new TimeSpan(i, 0, 0, 0)));
            }

            return TotalRevenueFromTo ;
        }
        //doanh thu của 1 sản phẩm trong 1 ngày
        public double CalculateRevenueOneCakeInDay(Cake cake, DateTime date)
        {
            double RevenueInDay = 0;

            var outputList = DataProvider.Ins.DB.OutputInfors.Where(x =>x.IdCake == cake.Id && x.Output.DateOutput.Value.Day == date.Day && x.Output.DateOutput.Value.Month == date.Month && x.Output.DateOutput.Value.Year == date.Year);
            foreach (var item in outputList)
            {
                RevenueInDay = RevenueInDay + item.Count * item.OutputPrice;
            }
            return RevenueInDay;

        }

        // tính tổng doanh thu trong 1 ngày
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
        
        //tìm sản phẩm có lượng xuất kho lớn nhất
        CakeCountRevenue findCakeCoutMax(ObservableCollection<CakeCountRevenue> list)
        {
            CakeCountRevenue max = new CakeCountRevenue();
            max = list[0];

            foreach (var item in list)
            {
                if (item.countOutput > max.countOutput)
                {
                    max = item;
                }
            }
            return max;
        }
        //tìm sản phẩm có lượng xuất kho nhỏ nhất
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
        public CakeCountRevenue itemRevenueFromTo(Cake cake, DateTime start, DateTime end)
        {
            CakeCountRevenue temp = new CakeCountRevenue();

            int sumInput = 0;
            int sumOutput = 0;
            int inventory = 0;
            double revenue = 0;
            double profit = 0;

            TimeSpan daySpan = DateEnd.Value.Subtract(DateStart.Value);
            TimeSpan timeSpan = new TimeSpan(1, 0, 0, 0);
            int CountDay = daySpan.Days + 1;

            for (int i = 0; i < CountDay; i++)
            {
                sumInput = sumInput + itemRevenueInDay(cake, DateStart.Value.Add(new TimeSpan(i, 0, 0, 0))).countInput;
                sumOutput = sumOutput + itemRevenueInDay(cake, DateStart.Value.Add(new TimeSpan(i, 0, 0, 0))).countOutput;
                inventory = sumOutput - sumInput;
                revenue = revenue + itemRevenueInDay(cake, DateStart.Value.Add(new TimeSpan(i, 0, 0, 0))).revenue;
                profit = profit + itemRevenueInDay(cake, DateStart.Value.Add(new TimeSpan(i, 0, 0, 0))).profit;
            }

            temp.cake = cake;
            temp.countOutput = sumOutput;
            temp.countInput = sumInput;
            temp.inventory = inventory;
            temp.revenue = revenue;
            temp.profit = profit;

            return temp;
        }
        public CakeCountRevenue itemRevenueInDay(Cake cake, DateTime date)
        {
            CakeCountRevenue temp = new CakeCountRevenue();
            var inputInforList = DataProvider.Ins.DB.InputInfors.Where(x => x.IdCake == cake.Id && x.Input.DateInput.Value.Day == date.Day && x.Input.DateInput.Value.Month == date.Month && x.Input.DateInput.Value.Year == date.Year);
            var outputInforList = DataProvider.Ins.DB.OutputInfors.Where(x => x.IdCake == cake.Id && x.Output.DateOutput.Value.Day == date.Day && x.Output.DateOutput.Value.Month == date.Month && x.Output.DateOutput.Value.Year == date.Year);

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
            foreach (var item in inputInforList)
            {
                priceInput = priceInput + item.Count * item.InputPrice;
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

        //tính tổng lượng nhập trong 1 ngày
        public int CountInput(DateTime date )
        {
            int Count = 0;
            var inputList = DataProvider.Ins.DB.InputInfors.Where(x => x.Input.DateInput.Value.Day == date.Day && x.Input.DateInput.Value.Month == date.Month && x.Input.DateInput.Value.Year == date.Year);

            if (inputList != null && inputList.Count() > 0)
            {
                Count = (int)inputList.Sum(x => x.Count);
            }
            return Count;
        }

        //tính tổng lượng xuất trong 1 ngày
        public int CountOutput(DateTime date)
        {
            int Count = 0;
            var outputList = DataProvider.Ins.DB.OutputInfors.Where(x => x.Output.DateOutput.Value.Day == date.Day && x.Output.DateOutput.Value.Month == date.Month && x.Output.DateOutput.Value.Year == date.Year);

            if (outputList != null && outputList.Count() > 0)
            {
                Count = (int)outputList.Sum(x => x.Count);
            }
            return Count;
        }
    }

    //public class Input_Output : BaseViewModel
    //{
    //    public string Title { get; set; }
    //    public int Count { get; set; }    
    //}
    //public class CakeListRevenue : BaseViewModel
    //{
    //    public string Title { get; set; }
    //    public double Revenue { get; set; }
    //}

}
