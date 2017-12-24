using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using StockTradingSystem.Client.ViewModel.Control;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Info
{
    public class UserMoneyInfo : Info
    {
        private readonly StockAgent _stockAgent;

        public UserMoneyInfo(StockAgent stockAgent)
        {
            _stockAgent = stockAgent;
        }

        /// <summary>
        /// The <see cref="CnyFree" /> property's name.
        /// </summary>
        public const string CnyFreePropertyName = nameof(CnyFree);

        private decimal _cnyFree;

        /// <summary>
        /// Sets and gets the <see cref="CnyFree"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal CnyFree
        {
            get => _cnyFree;
            set => Set(CnyFreePropertyName, ref _cnyFree, value);
        }

        /// <summary>
        /// The <see cref="CnyFreezed" /> property's name.
        /// </summary>
        public const string CnyFreezedPropertyName = nameof(CnyFreezed);

        private decimal _cnyFreezed;

        /// <summary>
        /// Sets and gets the <see cref="CnyFreezed"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal CnyFreezed
        {
            get => _cnyFreezed;
            set => Set(CnyFreezedPropertyName, ref _cnyFreezed, value);
        }

        /// <summary>
        /// The <see cref="GpMoney" /> property's name.
        /// </summary>
        public const string GpMoneyPropertyName = nameof(GpMoney);

        private decimal _gpMoney;

        /// <summary>
        /// Sets and gets the <see cref="GpMoney"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal GpMoney
        {
            get => _gpMoney;
            set => Set(GpMoneyPropertyName, ref _gpMoney, value);
        }

        public override void Update()
        {
            var ucr = _stockAgent.User_cny();
            CnyFree = ucr.CnyFree;
            CnyFreezed = ucr.CnyFreezed;
            GpMoney = ucr.GpMoney;
            Messenger.Default.Send(new GenericMessage<decimal>(ucr.CnyFree + ucr.CnyFreezed), AccountButtonViewModel.UpdateTotalMoney);
            Messenger.Default.Send(new GenericMessage<decimal>(ucr.CnyFree), AccountButtonViewModel.UpdateAvailableMoney);
        }
    }
}