using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel
{
    public sealed class StockViewModel : ViewModelBase, IDisposable
    {
        private readonly StockAgent _stockAgent;

        private readonly Task _updateStockInfo;

        public StockViewModel(StockAgent stockAgent)
        {
            _stockAgent = stockAgent;
            _updateStockInfo = Update();
        }

        private async Task Update()
        {
            var t = new TimeSpan(0, 0, 5);
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
                    StockInfoList = StockInfoList.OrderBy(x => x.StockId).ToList();
                }
                await Task.Delay(t);
            }
        }

        #region Property

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