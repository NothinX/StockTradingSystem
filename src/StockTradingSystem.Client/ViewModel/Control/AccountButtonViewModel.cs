using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class AccountButtonViewModel : ViewModelBase
    {
        public static readonly string UpdateTotalMoney = "UpdateTotalMoney";
        public static readonly string UpdateAvailableMoney = "UpdateAvailableMoney";

        private readonly MainWindowModel _mainWindowModel;
        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;

        public AccountButtonViewModel(MainWindowModel mainWindowModel, StockAgent stockAgent, IDialogService dialogService)
        {
            _mainWindowModel = mainWindowModel;
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            Messenger.Default.Register<GenericMessage<decimal>>(this, UpdateTotalMoney, d =>
            {
                lock (this)
                {
                    TotalMoneyText = $"总共：{d.Content:F2}";
                }
            });
            Messenger.Default.Register<GenericMessage<decimal>>(this, UpdateAvailableMoney, d =>
            {
                lock (this)
                {
                    AvailableMoneyText = $"可用：{d.Content:F2}";
                }
            });
        }

        #region Property

        /// <summary>
        /// The <see cref="TotalMoneyText" /> property's name.
        /// </summary>
        public const string TotalMoneyTextPropertyName = nameof(TotalMoneyText);

        private string _totalMoneyText = "总共：";

        /// <summary>
        /// Sets and gets the <see cref="TotalMoneyText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string TotalMoneyText
        {
            get => _totalMoneyText;
            set => Set(TotalMoneyTextPropertyName, ref _totalMoneyText, value, true);
        }

        /// <summary>
        /// The <see cref="AvailableMoneyText" /> property's name.
        /// </summary>
        public const string AvailableMoneyTextPropertyName = nameof(AvailableMoneyText);

        private string _availableMoneyText = "可用：";

        /// <summary>
        /// Sets and gets the <see cref="AvailableMoneyText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string AvailableMoneyText
        {
            get => _availableMoneyText;
            set => Set(AvailableMoneyTextPropertyName, ref _availableMoneyText, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _logoutCommand;

        /// <summary>
        /// Gets the <see cref="LogoutCommand"/>.
        /// </summary>
        public RelayCommand LogoutCommand => _logoutCommand
                                             ?? (_logoutCommand = new RelayCommand(ExecuteLogoutCommand));

        private async void ExecuteLogoutCommand()
        {
            await _dialogService.ShowMessage("确定要注销账号吗？", "提示", "确定", "取消", b =>
            {
                if (b)
                {
                    _stockAgent.User.IsLogin = false;
                }
            });
        }

        private RelayCommand _switchCommand;

        /// <summary>
        /// Gets the <see cref="SwitchCommand"/>.
        /// </summary>
        public RelayCommand SwitchCommand => _switchCommand
                                             ?? (_switchCommand = new RelayCommand(ExecuteSwitchCommand));

        private async void ExecuteSwitchCommand()
        {
            await _dialogService.ShowMessage("确定要切换账号吗？", "提示", "确定", "取消", b =>
            {
                if (b)
                {
                    _stockAgent.User.IsLogin = false;
                    _mainWindowModel.NavigateCommand.Execute("LoginView");
                }
            });
        }

        #endregion
    }
}