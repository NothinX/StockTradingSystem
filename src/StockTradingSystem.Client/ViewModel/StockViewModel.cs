using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.ViewModel.Control;

namespace StockTradingSystem.Client.ViewModel
{
    public sealed class StockViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly StockInfoViewModel _stockInfoViewModel;

        public StockViewModel(IDialogService dialogService, StockInfoViewModel stockInfoViewModel)
        {
            _dialogService = dialogService;
            _stockInfoViewModel = stockInfoViewModel;
        }

        #region Property

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
            get { return _searchText; }
            set { Set(SearchTextPropertyName, ref _searchText, value, true); }
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
            get { return _searchTextFocus; }
            set { Set(SearchTextFocusPropertyName, ref _searchTextFocus, value, true); }
        }

        #endregion

        #region Command

        private RelayCommand<int> _checkStockBtnCommand;

        /// <summary>
        /// Gets the <see cref="CheckStockBtnCommand"/>.
        /// </summary>
        public RelayCommand<int> CheckStockBtnCommand
        {
            get
            {
                return _checkStockBtnCommand
                       ?? (_checkStockBtnCommand = new RelayCommand<int>(ExecuteCheckStockBtnCommand));
            }
        }

        private void ExecuteCheckStockBtnCommand(int sid)
        {
            _stockInfoViewModel.UpdateCurrentStockInfo(sid);
        }

        private RelayCommand _searchCommand;

        /// <summary>
        /// Gets the SearchCommand.
        /// </summary>
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand
                       ?? (_searchCommand = new RelayCommand(ExecuteSearchCommand));
            }
        }

        private async void ExecuteSearchCommand()
        {
            if (SearchText == "") SearchTextFocus = true;
            else
            {
                var ss = _stockInfoViewModel.StockInfoList.Where(x =>
                    x.StockId.ToString() == SearchText || x.Name.Contains(SearchText)).ToList();
                if (ss.Count == 1)
                {
                    _stockInfoViewModel.UpdateCurrentStockInfo(ss.First().StockId);
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
    }
}