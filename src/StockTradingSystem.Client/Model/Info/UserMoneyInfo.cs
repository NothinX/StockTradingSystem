using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Info
{
    public class UserMoneyInfo : Info<UserCnyResult> 
    {
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
            get
            {
                return _cnyFree;
            }
            set
            {
                Set(CnyFreePropertyName, ref _cnyFree, value);
            }
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
            get
            {
                return _cnyFreezed;
            }
            set
            {
                Set(CnyFreezedPropertyName, ref _cnyFreezed, value);
            }
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
            get
            {
                return _gpMoney;
            }
            set
            {
                Set(GpMoneyPropertyName, ref _gpMoney, value);
            }
        }

        /// <summary>
        /// The <see cref="TotalMoney" /> property's name.
        /// </summary>
        public const string TotalMoneyPropertyName = nameof(TotalMoney);

        private decimal _totalMoney;

        /// <summary>
        /// Sets and gets the <see cref="TotalMoney"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal TotalMoney
        {
            get
            {
                return _totalMoney;
            }
            set
            {
                Set(TotalMoneyPropertyName, ref _totalMoney, value);
            }
        }

        public override void Create(UserCnyResult obj)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(UserCnyResult obj)
        {
            CnyFree = obj.CnyFree;
            CnyFreezed = obj.CnyFreezed;
            GpMoney = obj.GpMoney;
            TotalMoney = CnyFree + CnyFreezed + GpMoney;
        }
    }
}