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
using static StockTradingSystem.Client.App;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class TransactionRecentInfoViewModel : ViewModelBase, IDisposable
    {
        public static readonly string UpdateTransactionRecentInfo = "UpdateTransactionRecentInfo";

        private const int RecentNum = 100;

        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;
        private readonly StockInfoViewModel _stockInfoViewModel;

        private Task _updateTransactionRecentInfo;
        private CancellationTokenSource _cts;

        public TransactionRecentInfoViewModel(StockAgent stockAgent, IDialogService dialogService, StockInfoViewModel stockInfoViewModel)
        {
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            _stockInfoViewModel = stockInfoViewModel;
            Messenger.Default.Register<GenericMessage<bool>>(this, UpdateTransactionRecentInfo, b =>
            {
                lock (this)
                {
                    if (b.Content && _updateTransactionRecentInfo == null)
                    {
                        _cts = new CancellationTokenSource();
                        _updateTransactionRecentInfo = Update(_cts.Token);
                    }
                    else if (!b.Content && _cts != null && _updateTransactionRecentInfo != null)
                    {
                        _cts.Cancel();
                        _updateTransactionRecentInfo = null;
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
                    var addlist = new List<TransactionRecentInfo>();
                    var deletelist = new List<TransactionRecentInfo>();
                    var s = _stockAgent.GetRecentTrans(_stockInfoViewModel.CurrentStockInfo.StockId, RecentNum);
                    lock (this)
                    {
                        s.ForEach(x =>
                        {
                            var ss = TransactionRecentInfoList.FirstOrDefault(y => y.TranId == x.TranId);
                            if (ss != null) return;
                            var si = new TransactionRecentInfo();
                            si.Create(x);
                            addlist.Add(si);
                        });
                        TransactionRecentInfoList.ForEach(x =>
                        {
                            var ss = s.FirstOrDefault(y => y.TranId == x.TranId);
                            if (ss == null) deletelist.Add(x);
                        });
                        addlist.ForEach(x => TransactionRecentInfoList.Add(x));
                        deletelist.ForEach(x => TransactionRecentInfoList.Remove(x));
                        TransactionRecentInfoList = TransactionRecentInfoList.OrderBy(x => x.CreateDateTime).ToList();
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
        /// The <see cref="TransactionRecentInfoList" /> property's name.
        /// </summary>
        public const string BuyStockDepthInfoListPropertyName = nameof(TransactionRecentInfoList);

        private List<TransactionRecentInfo> _transactionRecentInfoList = new List<TransactionRecentInfo>();

        /// <summary>
        /// Sets and gets the <see cref="TransactionRecentInfoList"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public List<TransactionRecentInfo> TransactionRecentInfoList
        {
            get => _transactionRecentInfoList;
            set => Set(BuyStockDepthInfoListPropertyName, ref _transactionRecentInfoList, value, true);
        }

        public void Dispose()
        {
            _stockInfoViewModel?.Dispose();
            _updateTransactionRecentInfo?.Dispose();
            _cts?.Dispose();
        }
    }
}