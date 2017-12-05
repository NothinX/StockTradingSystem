using System;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using StockTradingSystem.Client.UI.Navigation;

namespace StockTradingSystem.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public static readonly string Canback = "FrameNavigationServiceCanBack";

        private readonly IFrameNavigationService _navigationService;

        public MainViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
            Messenger.Default.Register<GenericMessage<bool>>(this, Canback, b => BackBtnVisibility = b.Content ? Visibility.Visible : Visibility.Collapsed);
        }

        #region Property

        /// <summary>
        /// The <see cref="WindowState" /> property's name.
        /// </summary>
        public const string WindowStatePropertyName = "WindowState";

        private WindowState _windowState = WindowState.Normal;

        /// <summary>
        /// Sets and gets the MainWindowState property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                switch (value)
                {
                    case WindowState.Maximized:
                        WindowBorderMargin = new Thickness(7);
                        break;
                    case WindowState.Normal:
                        WindowBorderMargin = new Thickness(0);
                        break;
                    case WindowState.Minimized:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                Set(WindowStatePropertyName, ref _windowState, value);
            }
        }

        /// <summary>
        /// The <see cref="WindowBorderMargin" /> property's name.
        /// </summary>
        public const string WindowBorderMarginPropertyName = "WindowBorderMargin";

        private Thickness _windowBorderMargin = new Thickness(0);

        /// <summary>
        /// Sets and gets the WindowBorderMargin property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Thickness WindowBorderMargin
        {
            get => _windowBorderMargin;
            set => Set(WindowBorderMarginPropertyName, ref _windowBorderMargin, value);
        }

        /// <summary>
        /// The <see cref="WindowTitle" /> property's name.
        /// </summary>
        public const string WindowTitlePropertyName = "WindowTitle";

        private string _windowTitle = "股票交易系统";

        /// <summary>
        /// Sets and gets the WindowTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WindowTitle
        {
            get => _windowTitle;
            set => Set(WindowTitlePropertyName, ref _windowTitle, value);
        }

        /// <summary>
        /// The <see cref="BackBtnVisibility" /> property's name.
        /// </summary>
        public const string BackBtnVisibilityPropertyName = "BackBtnVisibility";

        private Visibility _backBtnVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the WindowTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility BackBtnVisibility
        {
            get => _backBtnVisibility;
            set => Set(BackBtnVisibilityPropertyName, ref _backBtnVisibility, value);
        }

        /// <summary>
        /// The <see cref="ThemeBrush" /> property's name.
        /// </summary>
        public const string ThemeBrushPropertyName = "ThemeBrush";

        private SolidColorBrush _themeBrush = new SolidColorBrush(Color.FromArgb(255, 0, 99, 177));

        /// <summary>
        /// Sets and gets the ThemeBrush property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SolidColorBrush ThemeBrush
        {
            get => _themeBrush;
            set => Set(ThemeBrushPropertyName, ref _themeBrush, value);
        }

        /// <summary>
        /// The <see cref="WindowContent" /> property's name.
        /// </summary>
        public const string ContentPropertyName = "WindowContent";

        private object _windowContent;

        /// <summary>
        /// Sets and gets the Content property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public object WindowContent
        {
            get => _windowContent;
            set => Set(ContentPropertyName, ref _windowContent, value);
        }

        #endregion

        #region Command

        private RelayCommand _minimize;

        /// <summary>
        /// Gets the Minimize.
        /// </summary>
        public RelayCommand Minimize => _minimize ?? (_minimize = new RelayCommand(ExecuteMinimize));

        private void ExecuteMinimize()
        {
            WindowState = WindowState.Minimized;
        }

        private RelayCommand _close;

        /// <summary>
        /// Gets the Close.
        /// </summary>
        public RelayCommand Close => _close ?? (_close = new RelayCommand(ExecuteClose));

        private static void ExecuteClose()
        {
            Application.Current.Shutdown();
        }

        private RelayCommand _maximize;

        /// <summary>
        /// Gets the Maximize.
        /// </summary>
        public RelayCommand Maximize => _maximize ?? (_maximize = new RelayCommand(ExecuteMaximize));

        private void ExecuteMaximize()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private RelayCommand<string> _navigateCommand;

        /// <summary>
        /// Gets the NavigateCommand.
        /// </summary>
        public RelayCommand<string> NavigateCommand => _navigateCommand
                                                       ?? (_navigateCommand = new RelayCommand<string>(ExecuteNavigateCommand));

        private void ExecuteNavigateCommand(string pageKey)
        {
            SimpleIoc.Default.GetInstance<IFrameNavigationService>()?.NavigateTo(pageKey);
        }

        private RelayCommand _goBackCommand;

        /// <summary>
        /// Gets the GoBackCommand.
        /// </summary>
        public RelayCommand GoBackCommand => _goBackCommand
                                             ?? (_goBackCommand = new RelayCommand(ExecuteGoBackCommand));

        private void ExecuteGoBackCommand()
        {
            _navigationService.GoBack();
        }

        #endregion

        public override void Cleanup()
        {
            // Clean up if needed
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }
    }
}