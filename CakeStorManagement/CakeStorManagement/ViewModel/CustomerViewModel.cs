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
    public class CustomerViewModel : BaseViewModel
    {

        private ObservableCollection<Customer> _List;
        public ObservableCollection<Customer> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Customer _SelectedItem;
        public Customer SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Address = SelectedItem.Address;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    ContractDate = SelectedItem.ContractDate;
                    MoreInfor = SelectedItem.MoreInfor;

                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _MoreInfor;
        public string MoreInfor { get => _MoreInfor; set { _MoreInfor = value; OnPropertyChanged(); } }

        private DateTime? _ContractDate; // thêm  "?": cho phép dữ liệu null
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, p => {
                var customer = new Customer() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfor = MoreInfor, ContractDate = ContractDate };
                DataProvider.Ins.DB.Customers.Add(customer);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(customer);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrWhiteSpace(DisplayName))
                    return false;
                if (String.IsNullOrEmpty(DisplayName))
                    return false;

                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, p => {
                var customer = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                customer.DisplayName = DisplayName;
                customer.DisplayName = DisplayName;
                customer.Address = Address;
                customer.Phone = Phone;
                customer.Email = Email;
                customer.MoreInfor = MoreInfor;
                customer.ContractDate = ContractDate;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DisplayName;
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Id == SelectedItem.Id)
                    {
                        List[i] = new Customer() { Id = SelectedItem.Id, DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfor = MoreInfor, ContractDate = ContractDate };
                        break;
                    }
                }
            });
        }
    }
}
