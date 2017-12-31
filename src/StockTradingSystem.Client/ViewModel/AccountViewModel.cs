using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Client.ViewModel.Control;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        public static readonly string UpdateCurrentUserStockInfoToken = "UpdateCurrentUserStockInfoToken";

        private readonly UserStockInfoViewModel _userStockInfoViewModel;
        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;

        public AccountViewModel(UserStockInfoViewModel userStockInfoViewModel, StockAgent stockAgent, IDialogService dialogService)
        {
            _userStockInfoViewModel = userStockInfoViewModel;
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            Messenger.Default.Register<GenericMessage<int?>>(this, UpdateCurrentUserStockInfoToken, i => UpdateCurrentUserStockInfo(i.Content));
        }

        public void UpdateCurrentUserStockInfo(int? sid)
        {
            lock (this)
            {
                if (sid == null) return;
                var usi = _userStockInfoViewModel.UserStockInfoList.FirstOrDefault(x => x.StockId == sid);
                if (usi != null) Set(CurrentUserStockInfoPropertyName, ref _currentUserStockInfo, usi, true);
            }
        }

        #region Property

        /// <summary>
        /// The <see cref="CurrentUserStockInfo" /> property's name.
        /// </summary>
        public const string CurrentUserStockInfoPropertyName = nameof(CurrentUserStockInfo);

        private UserStockInfo _currentUserStockInfo;

        /// <summary>
        /// Sets and gets the <see cref="CurrentUserStockInfo"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public UserStockInfo CurrentUserStockInfo
        {
            get { return _currentUserStockInfo; }
            set
            {
                Set(CurrentUserStockInfoPropertyName, ref _currentUserStockInfo, value, true);
                Messenger.Default.Send(new GenericMessage<int>(CurrentUserStockInfo.StockId), StockInfoViewModel.UpdateCurrentStockInfoToken);
            }
        }

        /// <summary>
        /// The <see cref="CurrentUserOrderInfo" /> property's name.
        /// </summary>
        public const string CurrentUserOrderInfoPropertyName = nameof(CurrentUserOrderInfo);

        private UserOrderInfo _currentUserOrderInfo;

        /// <summary>
        /// Sets and gets the <see cref="CurrentUserOrderInfo"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public UserOrderInfo CurrentUserOrderInfo
        {
            get { return _currentUserOrderInfo; }
            set { Set(CurrentUserOrderInfoPropertyName, ref _currentUserOrderInfo, value, true); }
        }

        #endregion

        #region Command

        private RelayCommand<string> _cancelOrderCommand;

        /// <summary>
        /// Gets the <see cref="CancelOrderCommand"/>.
        /// </summary>
        public RelayCommand<string> CancelOrderCommand => _cancelOrderCommand
                                                  ?? (_cancelOrderCommand = new RelayCommand<string>(ExecuteCancelOrderCommand));

        private async void ExecuteCancelOrderCommand(string orderId)
        {
            await _dialogService.ShowMessage("确定要取消吗？", "提示", "确定", "取消", b =>
            {
                if (b)
                {
                    _stockAgent.Cancel_Order(Convert.ToInt64(orderId));
                }
            });
        }

        #endregion

    }
}