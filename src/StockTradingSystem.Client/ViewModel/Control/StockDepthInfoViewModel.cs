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
    public class StockDepthInfoViewModel : ViewModelBase, IDisposable
    {
        public static readonly string UpdateStockDepthInfo = "UpdateStockDepthInfo";

        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;
        private readonly StockInfoViewModel _stockInfoViewModel;

        private Task _updateBuyStockDepthInfo;
        private Task _updateSellStockDepthInfo;
        private CancellationTokenSource _cts;

        public StockDepthInfoViewModel(StockAgent stockAgent, IDialogService dialogService, StockInfoViewModel stockInfoViewModel)
        {
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            _stockInfoViewModel = stockInfoViewModel;
            Messenger.Default.Register<GenericMessage<bool>>(this, UpdateStockDepthInfo, b =>
            {
                lock (this)
                {
                    if (b.Content && (_updateBuyStockDepthInfo == null || _updateSellStockDepthInfo == null))
                    {
                        _cts = new CancellationTokenSource();
                        _updateBuyStockDepthInfo = UpdateBuy(_cts.Token);
                        _updateSellStockDepthInfo = UpdateSell(_cts.Token);
                    }
                    else if (!b.Content && _cts != null && (_updateBuyStockDepthInfo != null || _updateSellStockDepthInfo != null))
                    {
                        _cts.Cancel();
                        _updateBuyStockDepthInfo = null;
                        _updateSellStockDepthInfo = null;
                        BuyStockDepthInfoList.Clear();
                        SellStockDepthInfoList.Clear();
                    }
                }
            });
        }

        private async Task UpdateBuy(CancellationToken ct)
        {
            var t = RefreshTimeSpan;
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var addlist = new List<StockDepthInfo>();
                    var deletelist = new List<StockDepthInfo>();
                    var s = _stockAgent.Stock_depth(_stockInfoViewModel.CurrentStockInfo.StockId, 0);
                    lock (this)
                    {
                        s.ForEach(x =>
                        {
                            var ss = BuyStockDepthInfoList.FirstOrDefault(y => y.Price == x.Price);
                            if (ss != null) ss.Update(x);
                            else
                            {
                                var si = new StockDepthInfo();
                                si.Create(x);
                                addlist.Add(si);
                            }
                        });
                        BuyStockDepthInfoList.ForEach(x =>
                        {
                            var ss = s.FirstOrDefault(y => y.Price == x.Price);
                            if (ss == null) deletelist.Add(x);
                        });
                        addlist.ForEach(x => BuyStockDepthInfoList.Add(x));
                        deletelist.ForEach(x => BuyStockDepthInfoList.Remove(x));
                        BuyStockDepthInfoList = SortStockDepthInfoList(BuyStockDepthInfoList);
                    }
                    await Task.Delay(t, ct);
                }
            }
            catch (Exception e)
            {
                if (!ct.IsCancellationRequested) await _dialogService.ShowError(e, "错误", "确定", null);
            }
        }

        private async Task UpdateSell(CancellationToken ct)
        {
            var t = RefreshTimeSpan;
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var addlist = new List<StockDepthInfo>();
                    var deletelist = new List<StockDepthInfo>();
                    var s = _stockAgent.Stock_depth(_stockInfoViewModel.CurrentStockInfo.StockId, 1);
                    lock (this)
                    {
                        s.ForEach(x =>
                        {
                            var ss = SellStockDepthInfoList.FirstOrDefault(y => y.Price == x.Price);
                            if (ss != null) ss.Update(x);
                            else
                            {
                                var si = new StockDepthInfo();
                                si.Create(x);
                                addlist.Add(si);
                            }
                        });
                        SellStockDepthInfoList.ForEach(x =>
                        {
                            var ss = s.FirstOrDefault(y => y.Price == x.Price);
                            if (ss == null) deletelist.Add(x);
                        });
                        addlist.ForEach(x => SellStockDepthInfoList.Add(x));
                        deletelist.ForEach(x => SellStockDepthInfoList.Remove(x));
                        SellStockDepthInfoList = SortStockDepthInfoList(SellStockDepthInfoList);
                    }
                    await Task.Delay(t, ct);
                }
            }
            catch (Exception e)
            {
                if (!ct.IsCancellationRequested) await _dialogService.ShowError(e, "错误", "确定", null);
            }
        }

        private static List<StockDepthInfo> SortStockDepthInfoList(IEnumerable<StockDepthInfo> sdil)
        {
            return sdil.OrderByDescending(s => s.Price).ToList();
        }

        /// <summary>
        /// The <see cref="BuyStockDepthInfoList" /> property's name.
        /// </summary>
        public const string BuyStockDepthInfoListPropertyName = nameof(BuyStockDepthInfoList);

        private List<StockDepthInfo> _buyStockDepthInfoList = new List<StockDepthInfo>();

        /// <summary>
        /// Sets and gets the <see cref="BuyStockDepthInfoList"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public List<StockDepthInfo> BuyStockDepthInfoList
        {
            get => _buyStockDepthInfoList;
            set => Set(BuyStockDepthInfoListPropertyName, ref _buyStockDepthInfoList, value, true);
        }

        /// <summary>
        /// The <see cref="SellStockDepthInfoList" /> property's name.
        /// </summary>
        public const string SellStockDepthInfoListPropertyName = nameof(SellStockDepthInfoList);

        private List<StockDepthInfo> _sellStockDepthInfoList = new List<StockDepthInfo>();

        /// <summary>
        /// Sets and gets the <see cref="SellStockDepthInfoList"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public List<StockDepthInfo> SellStockDepthInfoList
        {
            get => _sellStockDepthInfoList;
            set => Set(SellStockDepthInfoListPropertyName, ref _sellStockDepthInfoList, value, true);
        }

        public void Dispose()
        {
            _stockInfoViewModel?.Dispose();
            _updateBuyStockDepthInfo?.Dispose();
            _updateSellStockDepthInfo?.Dispose();
            _cts?.Dispose();
        }
    }
}