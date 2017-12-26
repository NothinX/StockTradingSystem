using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using StockTradingSystem.Client.ViewModel;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Info
{
    public class StockInfo : Info<StockResult>
    {
        private decimal _initPrice;

        /// <summary>
        /// The <see cref="StockId" /> property's name.
        /// </summary>
        public const string StockIdPropertyName = nameof(StockId);

        private int _stockId;

        /// <summary>
        /// Sets and gets the <see cref="StockId"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int StockId
        {
            get => _stockId;
            set => Set(StockIdPropertyName, ref _stockId, value);
        }

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = nameof(Name);

        private string _name = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="Name"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(NamePropertyName, ref _name, value);
        }

        /// <summary>
        /// The <see cref="Price" /> property's name.
        /// </summary>
        public const string PricePropertyName = nameof(Price);

        private decimal _price;

        /// <summary>
        /// Sets and gets the <see cref="Price"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal Price
        {
            get => _price;
            set => Set(PricePropertyName, ref _price, value);
        }

        /// <summary>
        /// The <see cref="PriceChange" /> property's name.
        /// </summary>
        public const string PriceChangePropertyName = nameof(PriceChange);

        private decimal _priceChange;

        /// <summary>
        /// Sets and gets the <see cref="PriceChange"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public decimal PriceChange
        {
            get => _priceChange;
            set => Set(PriceChangePropertyName, ref _priceChange, value);
        }

        /// <summary>
        /// The <see cref="PricePercent" /> property's name.
        /// </summary>
        public const string PricePercentPropertyName = nameof(PricePercent);

        private decimal _pricePercent;

        /// <summary>
        /// Sets and gets the <see cref="PricePercent"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public decimal PricePercent
        {
            get => _pricePercent;
            set => Set(PricePercentPropertyName, ref _pricePercent, value);
        }

        /// <summary>
        /// The <see cref="Change" /> property's name.
        /// </summary>
        public const string ChangePropertyName = nameof(Change);

        private bool? _change;

        /// <summary>
        /// Sets and gets the <see cref="Change"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool? Change
        {
            get => _change;
            set => Set(ChangePropertyName, ref _change, value);
        }

        public override void Create(StockResult obj)
        {
            StockId = obj.StockId;
            Name = obj.Name;
            _initPrice = Price = obj.Price;
        }

        public override void Update(StockResult obj)
        {
            StockId = obj.StockId;
            Name = obj.Name;
            if (_initPrice > obj.Price) Change = false;
            else if (_initPrice == obj.Price) Change = null;
            else Change = true;
            Price = obj.Price;
            PriceChange = (Price - _initPrice);
            PricePercent = (Price - _initPrice) / _initPrice * 100;
        }
    }
}