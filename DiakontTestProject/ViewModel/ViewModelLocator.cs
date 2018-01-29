/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DiakontTestProject"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Windows;
using DiakontTestProject.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace DiakontTestProject.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RatesViewModel>();
            SimpleIoc.Default.Register<PositionsViewModel>();
            SimpleIoc.Default.Register<ReportViewModel>();

            SimpleIoc.Default.Register<IReferenceRepository, ReferenceRepository>();
            SimpleIoc.Default.Register<IRepository, DefaultRepository>();

          
        }

        public static MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static RatesViewModel Rates
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RatesViewModel>();
            }
        }

        public static PositionsViewModel Positions
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PositionsViewModel>();
            }
        }

        public static ReportViewModel Report
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReportViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}