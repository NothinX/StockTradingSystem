using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using StockTradingSystem.Client.Model.Info;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class StockInfoViewModel : ViewModelBase
    {
        public static readonly string SetCurrentStockId = "SetCurrentStockId";

        public StockInfo StockInfo { get; set; }

        public StockInfoViewModel(StockInfo stockInfo)
        {
            StockInfo = stockInfo;
            Messenger.Default.Register<GenericMessage<int>>(this, SetCurrentStockId, x =>
            {
                if (x.Content == StockInfo.StockId) IsCurrent = true;
            });
        }

        #region Property

        /// <summary>
        /// The <see cref="IsCurrent" /> property's name.
        /// </summary>
        public const string IsCurrentPropertyName = nameof(IsCurrent);

        private bool _isCurrent;

        /// <summary>
        /// Sets and gets the <see cref="IsCurrent"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsCurrent
        {
            get => _isCurrent;
            set => Set(IsCurrentPropertyName, ref _isCurrent, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _checkStockBtnCommand;

        /// <summary>
        /// Gets the <see cref="CheckStockBtnCommand"/>.
        /// </summary>
        public RelayCommand CheckStockBtnCommand => _checkStockBtnCommand
                                                         ?? (_checkStockBtnCommand = new RelayCommand(ExecuteCheckStockBtnCommand));

        private void ExecuteCheckStockBtnCommand()
        {
            Messenger.Default.Send(new GenericMessage<int>(StockInfo.StockId), StockViewModel.UpdateCurrentStockIdToken);
        }

        #endregion

    }
}