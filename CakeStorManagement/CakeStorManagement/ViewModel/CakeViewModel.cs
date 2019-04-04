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
    public class CakeViewModel : BaseViewModel
    {
        public ICommand UnitCommand { get; set; }

        private ObservableCollection<Cake> _List;
        public ObservableCollection<Cake> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Unit> _Unit;
        public ObservableCollection<Unit> UnitList { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

        private ObservableCollection<Suplier> _Suplier;
        public ObservableCollection<Suplier> SuplierList { get => _Suplier; set { _Suplier = value; OnPropertyChanged(); } }

        private Cake _SelectedItem;
        public Cake SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    BarCode = SelectedItem.BarCode;
                    SelectedUnit = SelectedItem.Unit;
                    SelectedSuplier = SelectedItem.Suplier;
                }
            }
        }

        private Unit _SelectedUnit;
        public Unit SelectedUnit { get => _SelectedUnit; set { _SelectedUnit = value; OnPropertyChanged(); } }

        private Suplier _SelectedSuplier;
        public Suplier SelectedSuplier { get => _SelectedSuplier; set { _SelectedSuplier = value; OnPropertyChanged(); } }

        private Unit _UnitDisplayName;
        public Unit UnitDisplayName
        {
            get => _UnitDisplayName; set
            {
                UnitDisplayName = value;
                OnPropertyChanged();

            }
        }

        private Suplier _SuplierDisplayName;
        public Suplier SuplierDisplayName { get => _SuplierDisplayName; set { _SuplierDisplayName = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _QRCode;
        public string QRCode { get => _QRCode; set { _QRCode = value; OnPropertyChanged(); } }


        private string _BarCode;
        public string BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public CakeViewModel()
        {
            UnitCommand = new RelayCommand<Window>((p) => {return true; }, p =>
            {
                UnitWindow wd = new UnitWindow();
                if (p != null)
                {
                    p.Close();
                    wd.ShowDialog();
                }
               
            }
            );


            List = new ObservableCollection<Cake>(DataProvider.Ins.DB.Cakes);
            UnitWindow uw = new UnitWindow();
            var UnitDataContext = uw.DataContext as UnitViewModel;

            UnitList = new ObservableCollection<Unit>(UnitDataContext.List);
            SuplierList = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSuplier == null || SelectedUnit == null)
                    return false;
                return true;

            }, (p) =>
            {
                var cake = new Cake()
                {
                    DisplayName = DisplayName,
                    BarCode = BarCode,
                    IdSuplier = SelectedSuplier.Id,
                    IdUnit = SelectedUnit.Id,
                    Id = Guid.NewGuid().ToString(),
                    CakeType = 1
                };

                DataProvider.Ins.DB.Cakes.Add(cake);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(cake);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedSuplier == null || SelectedUnit == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Cakes.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var cake = DataProvider.Ins.DB.Cakes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                cake.DisplayName = DisplayName;
                cake.BarCode = BarCode;
                cake.IdSuplier = SelectedSuplier.Id;
                cake.IdUnit = SelectedUnit.Id;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DisplayName;
                for (int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Id == SelectedItem.Id)
                    {
                        List[i] = new Cake()
                        {
                            Id = SelectedItem.Id,
                            DisplayName = DisplayName,
                            BarCode = BarCode,               
                            IdSuplier = SelectedSuplier.Id,
                            IdUnit = SelectedUnit.Id,
                            Suplier = SelectedSuplier,
                            Unit = SelectedUnit
                        };
                        break;
                    }
                }
            });
        }
    }
}


