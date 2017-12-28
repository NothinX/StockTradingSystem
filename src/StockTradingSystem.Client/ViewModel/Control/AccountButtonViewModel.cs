using System;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Client.Model.UI.Navigation;
using StockTradingSystem.Core.Model;
using static StockTradingSystem.Client.App;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public sealed class AccountButtonViewModel : ViewModelBase, IDisposable
    {
        public static readonly string UpdateUserMoneyInfo = "UpdateUserMoneyInfo";

        private readonly MainWindowModel _mainWindowModel;
        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;
        private readonly UserMoneyInfo _userMoneyInfo;
        private readonly IFrameNavigationService _frameNavigationService;

        private Task _updateUserMoneyInfo;
        private CancellationTokenSource _cts;

        public AccountButtonViewModel(MainWindowModel mainWindowModel, StockAgent stockAgent, IDialogService dialogService, UserMoneyInfo userMoneyInfo, IFrameNavigationService frameNavigationService)
        {
            _mainWindowModel = mainWindowModel;
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            _userMoneyInfo = userMoneyInfo;
            _frameNavigationService = frameNavigationService;
            Messenger.Default.Register<GenericMessage<bool>>(this, UpdateUserMoneyInfo, b =>
            {
                lock (this)
                {
                    if (b.Content && _updateUserMoneyInfo == null)
                    {
                        _cts = new CancellationTokenSource();
                        _updateUserMoneyInfo = Update(_cts.Token);
                    }
                    else if (!b.Content && _cts != null && _updateUserMoneyInfo != null)
                    {
                        _cts.Cancel();
                        _updateUserMoneyInfo = null;
                    }
                }
            });
        }

        private async Task Update(CancellationToken ct)
        {
            var t = RefreshTimeSpan;
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var ucr = _stockAgent.User_cny();
                    lock (this)
                    {
                        TotalMoneyText = $"总共：{ucr.CnyFree + ucr.CnyFreezed:F2}";
                        AvailableMoneyText = $"可用：{ucr.CnyFree:F2}";
                    }
                    _userMoneyInfo.Update(ucr);
                    await Task.Delay(t, ct);
                }
            }
            catch (Exception e)
            {
                if (!ct.IsCancellationRequested) await _dialogService.ShowError(e, "错误", "确定", null);
            }
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
                if (!b) return;
                _stockAgent.User.IsLogin = false;
                Messenger.Default.Send(new GenericMessage<bool>(false), UpdateUserMoneyInfo);
                Messenger.Default.Send(new GenericMessage<bool>(false), UserStockInfoViewModel.UpdateUserStockInfo);
                Messenger.Default.Send(new GenericMessage<bool>(false), UserOrderInfoViewModel.UpdateUserOrderInfo);
                if (_frameNavigationService.CurrentPageKey == "AccountView") _mainWindowModel.GoBackCommand.Execute(null);
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
                if (!b) return;
                _stockAgent.User.IsLogin = false;
                Messenger.Default.Send(new GenericMessage<bool>(false), UpdateUserMoneyInfo);
                Messenger.Default.Send(new GenericMessage<bool>(false), UserStockInfoViewModel.UpdateUserStockInfo);
                Messenger.Default.Send(new GenericMessage<bool>(false), UserOrderInfoViewModel.UpdateUserOrderInfo);
                _mainWindowModel.NavigateCommand.Execute("LoginView");
            });
        }

        #endregion

        public void Dispose()
        {
            _cts?.Cancel();
            _updateUserMoneyInfo?.Dispose();
            _cts?.Dispose();
        }
    }
}