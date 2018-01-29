using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DiakontTestProject.Data;
using DiakontTestProject.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace DiakontTestProject.ViewModel
{
    public class PositionsViewModel : ViewModelBase
    {
        private readonly IReferenceRepository _referenceRepository;
        private readonly IRepository _repository;
        private ReferenceItem _selectedPosition;
        private int _employersCount;
        private ReferenceItem _selectedDepartment;

        public PositionsViewModel(IReferenceRepository referenceRepository, IRepository repository)
        {
            _referenceRepository = referenceRepository;
            _repository = repository;

            ViewModelLocator.Main.Title = "Должности";

            AddCommand = new RelayCommand(AddRate);
            LoadedCommand = new RelayCommand(Loaded);

            Schedule = new ObservableCollection<SchedulePosition>();
        }

        public IEnumerable<ReferenceItem> Positions { get; set; }
        public IEnumerable<ReferenceItem> Departments { get; set; }

        public ObservableCollection<SchedulePosition> Schedule { get; set; }

        public ReferenceItem SelectedPosition
        {
            get => _selectedPosition;
            set { Set(() => SelectedPosition, ref _selectedPosition, value); }
        }

        public ReferenceItem SelectedDepartment
        {
            get => _selectedDepartment;
            set { Set(() => SelectedDepartment, ref _selectedDepartment, value); }
        }

        public int EmployersCount
        {
            get => _employersCount;
            set { Set(() => EmployersCount, ref _employersCount, value); }
        }

        public DateTime? SelectedDate { get; set; }

        public DateTime MinSelelectionDate => DateTime.Today;

        public ICommand AddCommand { get; }

        public ICommand LoadedCommand { get; set; }

        private void AddRate()
        {
            try
            {
                SchedulePosition newSchedulePosition = new SchedulePosition(SelectedDepartment, SelectedPosition, SelectedDate ?? default(DateTime), EmployersCount);
                _repository.AddSchedulePosition(newSchedulePosition);
                Schedule.Add(newSchedulePosition);
                
                SelectedPosition = null;
                SelectedDate = null;
                EmployersCount = 0;
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
                Departments = _referenceRepository.GetDepartments();
                var schedulePositions = _repository.GetSchedule();

                foreach (var item in schedulePositions)
                {
                    Schedule.Add(item);
                }
                RaisePropertyChanged(() => Positions);
                RaisePropertyChanged(() => Departments);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
