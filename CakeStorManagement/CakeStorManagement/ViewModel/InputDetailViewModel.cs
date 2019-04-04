using CakeStorManagement.Model;
using CakeStorManagement.MyUserControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CakeStorManagement.ViewModel
{
    public class InputDetailViewModel : BaseViewModel
    {
        public ICommand SortCommand { get; set; }
        bool IsSort;

        private ObservableCollection<InputInfor> _List;
        public ObservableCollection<InputInfor> List { get => _List; set { _List = value; OnPropertyChanged();}}
        private int _IdInput;
        public int IdInput{get => _IdInput;set{_IdInput = value;OnPropertyChanged();}}
        private string _UserNameInput;
        public string UserNameInput { get => _UserNameInput; set { _UserNameInput = value; OnPropertyChanged();}}
        private DateTime? _DateInput;
        public DateTime? DateInput { get => _DateInput; set { _DateInput = value; OnPropertyChanged();}}
        private int _TotalCount;
        public int TotalCount{get => _TotalCount; set{_TotalCount = value;OnPropertyChanged();}}

        public InputDetailViewModel()
        {
            InputWindow inputWindow = new InputWindow();
            var inputVM = inputWindow.DataContext as InputViewModel;         

            IdInput =inputVM.IdInput;
            TotalCount = TotalCoutCake();

            List = new ObservableCollection<InputInfor>(DataProvider.Ins.DB.InputInfors.Where(x=> x.IdInput == IdInput));

            //CollectionView View = (CollectionView)CollectionViewSource.GetDefaultView(List);

            IsSort = false;
            SortCommand = new RelayCommand<object>((p) => { return true; }, p => {
                GridViewColumnHeader header = p as GridViewColumnHeader;
                CollectionView View = (CollectionView)CollectionViewSource.GetDefaultView(List);
                if (IsSort)
                {
                    View.SortDescriptions.Clear();
                    View.SortDescriptions.Add(new SortDescription(header.Content.ToString(), ListSortDirection.Ascending));
                }
                else
                {
                    View.SortDescriptions.Clear();
                    View.SortDescriptions.Add(new SortDescription(header.Content.ToString(), ListSortDirection.Descending));

                }
                IsSort = !IsSort;
            });

        }
        public int TotalCoutCake()
        {
            var inputList = DataProvider.Ins.DB.InputInfors.Where(x => x.IdInput == IdInput);

            int sumInput = 0;
            if (inputList != null && inputList.Count() > 0)
            {
                sumInput = (int)inputList.Sum(p => p.Count);
            }

            return sumInput;
        }
    }
}
