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
using StockTradingSystem.Client.Design.Model;
using StockTradingSystem.Client.Design.Model.Access;
using StockTradingSystem.Client.Design.Model.Business;
using StockTradingSystem.Client.Design.Model.Info;
using StockTradingSystem.Client.Design.Model.UI.Dialog;
using StockTradingSystem.Client.Model;
using StockTradingSystem.Client.Model.Access;
using StockTradingSystem.Client.Model.Business;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Client.Model.UI.Dialog;
using StockTradingSystem.Client.Model.UI.Navigation;
using StockTradingSystem.Client.ViewModel.Control;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Business;
using StockTradingSystem.Core.Model;

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
            SimpleIoc.Default.Reset();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDialogService, DesignDialogService>();
                SimpleIoc.Default.Register<IUserAccess, DesignGpUserAccess>();
                SimpleIoc.Default.Register<IBusiness, DesignGpBusiness>();
                SimpleIoc.Default.Register<IUser, DesignGpUser>();
                SimpleIoc.Default.Register<StockAgent, DesignGpStockAgent>();
                SimpleIoc.Default.Register<UserMoneyInfo, DesignUserMoneyInfo>();
            }
            else
            {
                SimpleIoc.Default.Register<IDialogService, DialogService>();
                SimpleIoc.Default.Register<IUserAccess, GpUserAccess>();
                SimpleIoc.Default.Register<IBusiness, GpBusiness>();
                SimpleIoc.Default.Register<IUser, GpUser>();
                SimpleIoc.Default.Register<StockAgent, GpStockAgent>();
                SimpleIoc.Default.Register<UserMoneyInfo>();
            }

            InitNavigation();

            SimpleIoc.Default.Register<MainWindowModel>();
            SimpleIoc.Default.Register<AccountButtonViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<StockViewModel>();
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<StockInfoViewModel>();
            SimpleIoc.Default.Register<UserStockInfoViewModel>(true);
        }

        /// <summary>
        /// Gets the <see cref="FrameNavigationService"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public IFrameNavigationService FrameNavigationService => ServiceLocator.Current.GetInstance<IFrameNavigationService>();

        /// <summary>
        /// Gets the <see cref="StockAgent"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public StockAgent StockAgent => ServiceLocator.Current.GetInstance<StockAgent>();

        /// <summary>
        /// Gets the <see cref="UserMoneyInfo"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UserMoneyInfo UserMoneyInfo => ServiceLocator.Current.GetInstance<UserMoneyInfo>();

        /// <summary>
        /// Gets the <see cref="StockInfoViewModel"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public StockInfoViewModel StockInfoViewModel => ServiceLocator.Current.GetInstance<StockInfoViewModel>();

        /// <summary>
        /// Gets the <see cref="UserStockInfoViewModel"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UserStockInfoViewModel UserStockInfoViewModel => ServiceLocator.Current.GetInstance<UserStockInfoViewModel>();

        /// <summary>
        /// Gets the <see cref="Main"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainWindowModel Main => ServiceLocator.Current.GetInstance<MainWindowModel>();

        /// <summary>
        /// Gets the <see cref="AccountBtn"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AccountButtonViewModel AccountBtn => ServiceLocator.Current.GetInstance<AccountButtonViewModel>();

        /// <summary>
        /// Gets the <see cref="Login"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();

        /// <summary>
        /// Gets the <see cref="Register"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public RegisterViewModel Register => ServiceLocator.Current.GetInstance<RegisterViewModel>();

        /// <summary>
        /// Gets the <see cref="Stock"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public StockViewModel Stock => ServiceLocator.Current.GetInstance<StockViewModel>();

        /// <summary>
        /// Gets the <see cref="Account"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AccountViewModel Account => ServiceLocator.Current.GetInstance<AccountViewModel>();

        /// <summary>
        /// Gets the <see cref="Settings"/> property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel Settings => ServiceLocator.Current.GetInstance<SettingsViewModel>();

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

        }
    }
}