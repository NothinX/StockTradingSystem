using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Info
{
    public class UserStockInfo : Info<UserStockResult>
    {
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
        /// The <see cref="StockName" /> property's name.
        /// </summary>
        public const string StockNamePropertyName = nameof(StockName);

        private string _stockName = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="StockName"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StockName
        {
            get => _stockName;
            set => Set(StockNamePropertyName, ref _stockName, value);
        }

        /// <summary>
        /// The <see cref="TotalStock" /> property's name.
        /// </summary>
        public const string TotalStockPropertyName = nameof(TotalStock);

        private int _totalStock;

        /// <summary>
        /// Sets and gets the <see cref="TotalStock"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TotalStock
        {
            get => _totalStock;
            set => Set(TotalStockPropertyName, ref _totalStock, value);
        }

        /// <summary>
        /// The <see cref="AvailableStock" /> property's name.
        /// </summary>
        public const string AvailableStockPropertyName = nameof(AvailableStock);

        private int _availableStock;

        /// <summary>
        /// Sets and gets the <see cref="AvailableStock"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int AvailableStock
        {
            get => _availableStock;
            set => Set(AvailableStockPropertyName, ref _availableStock, value);
        }

        /// <summary>
        /// The <see cref="FreezedStock" /> property's name.
        /// </summary>
        public const string FreezedStockPropertyName = nameof(FreezedStock);

        private int _freezedStock;

        /// <summary>
        /// Sets and gets the <see cref="FreezedStock"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int FreezedStock
        {
            get => _freezedStock;
            set => Set(FreezedStockPropertyName, ref _freezedStock, value);
        }

        public override void Create(UserStockResult obj)
        {
            StockId = obj.StockId;
            AvailableStock = obj.NumFree;
            FreezedStock = obj.NumFreezed;
            TotalStock = AvailableStock + FreezedStock;
        }

        public override void Update(UserStockResult obj)
        {
            StockId = obj.StockId;
            AvailableStock = obj.NumFree;
            FreezedStock = obj.NumFreezed;
            TotalStock = AvailableStock + FreezedStock;
        }
    }
}