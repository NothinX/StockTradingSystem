/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:StockTradingSystem.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/using System;using GalaSoft.MvvmLight;using GalaSoft.MvvmLight.Ioc;using GalaSoft.MvvmLight.Views;using Microsoft.Practices.ServiceLocation;using StockTradingSystem.Client.Model.UI.Dialog;using StockTradingSystem.Client.Model.UI.Navigation;namespace StockTradingSystem.Client.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
            }
            else
            {
                SimpleIoc.Default.Register<IDialogService, DialogService>(true);
            }
            InitNavigation();

            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// Gets the <see cref="Main"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// Gets the <see cref="FrameNavigationService"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public IFrameNavigationService FrameNavigationService => ServiceLocator.Current.GetInstance<IFrameNavigationService>();

        /// <summary>
        /// Gets the <see cref="DialogService"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public IDialogService DialogService => ServiceLocator.Current.GetInstance<IDialogService>();

        private static void InitNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("MainView", new Uri("../View/MainView.xaml", UriKind.Relative));
            navigationService.Configure("LoginView", new Uri("../View/LoginView.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<IDialogService>();
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Unregister<IFrameNavigationService>();
        }
    }
}