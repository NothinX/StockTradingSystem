using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Core.Model;
using static StockTradingSystem.Client.App;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class StockInfoViewModel : ViewModelBase, IDisposable
    {
        public static readonly string UpdateCurrentStockInfoToken = "UpdateCurrentStockInfoToken";

        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;

        private readonly Task _updateStockInfo;

        public StockInfoViewModel(StockAgent stockAgent, IDialogService dialogService)
        {
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            _updateStockInfo = Update();
            Messenger.Default.Register<GenericMessage<int>>(this, UpdateCurrentStockInfoToken, i => UpdateCurrentStockInfo(i.Content));
        }

        public void UpdateCurrentStockInfo(int sid)
        {
            lock (this)
            {
                CurrentStockInfo = StockInfoList.First(s => s.StockId == sid);
            }
        }

        private async Task Update()
        {
            var t = new TimeSpan(0, 0, 100);
            try
            {
                while (true)
                {
                    var addlist = new List<StockInfo>();
                    var deletelist = new List<StockInfo>();
                    var s = _stockAgent.GetAllStocks();
                    lock (this)
                    {
                        s.ForEach(x =>
                        {
                            var ss = StockInfoList.FirstOrDefault(y => y.StockId == x.StockId);
                            if (ss != null) ss.Update(x);
                            else
                            {
                                var si = new StockInfo();
                                si.Create(_stockAgent.GetStock(x.StockId, DateTime.Now));
                                si.Update(x);
                                addlist.Add(si);
                            }
                        });
                        StockInfoList.ForEach(x =>
                        {
                            var ss = s.FirstOrDefault(y => y.StockId == x.StockId);
                            if (ss == null) deletelist.Add(x);
                        });
                        addlist.ForEach(x => StockInfoList.Add(x));
                        deletelist.ForEach(x => StockInfoList.Remove(x));
                        SortStockInfoList();
                    }
                    await Task.Delay(t);
                }
            }
            catch (Exception e)
            {
                await _dialogService.ShowError(e, "错误", "确定", null);
            }
        }

        private void SortStockInfoList()
        {
            if (CurrentStockInfo != null)
                StockInfoList = StockInfoList.OrderByDescending(x => x.StockId == CurrentStockInfo.StockId)
                    .ThenBy(x => x.StockId).ToList();
            else StockInfoList = StockInfoList.OrderBy(x => x.StockId).ToList();
        }

        #region Property


        /// <summary>
        /// The <see cref="CurrentStockInfo" /> property's name.
        /// </summary>
        public const string CurrentStockInfoPropertyName = nameof(CurrentStockInfo);

        private StockInfo _currentStockInfo;

        /// <summary>
        /// Sets and gets the <see cref="CurrentStockInfo"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public StockInfo CurrentStockInfo
        {
            get => _currentStockInfo;
            set
            {
                Set(CurrentStockInfoPropertyName, ref _currentStockInfo, value, true);
                SortStockInfoList();
                Messenger.Default.Send(new GenericMessage<int?>(CurrentStockInfo.StockId), AccountViewModel.UpdateCurrentUserStockInfoToken);
                Messenger.Default.Send(new GenericMessage<bool>(CurrentStockInfo != null), StockDepthInfoViewModel.UpdateStockDepthInfo);
                Messenger.Default.Send(new GenericMessage<bool>(CurrentStockInfo != null), TransactionRecentInfoViewModel.UpdateTransactionRecentInfo);
                Messenger.Default.Send(new GenericMessage<double?>(CurrentStockInfo == null ? (double?)null : Convert.ToDouble(CurrentStockInfo.Price)), TradeGridViewModel.UpdateSingleText);
            }
        }

        /// <summary>
        /// The <see cref="StockInfoList" /> property's name.
        /// </summary>
        public const string StockInfoListPropertyName = nameof(StockInfoList);

        private List<StockInfo> _stockInfoList = new List<StockInfo>();

        /// <summary>
        /// Sets and gets the <see cref="StockInfoList"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public List<StockInfo> StockInfoList
        {
            get => _stockInfoList;
            set => Set(StockInfoListPropertyName, ref _stockInfoList, value, true);
        }

        #endregion

        #region Command



        #endregion

        public void Dispose()
        {
            _updateStockInfo?.Dispose();
        }
    }
}