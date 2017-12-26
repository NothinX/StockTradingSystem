using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Client.ViewModel.Control;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel
{
    public sealed class StockViewModel : ViewModelBase, IDisposable
    {
        public static readonly string UpdateCurrentStockIdToken = "UpdateCurrentStockId";

        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;

        private Task _updateStockInfo;

        public StockViewModel(StockAgent stockAgent, IDialogService dialogService)
        {
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            Messenger.Default.Register<GenericMessage<int>>(this, UpdateCurrentStockIdToken, m => UpdateCurrentStockId(m.Content));
        }

        private void UpdateCurrentStockId(int id)
        {
            lock (this)
            {
                StockInfoList.ForEach(s =>
                {
                    if (s.StockInfo.StockId != id) s.IsCurrent = false;
                });
                var si = StockInfoList.FirstOrDefault(s => s.StockInfo.StockId == id);
                if (si == null) return;
                si.IsCurrent = true;
                CurrentStockInfo = si.StockInfo;
            }
        }

        private async Task Update()
        {
            var t = new TimeSpan(0, 0, 5);
            try
            {
                while (true)
                {
                    var addlist = new List<StockInfoViewModel>();
                    var deletelist = new List<StockInfoViewModel>();
                    var s = _stockAgent.GetAllStocks();
                    lock (this)
                    {
                        s.ForEach(x =>
                        {
                            var ss = StockInfoList.FirstOrDefault(y => y.StockInfo.StockId == x.StockId);
                            if (ss != null) ss.StockInfo.Update(x);
                            else
                            {
                                var si = new StockInfo();
                                si.Create(_stockAgent.GetStock(x.StockId, DateTime.Now));
                                si.Update(x);
                                addlist.Add(new StockInfoViewModel(si));
                            }
                        });
                        StockInfoList.ForEach(x =>
                        {
                            var ss = s.FirstOrDefault(y => y.StockId == x.StockInfo.StockId);
                            if (ss == null) deletelist.Add(x);
                        });
                        addlist.ForEach(x => StockInfoList.Add(x));
                        deletelist.ForEach(x => StockInfoList.Remove(x));
                        StockInfoList = StockInfoList.OrderByDescending(x => x.IsCurrent).ThenBy(x => x.StockInfo.StockId).ToList();
                    }
                    await Task.Delay(t);
                }
            }
            catch (Exception e)
            {
                await _dialogService.ShowError(e, "错误", "确定", null);
            } 
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
                StockInfoList = StockInfoList.OrderByDescending(x => x.IsCurrent).ThenBy(x => x.StockInfo.StockId).ToList();
                Set(CurrentStockInfoPropertyName, ref _currentStockInfo, value, true);
            }
        }

        /// <summary>
        /// The <see cref="StockInfoList" /> property's name.
        /// </summary>
        public const string StockInfoListPropertyName = nameof(StockInfoList);

        private List<StockInfoViewModel> _stockInfoList = new List<StockInfoViewModel>();

        /// <summary>
        /// Sets and gets the <see cref="StockInfoList"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public List<StockInfoViewModel> StockInfoList
        {
            get => _stockInfoList;
            set => Set(StockInfoListPropertyName, ref _stockInfoList, value, true);
        }

        /// <summary>
        /// The <see cref="SearchText" /> property's name.
        /// </summary>
        public const string SearchTextPropertyName = nameof(SearchText);

        private string _searchText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="SearchText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set => Set(SearchTextPropertyName, ref _searchText, value, true);
        }

        /// <summary>
        /// The <see cref="SearchTextFocus" /> property's name.
        /// </summary>
        public const string SearchTextFocusPropertyName = nameof(SearchTextFocus);

        private bool _searchTextFocus;

        /// <summary>
        /// Sets and gets the <see cref="SearchTextFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool SearchTextFocus
        {
            get => _searchTextFocus;
            set => Set(SearchTextFocusPropertyName, ref _searchTextFocus, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _loadedCommand;

        /// <summary>
        /// Gets the LoadedCommand.
        /// </summary>
        public RelayCommand LoadedCommand => _loadedCommand
                                             ?? (_loadedCommand = new RelayCommand(ExecuteLoadedCommand));

        private async void ExecuteLoadedCommand()
        {
            await Task.Run(() => _updateStockInfo = Update());
        }

        private RelayCommand _searchCommand;

        /// <summary>
        /// Gets the SearchCommand.
        /// </summary>
        public RelayCommand SearchCommand => _searchCommand
                                             ?? (_searchCommand = new RelayCommand(ExecuteSearchCommand));

        private async void ExecuteSearchCommand()
        {
            if (SearchText == "") SearchTextFocus = true;
            else
            {
                var ss = StockInfoList.Where(x =>
                    x.StockInfo.StockId.ToString() == SearchText || x.StockInfo.Name.Contains(SearchText)).ToList();
                if (ss.Count == 1)
                {
                    UpdateCurrentStockId(ss.First().StockInfo.StockId);
                    SearchText = "";
                } 
                else if (ss.Count > 1)
                {
                    await _dialogService.ShowMessage($"找到与“{SearchText}”相关的股票不唯一", "提示");
                    SearchTextFocus = true;
                }
                else
                {
                    await _dialogService.ShowMessage($"无法找到与“{SearchText}”相关的股票", "提示");
                    SearchTextFocus = true;
                }
            }
        }

        #endregion

        public void Dispose()
        {
            _updateStockInfo?.Dispose();
        }
    }
}