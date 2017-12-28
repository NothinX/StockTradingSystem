using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Core.Business;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class TradeGridViewModel : ViewModelBase
    {
        public static readonly string UpdateSingleText = "UpdateSingleText";

        private readonly StockInfoViewModel _stockInfoViewModel;
        private readonly IDialogService _dialogService;
        private readonly StockAgent _stockAgent;
        private readonly UserStockInfoViewModel _userStockInfoViewModel;
        private readonly UserMoneyInfo _userMoneyInfo;

        public TradeGridViewModel(StockInfoViewModel stockInfoViewModel, IDialogService dialogService, StockAgent stockAgent, UserStockInfoViewModel userStockInfoViewModel, UserMoneyInfo userMoneyInfo)
        {
            _stockInfoViewModel = stockInfoViewModel;
            _dialogService = dialogService;
            _stockAgent = stockAgent;
            _userStockInfoViewModel = userStockInfoViewModel;
            _userMoneyInfo = userMoneyInfo;
            Messenger.Default.Register<GenericMessage<double?>>(this, UpdateSingleText, d =>
            {
                if (d.Content == null)
                {
                    BuyCbbSelectedIndex = -1;
                    SellCbbSelectedIndex = -1;
                }
                else
                {
                    if (BuyCbbSelectedIndex == 0) BuySingleText = ((double)d.Content).ToString("F2");
                    if (SellCbbSelectedIndex == 0) SellSingleText = ((double)d.Content).ToString("F2");
                }
            });
        }

        private void CalBuyTotal()
        {
            var a = BuySingleText == "" ? 0 : double.Parse(BuySingleText);
            var b = BuyAmountText == "" ? 0 : double.Parse(BuyAmountText);
            BuyTotalText = (a * b).ToString("F2");
        }

        private void CalSellTotal()
        {
            var a = SellSingleText == "" ? 0 : double.Parse(SellSingleText);
            var b = SellAmountText == "" ? 0 : double.Parse(SellAmountText);
            SellTotalText = (a * b).ToString("F2");
        }

        #region Property

        /// <summary>
        /// The <see cref="TradeType" /> property's name.
        /// </summary>
        public const string TradeTypePropertyName = nameof(TradeType);

        private string[] _tradeType = { "市价单", "限价单" };

        /// <summary>
        /// Sets and gets the <see cref="TradeType"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string[] TradeType
        {
            get => _tradeType;
            set => Set(TradeTypePropertyName, ref _tradeType, value, true);
        }

        /// <summary>
        /// The <see cref="BuySingleText" /> property's name.
        /// </summary>
        public const string BuySingleTextPropertyName = nameof(BuySingleText);

        private string _buySingleText = "";

        /// <summary>
        /// Sets and gets the <see cref="BuySingleText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string BuySingleText
        {
            get => _buySingleText;
            set
            {
                Set(BuySingleTextPropertyName, ref _buySingleText, value, true);
                CalBuyTotal();
            }
        }

        /// <summary>
        /// The <see cref="SellSingleText" /> property's name.
        /// </summary>
        public const string SellSingleTextPropertyName = nameof(SellSingleText);

        private string _sellSingleText = "";

        /// <summary>
        /// Sets and gets the <see cref="SellSingleText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string SellSingleText
        {
            get => _sellSingleText;
            set
            {
                Set(SellSingleTextPropertyName, ref _sellSingleText, value, true);
                CalSellTotal();
            }
        }

        /// <summary>
        /// The <see cref="BuyAmountText" /> property's name.
        /// </summary>
        public const string BuyAmountTextPropertyName = nameof(BuyAmountText);

        private string _buyAmountText = "";

        /// <summary>
        /// Sets and gets the <see cref="BuyAmountText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string BuyAmountText
        {
            get => _buyAmountText;
            set
            {
                Set(BuyAmountTextPropertyName, ref _buyAmountText, value, true);
                CalBuyTotal();
            }
        }

        /// <summary>
        /// The <see cref="SellAmountText" /> property's name.
        /// </summary>
        public const string SellAmountTextPropertyName = nameof(SellAmountText);

        private string _sellAmountText = "";

        /// <summary>
        /// Sets and gets the <see cref="SellAmountText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string SellAmountText
        {
            get => _sellAmountText;
            set
            {
                Set(SellAmountTextPropertyName, ref _sellAmountText, value, true);
                CalSellTotal();
            }
        }

        /// <summary>
        /// The <see cref="BuyTotalText" /> property's name.
        /// </summary>
        public const string BuyTotalTextPropertyName = nameof(BuyTotalText);

        private string _buyTotalText = "";

        /// <summary>
        /// Sets and gets the <see cref="BuyTotalText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string BuyTotalText
        {
            get => _buyTotalText;
            set => Set(BuyTotalTextPropertyName, ref _buyTotalText, value, true);
        }

        /// <summary>
        /// The <see cref="SellTotalText" /> property's name.
        /// </summary>
        public const string SellTotalTextPropertyName = nameof(SellTotalText);

        private string _sellTotalText = "";

        /// <summary>
        /// Sets and gets the <see cref="SellTotalText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string SellTotalText
        {
            get => _sellTotalText;
            set => Set(SellTotalTextPropertyName, ref _sellTotalText, value, true);
        }

        /// <summary>
        /// The <see cref="BuySingleIsReadOnly" /> property's name.
        /// </summary>
        public const string BuySingleIsReadOnlyPropertyName = nameof(BuySingleIsReadOnly);

        private bool _buySingleIsReadOnly = true;

        /// <summary>
        /// Sets and gets the <see cref="BuySingleIsReadOnly"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool BuySingleIsReadOnly
        {
            get => _buySingleIsReadOnly;
            set => Set(BuySingleIsReadOnlyPropertyName, ref _buySingleIsReadOnly, value, true);
        }

        /// <summary>
        /// The <see cref="SellSingleIsReadOnly" /> property's name.
        /// </summary>
        public const string SellSingleIsReadOnlyPropertyName = nameof(SellSingleIsReadOnly);

        private bool _sellSingleIsReadOnly = true;

        /// <summary>
        /// Sets and gets the <see cref="SellSingleIsReadOnly"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool SellSingleIsReadOnly
        {
            get => _sellSingleIsReadOnly;
            set => Set(SellSingleIsReadOnlyPropertyName, ref _sellSingleIsReadOnly, value, true);
        }

        /// <summary>
        /// The <see cref="BuySingleFocus" /> property's name.
        /// </summary>
        public const string BuySingleFocusPropertyName = nameof(BuySingleFocus);

        private bool? _buySingleFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="BuySingleFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? BuySingleFocus
        {
            get => _buySingleFocus;
            set => Set(BuySingleFocusPropertyName, ref _buySingleFocus, value, true);
        }

        /// <summary>
        /// The <see cref="SellSingleFocus" /> property's name.
        /// </summary>
        public const string SellSingleFocusPropertyName = nameof(SellSingleFocus);

        private bool? _sellSingleFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="SellSingleFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? SellSingleFocus
        {
            get => _sellSingleFocus;
            set => Set(SellSingleFocusPropertyName, ref _sellSingleFocus, value, true);
        }

        /// <summary>
        /// The <see cref="BuyAmountFocus" /> property's name.
        /// </summary>
        public const string BuyAmountFocusPropertyName = nameof(BuyAmountFocus);

        private bool? _buyAmountFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="BuyAmountFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? BuyAmountFocus
        {
            get => _buyAmountFocus;
            set => Set(BuyAmountFocusPropertyName, ref _buyAmountFocus, value, true);
        }

        /// <summary>
        /// The <see cref="SellAmountFocus" /> property's name.
        /// </summary>
        public const string SellAmountFocusPropertyName = nameof(SellAmountFocus);

        private bool? _sellAmountFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="SellAmountFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? SellAmountFocus
        {
            get => _sellAmountFocus;
            set => Set(SellAmountFocusPropertyName, ref _sellAmountFocus, value, true);
        }

        /// <summary>
        /// The <see cref="BuyCbbSelectedIndex" /> property's name.
        /// </summary>
        public const string BuyCbbSelectedIndexPropertyName = nameof(BuyCbbSelectedIndex);

        private int _buyCbbSelectedIndex = -1;

        /// <summary>
        /// Sets and gets the <see cref="BuyCbbSelectedIndex"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public int BuyCbbSelectedIndex
        {
            get => _buyCbbSelectedIndex;
            set => Set(BuyCbbSelectedIndexPropertyName, ref _buyCbbSelectedIndex, value, true);
        }

        /// <summary>
        /// The <see cref="SellCbbSelectedIndex" /> property's name.
        /// </summary>
        public const string SellCbbSelectedIndexPropertyName = nameof(SellCbbSelectedIndex);

        private int _sellCbbSelectedIndex = -1;

        /// <summary>
        /// Sets and gets the <see cref="SellCbbSelectedIndex"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public int SellCbbSelectedIndex
        {
            get => _sellCbbSelectedIndex;
            set => Set(SellCbbSelectedIndexPropertyName, ref _sellCbbSelectedIndex, value, true);
        }

        /// <summary>
        /// The <see cref="BuyCbbFocus" /> property's name.
        /// </summary>
        public const string BuyCbbFoucsPropertyName = nameof(BuyCbbFocus);

        private bool? _buyCbbFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="BuyCbbFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? BuyCbbFocus
        {
            get => _buyCbbFocus;
            set => Set(BuyCbbFoucsPropertyName, ref _buyCbbFocus, value, true);
        }

        /// <summary>
        /// The <see cref="SellCbbFocus" /> property's name.
        /// </summary>
        public const string SellCbbFocusPropertyName = nameof(SellCbbFocus);

        private bool? _sellCbbFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="SellCbbFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? SellCbbFocus
        {
            get => _sellCbbFocus;
            set => Set(SellCbbFocusPropertyName, ref _sellCbbFocus, value, true);
        }

        #endregion

        #region Command

        private RelayCommand<string> _buyCbbCommand;

        /// <summary>
        /// Gets the <see cref="BuyCbbCommand"/>.
        /// </summary>
        public RelayCommand<string> BuyCbbCommand => _buyCbbCommand
                                                     ?? (_buyCbbCommand = new RelayCommand<string>(ExecuteBuyCbbCommand));

        private void ExecuteBuyCbbCommand(string tradeType)
        {
            BuySingleIsReadOnly = tradeType == "市价单";
            if (tradeType == "市价单") BuySingleText = _stockInfoViewModel.CurrentStockInfo.Price.ToString("F2");
        }

        private RelayCommand<string> _sellCbbCommand;

        /// <summary>
        /// Gets the SellCbbCommand.
        /// </summary>
        public RelayCommand<string> SellCbbCommand => _sellCbbCommand
                                                      ?? (_sellCbbCommand = new RelayCommand<string>(ExecuteSellCbbCommand));

        private void ExecuteSellCbbCommand(string tradeType)
        {
            SellSingleIsReadOnly = tradeType == "市价单";
            if (tradeType == "市价单") SellSingleText = _stockInfoViewModel.CurrentStockInfo.Price.ToString("F2");
        }

        private RelayCommand<TextCompositionEventArgs> _buySinglePreviewTextInputCommand;

        /// <summary>
        /// Gets the <see cref="BuySinglePreviewTextInputCommand"/>.
        /// </summary>
        public RelayCommand<TextCompositionEventArgs> BuySinglePreviewTextInputCommand => _buySinglePreviewTextInputCommand
                                                                                 ?? (_buySinglePreviewTextInputCommand = new RelayCommand<TextCompositionEventArgs>(ExecuteBuySinglePreviewTextInputCommand));

        private void ExecuteBuySinglePreviewTextInputCommand(TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(BuySingleText + e.Text, out var res);
        }

        private RelayCommand<TextCompositionEventArgs> _sellSinglePreviewTextInputCommand;

        /// <summary>
        /// Gets the SellSinglePreviewTextInputCommand.
        /// </summary>
        public RelayCommand<TextCompositionEventArgs> SellSinglePreviewTextInputCommand => _sellSinglePreviewTextInputCommand
                                                                                           ?? (_sellSinglePreviewTextInputCommand = new RelayCommand<TextCompositionEventArgs>(ExecuteSellSinglePreviewTextInputCommand));

        private void ExecuteSellSinglePreviewTextInputCommand(TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(SellSingleText + e.Text, out var res);
        }

        private RelayCommand<TextCompositionEventArgs> _amountPreviewTextInputCommand;

        /// <summary>
        /// Gets the AmountPreviewTextInputCommand.
        /// </summary>
        public RelayCommand<TextCompositionEventArgs> AmountPreviewTextInputCommand => _amountPreviewTextInputCommand
                                                                                       ?? (_amountPreviewTextInputCommand = new RelayCommand<TextCompositionEventArgs>(ExecuteAmountPreviewTextInputCommand));

        private static void ExecuteAmountPreviewTextInputCommand(TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out var res);
        }

        private RelayCommand _buyCommand;

        /// <summary>
        /// Gets the <see cref="BuyCommand"/>.
        /// </summary>
        public RelayCommand BuyCommand => _buyCommand
                                          ?? (_buyCommand = new RelayCommand(ExecuteBuyCommand));

        private async void ExecuteBuyCommand()
        {
            if (BuySingleText == "") BuySingleFocus = true;
            else if (BuyAmountText == "")
                BuyAmountFocus = true;
            else if (BuyCbbSelectedIndex < 0)
                await _dialogService.ShowMessage("请选择委托单类型", "错误", "确定", () => BuyCbbFocus = true);
            else
            {
                var a = decimal.Parse(BuySingleText);
                var b = int.Parse(BuyAmountText);
                if (double.TryParse(BuyTotalText, out var total) && total <= Convert.ToDouble(_userMoneyInfo.CnyFree))
                {
                    var res = _stockAgent.Exec_Order(_stockInfoViewModel.CurrentStockInfo.StockId, 0, a, b);
                    switch (res)
                    {
                        case ExecOrderResult.Ok:
                            await _dialogService.ShowMessage("委托成功", "提示");
                            break;
                        case ExecOrderResult.NotEnoughCnyFree:
                            await _dialogService.ShowMessage("可用资金不足，无法委托", "错误", "确定", () =>
                            {
                                if (BuySingleIsReadOnly) BuyAmountFocus = true;
                                else BuySingleFocus = true;
                            });
                            break;
                        case ExecOrderResult.NotEnoughNumFree:
                            await _dialogService.ShowMessage("股票余额不足，无法委托", "错误", "确定", () =>
                            {
                                if (BuySingleIsReadOnly) BuyAmountFocus = true;
                                else BuySingleFocus = true;
                            });
                            break;
                        case ExecOrderResult.Wrong:
                            await _dialogService.ShowMessage("委托失败", "错误");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else await _dialogService.ShowMessage("可用资金不足，无法委托", "错误", "确定", () =>
                {
                    if (BuySingleIsReadOnly) BuyAmountFocus = true;
                    else BuySingleFocus = true;
                });
            }
        }

        private RelayCommand _sellCommand;

        /// <summary>
        /// Gets the <see cref="SellCommand"/>.
        /// </summary>
        public RelayCommand SellCommand => _sellCommand
                                           ?? (_sellCommand = new RelayCommand(ExecuteSellCommand));

        private async void ExecuteSellCommand()
        {
            if (SellSingleText == "") SellSingleFocus = true;
            else if (SellAmountText == "")
                SellAmountFocus = true;
            else if (SellCbbSelectedIndex < 0)
                await _dialogService.ShowMessage("请选择委托单类型", "错误", "确定", () => SellCbbFocus = true);
            else
            {
                var a = decimal.Parse(SellSingleText);
                var b = int.Parse(SellAmountText);
                var gpNum = _userStockInfoViewModel.UserStockInfoList
                                .FirstOrDefault(x => x.StockId == _stockInfoViewModel.CurrentStockInfo.StockId)
                                ?.AvailableStock ?? 0;
                if (b <= gpNum)
                {
                    var res = _stockAgent.Exec_Order(_stockInfoViewModel.CurrentStockInfo.StockId, 1, a, b);
                    switch (res)
                    {
                        case ExecOrderResult.Ok:
                            await _dialogService.ShowMessage("委托成功", "提示");
                            break;
                        case ExecOrderResult.NotEnoughCnyFree:
                            await _dialogService.ShowMessage("资金余额不足，无法委托", "错误", "确定", () =>
                            {
                                if (SellSingleIsReadOnly) SellAmountFocus = true;
                                else SellSingleFocus = true;
                            });
                            break;
                        case ExecOrderResult.NotEnoughNumFree:
                            await _dialogService.ShowMessage("可用股票不足，无法委托", "错误", "确定", () =>
                            {
                                if (SellSingleIsReadOnly) SellAmountFocus = true;
                                else SellSingleFocus = true;
                            });
                            break;
                        case ExecOrderResult.Wrong:
                            await _dialogService.ShowMessage("委托失败", "错误");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else await _dialogService.ShowMessage("可用股票不足，无法委托", "错误", "确定", () =>
                {
                    if (SellSingleIsReadOnly) SellAmountFocus = true;
                    else SellSingleFocus = true;
                });
            }
        }

        #endregion
    }
}