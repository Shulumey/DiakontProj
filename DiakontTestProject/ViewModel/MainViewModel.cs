using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace DiakontTestProject.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private string _title;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            RatesCommand = new RelayCommand(() => CurrentViewModel = ViewModelLocator.Rates);
            PositionsCommand = new RelayCommand(() => CurrentViewModel = ViewModelLocator.Positions);
            ReportCommand = new RelayCommand(() => CurrentViewModel = ViewModelLocator.Report);
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(() => CurrentViewModel, ref _currentViewModel, value); }
        }

        public string Title
        {
            get { return _title; }
            set { Set(() => Title, ref _title, value); }
        }

        public ICommand RatesCommand { get; private set; }

        public ICommand PositionsCommand { get; private set; }

        public ICommand ReportCommand { get; private set; }
    }
}