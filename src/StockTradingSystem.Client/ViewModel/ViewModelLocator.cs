/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:StockTradingSystem.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using StockTradingSystem.Client.Model.UI.Dialog;
using StockTradingSystem.Client.Model.UI.Navigation;
using StockTradingSystem.Client.ViewModel.Control;

namespace StockTradingSystem.Client.ViewModel
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

            SimpleIoc.Default.Register<MainWindowModel>();
            SimpleIoc.Default.Register<AccountButtonViewModel>();
        }

        /// <summary>
        /// Gets the <see cref="Main"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainWindowModel Main => ServiceLocator.Current.GetInstance<MainWindowModel>();

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

        /// <summary>
        /// Gets the <see cref="AccountBtn"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AccountButtonViewModel AccountBtn => ServiceLocator.Current.GetInstance<AccountButtonViewModel>();

        private static void InitNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("MainView", new Uri("../View/MainView.xaml", UriKind.Relative));
            navigationService.Configure("LoginView", new Uri("../View/LoginView.xaml", UriKind.Relative));
            navigationService.Configure("RegisterView", new Uri("../View/RegisterView.xaml", UriKind.Relative));
            navigationService.Configure("StockView", new Uri("../View/StockView.xaml", UriKind.Relative));
            navigationService.Configure("TradeView", new Uri("../View/TradeView.xaml", UriKind.Relative));
            navigationService.Configure("AccountView", new Uri("../View/AccountView.xaml", UriKind.Relative));
            navigationService.Configure("SettingsView", new Uri("../View/SettingsView.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<IDialogService>();
            SimpleIoc.Default.Unregister<AccountButtonViewModel>();
            SimpleIoc.Default.Unregister<MainWindowModel>();
            SimpleIoc.Default.Unregister<IFrameNavigationService>();
        }
    }
}