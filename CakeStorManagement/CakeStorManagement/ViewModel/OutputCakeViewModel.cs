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
    public class OutputCakeViewModel : BaseViewModel
    {

        private ObservableCollection<OutputInfor> _List;
        public ObservableCollection<OutputInfor> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private int _IdInput;
        public int IdInput { get => _IdInput; set { _IdInput = value; OnPropertyChanged(); } }

        //private Output _Output;
        //public Output Output { get => _Output; set { _Output = value; OnPropertyChanged(); } }

        private int _IdOutput;
        public int IdOutput { get => _IdOutput; set { _IdOutput = value; OnPropertyChanged(); } }

        private string _UserOutput;
        public string UserOutput { get => _UserOutput; set { _UserOutput = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        
        private DateTime? _DateOutput;
        public DateTime? DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }

        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        //private double _InputPrice;
        //public double InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }

        private double _OutputPrice;
        public double OutputPrice { get => _OutputPrice; set { _OutputPrice = value; OnPropertyChanged(); } }

        private ObservableCollection<double> _OutputPriceList;
        public ObservableCollection<double> OutputPriceList { get => _OutputPriceList; set { _OutputPriceList = value; OnPropertyChanged(); } }

        private double _SelectedPrice;
        public double SelectedPrice { get => _SelectedPrice; set { _SelectedPrice = value; OnPropertyChanged(); } }

        private string _Status;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private int _InventoryCake;
        public int InventoryCake { get => _InventoryCake; set { _InventoryCake = value; OnPropertyChanged(); } }

        private ObservableCollection<Cake> _Cake;
        public ObservableCollection<Cake> CakeList { get => _Cake; set { _Cake = value; OnPropertyChanged(); } }

        private Cake _SelectedCake;
        public Cake SelectedCake
        {
            get => _SelectedCake; set
            {
                _SelectedCake = value; OnPropertyChanged();
                if (SelectedCake != null)
                {
                    OutputPriceList = new ObservableCollection<double>();
                    var tempOutputPriceList = new ObservableCollection<InputInfor>(DataProvider.Ins.DB.InputInfors.Where(x => x.IdCake == SelectedCake.Id));
                    foreach (var item in tempOutputPriceList) { double price = item.OutputPrice; OutputPriceList.Add(price); }

                    InventoryList = LoadInvertoryList();
     
                    foreach(var item in InventoryList)
                    {
                        if(SelectedCake.Id == item.cake.Id)
                        {
                            InventoryCake = item.Count;
                            break;
                        }
                        
                    }
                }
            }
        }

        private ObservableCollection<Customer> _Customer;
        public ObservableCollection<Customer> CustomerList { get => _Customer; set { _Customer = value; OnPropertyChanged(); } }

        private Customer _SelectedCustomer;
        public Customer SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; OnPropertyChanged(); } }

        private OutputInfor _SelectedItem;
        public OutputInfor SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedCake = SelectedItem.Cake;
                    Count = SelectedItem.Count;
                    SelectedPrice = SelectedItem.OutputPrice;
                    //OutputPrice = SelectedItem.OutputPrice;
                    //OutputPrice = SelectedItem.OutputPrice;
                    Status = SelectedItem.Status;
                }
            }
        }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand FinishCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public OutputCakeViewModel()
        {
            UserOutput = MainViewModel.User;
            int IdUser = MainViewModel.IdUser;
            DateOutput = DateTime.Now;

            Output newOutput = new Output() { IdUser = IdUser, DateOutput = DateOutput };
            DataProvider.Ins.DB.Outputs.Add(newOutput);
            DataProvider.Ins.DB.SaveChanges();
            IdOutput = newOutput.Id;

            List = new ObservableCollection<OutputInfor>();
            CakeList = new ObservableCollection<Cake>(DataProvider.Ins.DB.Cakes);
            CustomerList = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);
            OutputPriceList = new ObservableCollection<double>();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedCake == null || SelectedCustomer == null || Count == 0 || Count > InventoryCake)
                    return false;

                return true;

            }, (p) =>
            {

                var OutputInforTemp = new OutputInfor()
                {
                    Cake = SelectedCake,
                    IdCake = SelectedCake.Id,
                    IdOutput = IdOutput,
                    Customer = SelectedCustomer,
                    OutputPrice = SelectedPrice,
                    Status = Status,
                    Count = Count
                };
                List.Add(OutputInforTemp);
            });
            FinishCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {

                foreach (var item in List)
                {
                    DataProvider.Ins.DB.OutputInfors.Add(item);
                }
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
            });
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedCake == null || SelectedCustomer == null || Count > InventoryCake)
                    return false;
                return true;

            }, (p) =>
            {
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Cake == SelectedItem.Cake && List[i].Status == SelectedItem.Status && List[i].Count == SelectedItem.Count && List[i].Customer == SelectedItem.Customer)
                    {
                        List[i] = new OutputInfor()
                        {
                            Cake = SelectedCake,
                            IdCake = SelectedCake.Id,
                            IdOutput = IdOutput,
                            OutputPrice = SelectedPrice,
                            Customer = SelectedCustomer,
                            Status = Status,
                            Count = Count
                        };
                        break;
                    }
                }
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedCake == null || SelectedCustomer == null)
                    return false;
                return true;

            }, (p) =>
            {
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Cake == SelectedItem.Cake  && List[i].Status == SelectedItem.Status && List[i].Count == SelectedItem.Count)
                    {
                        List.Remove(List[i]);
                        break;
                    }
                }

            });
        }

        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }

        ObservableCollection<Inventory> LoadInvertoryList()
        {
            ObservableCollection<Inventory> inventoryList = new ObservableCollection<Inventory>();

            var CakeList = DataProvider.Ins.DB.Cakes;
            int i = 1;
            foreach (var item in CakeList)
            {
                var inputList = DataProvider.Ins.DB.InputInfors.Where(p => p.IdCake == item.Id);
                var outputList = DataProvider.Ins.DB.OutputInfors.Where(p => p.IdCake == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList != null && inputList.Count() > 0)
                {
                    sumInput = inputList.Sum(p => p.Count);
                }
                if (outputList != null && outputList.Count() > 0)
                {
                    sumOutput = outputList.Sum(p => p.Count);
                }
                int inventoryCount = sumInput - sumOutput;
                Inventory temp = new Inventory() { STT = i, Count = inventoryCount, cake = item };
                inventoryList.Add(temp);
            }
            return inventoryList;
        }
    }

}
