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
    public class UserStockInfoViewModel : ViewModelBase, IDisposable
    {
        public static readonly string UpdateUserStockInfo = "UpdateUserStockInfo";

        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;
        private readonly StockInfoViewModel _stockInfoViewModel;

        private Task _updateUserStockInfo;
        private CancellationTokenSource _cts;

        public UserStockInfoViewModel(StockAgent stockAgent, IDialogService dialogService, StockInfoViewModel stockInfoViewModel)
        {
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            _stockInfoViewModel = stockInfoViewModel;
            Messenger.Default.Register<GenericMessage<bool>>(this, UpdateUserStockInfo, b =>
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
            var t = new TimeSpan(0, 0, 5);
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var addlist = new List<UserStockInfo>();
                    var deletelist = new List<UserStockInfo>();
                    var s = _stockAgent.User_stock();
                    lock (this)
                    {
                        s.ForEach(x =>
                        {
                            var ss = UserStockList.FirstOrDefault(y => y.StockId == x.StockId);
                            if (ss != null) ss.Update(x);
                            else
                            {
                                var si = new UserStockInfo
                                {
                                    StockInfo = _stockInfoViewModel.StockInfoList.First(y => y.StockId == x.StockId)
                                };
                                si.Update(x);
                                addlist.Add(si);
                            }
                        });
                        UserStockList.ForEach(x =>
                        {
                            var ss = s.FirstOrDefault(y => y.StockId == x.StockId);
                            if (ss == null) deletelist.Add(x);
                        });
                        addlist.ForEach(x => UserStockList.Add(x));
                        deletelist.ForEach(x => UserStockList.Remove(x));
                        UserStockList = UserStockList.OrderBy(x => x.StockId).ToList();
                    }
                    await Task.Delay(t, ct);
                }
            }
            catch (Exception e)
            {
                await _dialogService.ShowError(e, "错误", "确定", null);
            }
        }

        /// <summary>
        /// The <see cref="UserStockList" /> property's name.
        /// </summary>
        public const string UserStockListPropertyName = nameof(UserStockList);

        private List<UserStockInfo> _userStockList = new List<UserStockInfo>();

        /// <summary>
        /// Sets and gets the <see cref="UserStockList"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public List<UserStockInfo> UserStockList
        {
            get => _userStockList;
            set => Set(UserStockListPropertyName, ref _userStockList, value, true);
        }

        public void Dispose()
        {
            _updateUserStockInfo?.Dispose();
            _cts?.Dispose();
        }
    }
}