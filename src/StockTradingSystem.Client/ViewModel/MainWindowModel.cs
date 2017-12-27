using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Converters;
using System.Windows.Media;
using System.Windows.Media.Effects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.UI.Navigation;

namespace StockTradingSystem.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the <see cref="MainWindow"/> can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainWindowModel : ViewModelBase
    {
        public static readonly string ShowDialog = "DialogServiceShowDialog";
        public static readonly string FirstView = "NavigateToFirstView";

        private const string TitleBtnViews = "StockView#TradeView#AccountView";

        private readonly IFrameNavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private bool _titleBtnState = true;

        public MainWindowModel(IFrameNavigationService navigationService, IDialogService dialogService)
        {
            try
            {
                var themeColors = Array.ConvertAll(ConfigurationManager.AppSettings["ThemeColorRGB"].Split('#'), Convert.ToByte);
                ThemeBrush = new SolidColorBrush(Color.FromArgb(255, themeColors[0], themeColors[1], themeColors[2]));
            }
            catch
            {
                ThemeBrush = new SolidColorBrush(Color.FromArgb(255, 0, 99, 177));
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            Messenger.Default.Register<GenericMessage<bool>>(this, ShowDialog, b =>
            {
                IsEnabledWithDialog = !b.Content;
                MainGridEffect = b.Content
                    ? new BlurEffect { Radius = 17, RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian }
                    : null;
            });
            Messenger.Default.Register<GenericMessage<string>>(this, FirstView, v =>
            {
                _navigationService.NavigateTo(v.Content);
                SyncTitleBarState();
            });
        }

        private void SyncTitleBarState()
        {
            TitleBtnIsEnabled = TitleBtnViews.Split('#').ToList().Contains(_navigationService.CurrentPageKey);
            TitleBtnIsChecked = TitleBtnIsEnabled && _titleBtnState;
        }

        #region Property

        /// <summary>
        /// The <see cref="WindowState" /> property's name.
        /// </summary>
        public const string WindowStatePropertyName = nameof(WindowState);

        private WindowState _windowState = WindowState.Normal;

        /// <summary>
        /// Sets and gets the <see cref="WindowState"/> property.
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
        public const string WindowBorderMarginPropertyName = nameof(WindowBorderMargin);

        private Thickness _windowBorderMargin = new Thickness(0);

        /// <summary>
        /// Sets and gets the <see cref="WindowBorderMargin"/> property.
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
        public const string WindowTitlePropertyName = nameof(WindowTitle);

        private string _windowTitle = "股票交易系统";

        /// <summary>
        /// Sets and gets the <see cref="WindowTitle"/> property.
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
        public const string BackBtnVisibilityPropertyName = nameof(BackBtnVisibility);

        private Visibility _backBtnVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the <see cref="BackBtnVisibility"/> property.
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
        public const string ThemeBrushPropertyName = nameof(ThemeBrush);

        private SolidColorBrush _themeBrush;

        /// <summary>
        /// Sets and gets the <see cref="ThemeBrush"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SolidColorBrush ThemeBrush
        {
            get => _themeBrush;
            set => Set(ThemeBrushPropertyName, ref _themeBrush, value);
        }

        /// <summary>
        /// The <see cref="IsEnabledWithDialog" /> property's name.
        /// </summary>
        public const string BackBtnEnabledPropertyName = nameof(IsEnabledWithDialog);

        private bool _isEnabledWithDialog = true;

        /// <summary>
        /// Sets and gets the <see cref="IsEnabledWithDialog"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool IsEnabledWithDialog
        {
            get => _isEnabledWithDialog;
            set => Set(BackBtnEnabledPropertyName, ref _isEnabledWithDialog, value, true);
        }

        /// <summary>
        /// The <see cref="MainGridEffect" /> property's name.
        /// </summary>
        public const string MainFrameEffectPropertyName = nameof(MainGridEffect);

        private Effect _mainGridEffect;

        /// <summary>
        /// Sets and gets the <see cref="MainGridEffect"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Effect MainGridEffect
        {
            get => _mainGridEffect;
            set => Set(MainFrameEffectPropertyName, ref _mainGridEffect, value, true);
        }

        /// <summary>
        /// The <see cref="TitleBtnIsEnabled" /> property's name.
        /// </summary>
        public const string TitleBtnIsEnabledPropertyName = nameof(TitleBtnIsEnabled);

        private bool _titleBtnIsEnabled;

        /// <summary>
        /// Sets and gets the <see cref="TitleBtnIsEnabled"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool TitleBtnIsEnabled
        {
            get => _titleBtnIsEnabled;
            set => Set(TitleBtnIsEnabledPropertyName, ref _titleBtnIsEnabled, value, true);
        }

        /// <summary>
        /// The <see cref="TitleBtnIsChecked" /> property's name.
        /// </summary>
        public const string TitleBtnIsCheckedPropertyName = nameof(TitleBtnIsChecked);

        private bool _titleBtnIsChecked;

        /// <summary>
        /// Sets and gets the <see cref="TitleBtnIsChecked"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool TitleBtnIsChecked
        {
            get => _titleBtnIsChecked;
            set => Set(TitleBtnIsCheckedPropertyName, ref _titleBtnIsChecked, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _minimizeCommand;

        /// <summary>
        /// Gets the <see cref="MinimizeCommand"/>.
        /// </summary>
        public RelayCommand MinimizeCommand => _minimizeCommand ?? (_minimizeCommand = new RelayCommand(ExecuteMinimize));

        private void ExecuteMinimize()
        {
            WindowState = WindowState.Minimized;
        }

        private RelayCommand _closeCommand;

        /// <summary>
        /// Gets the <see cref="CloseCommand"/>.
        /// </summary>
        public RelayCommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(ExecuteClose));

        private async void ExecuteClose()
        {
            await _dialogService.ShowMessage("确定要退出吗？", "提示", "确定", "取消", b =>
            {
                if (b) Application.Current.Shutdown();
            });
        }

        private RelayCommand _maximizeCommand;

        /// <summary>
        /// Gets the <see cref="MaximizeCommand"/>.
        /// </summary>
        public RelayCommand MaximizeCommand => _maximizeCommand ?? (_maximizeCommand = new RelayCommand(ExecuteMaximize));

        private void ExecuteMaximize()
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private RelayCommand<string> _navigateCommand;

        /// <summary>
        /// Gets the <see cref="NavigateCommand"/>.
        /// </summary>
        public RelayCommand<string> NavigateCommand => _navigateCommand
                                                       ?? (_navigateCommand = new RelayCommand<string>(ExecuteNavigateCommand));

        private void ExecuteNavigateCommand(string pageKey)
        {
            _navigationService.NavigateTo(pageKey);
            BackBtnVisibility = _navigationService.CanBack() ? Visibility.Visible : Visibility.Collapsed;
            SyncTitleBarState();
        }

        private RelayCommand _goBackCommand;

        /// <summary>
        /// Gets the <see cref="GoBackCommand"/>.
        /// </summary>
        public RelayCommand GoBackCommand => _goBackCommand
                                             ?? (_goBackCommand = new RelayCommand(ExecuteGoBackCommand));

        private void ExecuteGoBackCommand()
        {
            _navigationService.GoBack();
            BackBtnVisibility = _navigationService.CanBack() ? Visibility.Visible : Visibility.Collapsed;
            SyncTitleBarState();
        }

        private RelayCommand _titleBtnCommand;

        /// <summary>
        /// Gets the <see cref="TitleBtnCommand"/>.
        /// </summary>
        public RelayCommand TitleBtnCommand => _titleBtnCommand
                                               ?? (_titleBtnCommand = new RelayCommand(ExecuteTitleBtnCommand));

        private void ExecuteTitleBtnCommand()
        {
            _titleBtnState = !_titleBtnState;
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