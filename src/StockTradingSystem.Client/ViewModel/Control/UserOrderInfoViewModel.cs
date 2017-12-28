using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class UserOrderInfoViewModel : ViewModelBase, IDisposable
    {
        public static readonly string UpdateUserOrderInfo = "UpdateUserOrderInfo";

        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;
        private readonly StockInfoViewModel _stockInfoViewModel;

        private Task _updateUserStockInfo;
        private CancellationTokenSource _cts;

        public UserOrderInfoViewModel(StockAgent stockAgent, IDialogService dialogService, StockInfoViewModel stockInfoViewModel)
        {
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            _stockInfoViewModel = stockInfoViewModel;
            Messenger.Default.Register<GenericMessage<bool>>(this, UpdateUserOrderInfo, b =>
            {
                lock (this)
                {
                    if (b.Content && _updateUserStockInfo == null)
                    {
                        _cts = new CancellationTokenSource();
                        _updateUserStockInfo = Update(_cts.Token);
                    }
                    else if (!b.Content && _cts != null && _updateUserStockInfo != null)
                    {
                        _cts.Cancel();
                        _updateUserStockInfo = null;
                    }
                }
            });
        }

        private async Task Update(CancellationToken ct)
        {
            var t = new TimeSpan(0, 0, 3);
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var addlist = new List<UserOrderInfo>();
                    var deletelist = new List<UserOrderInfo>();
                    var s = _stockAgent.User_order();
                    lock (this)
                    {
                        s.ForEach(x =>
                        {
                            var ss = UserOrderInfoList.FirstOrDefault(y => y.OrderId == x.OrderId);
                            if (ss != null) ss.Update(x);
                            else
                            {
                                var si = new UserOrderInfo
                                {
                                    StockInfo = _stockInfoViewModel.StockInfoList.First(y => y.StockId == x.StockId)
                                };
                                si.Create(x);
                                addlist.Add(si);
                            }
                        });
                        UserOrderInfoList.ForEach(x =>
                        {
                            var ss = s.FirstOrDefault(y => y.OrderId == x.OrderId);
                            if (ss == null) deletelist.Add(x);
                        });
                        addlist.ForEach(x => UserOrderInfoList.Add(x));
                        deletelist.ForEach(x => UserOrderInfoList.Remove(x));
                        UserOrderInfoList = UserOrderInfoList.OrderBy(x => x.OrderId).ToList();
                    }
                    await Task.Delay(t, ct);
                }
            }
            catch (Exception e)
            {
                if (!ct.IsCancellationRequested) await _dialogService.ShowError(e, "错误", "确定", null);
            }
        }

        /// <summary>
        /// The <see cref="UserOrderInfoList" /> property's name.
        /// </summary>
        public const string UserOrderInfoListPropertyName = nameof(UserOrderInfoList);

        private List<UserOrderInfo> _userStockOrderList = new List<UserOrderInfo>();

        /// <summary>
        /// Sets and gets the <see cref="UserOrderInfoList"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public List<UserOrderInfo> UserOrderInfoList
        {
            get => _userStockOrderList;
            set => Set(UserOrderInfoListPropertyName, ref _userStockOrderList, value, true);
        }

        public void Dispose()
        {
            _updateUserStockInfo?.Dispose();
            _cts?.Dispose();
        }
    }
}