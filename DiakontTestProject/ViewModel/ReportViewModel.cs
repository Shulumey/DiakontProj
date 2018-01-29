using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DiakontTestProject.Data;
using DiakontTestProject.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace DiakontTestProject.ViewModel
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly IRepository _repository;
        private DateTime? _fromDate;
        private DateTime? _toDate;

        public ReportViewModel(IRepository repository)
        {
            _repository = repository;
            Result = new ObservableCollection<SearchResult>();
            SearchCommand = new RelayCommand(Search);
            ViewModelLocator.Main.Title = "Отчет о затратах";
        }

        private void Search()
        {
            try
            {
                Result.Clear();

                var result = _repository.FindFundOfSalary(FromDate ?? DateTime.MinValue, ToDate ?? DateTime.MaxValue);
                foreach (var searchResult in result)
                {
                    Result.Add(searchResult);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ObservableCollection<SearchResult> Result { get; private set; }
        public ICommand SearchCommand { get; set; }

        public DateTime? FromDate
        {
            get => _fromDate;
            set { Set(() => FromDate, ref _fromDate, value); }
        }

        public DateTime? ToDate
        {
            get => _toDate;
            set { Set(() => ToDate, ref _toDate, value); }
        }
    }
}
