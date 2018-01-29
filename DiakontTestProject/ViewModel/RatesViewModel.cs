using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using DiakontTestProject.Data;
using DiakontTestProject.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace DiakontTestProject.ViewModel
{
    public class RatesViewModel : ViewModelBase
    {
        private readonly IReferenceRepository _referenceRepository;
        private readonly IRepository _repository;
        private ReferenceItem _selectedPosition;
        private int _amount;

        public RatesViewModel(IReferenceRepository referenceRepository, IRepository repository)
        {
            _referenceRepository = referenceRepository;
            _repository = repository;

            ViewModelLocator.Main.Title = "Ставки";
            AddCommand = new RelayCommand(AddRate);
            LoadedCommand = new RelayCommand(Loaded);
            Rates = new ObservableCollection<Rate>();
        }

        public IEnumerable<ReferenceItem> Positions { get; private set; }

        public ObservableCollection<Rate> Rates { get; set; }

        public ReferenceItem SelectedPosition
        {
            get => _selectedPosition;
            set { Set(() => SelectedPosition, ref _selectedPosition, value); }
        }

        public int Amount
        {
            get => _amount;
            set { Set(() => Amount, ref _amount, value); }
        }

        public DateTime? SelectedDate { get; set; }

        public ICommand AddCommand { get; }
        public ICommand LoadedCommand { get; set; }

        private void AddRate()
        {
            try
            {
                Rate newRate = new Rate(SelectedPosition, Amount, SelectedDate ?? default(DateTime));
                _repository.AddRate(newRate);
                Rates.Add(newRate);

                SelectedPosition = null;
                SelectedDate = null;
                Amount = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Loaded()
        {
            try
            {
                Positions = _referenceRepository.GetPositions();
                var rates = _repository.GetRates();
                foreach (var rate in rates)
                {
                    Rates.Add(rate);
                }
                RaisePropertyChanged(() => Positions);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }         
        }
    }
}
