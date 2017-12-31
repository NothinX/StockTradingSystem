using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Info
{
    public class StockDepthInfo : Info<StockDepthResult>
    {
        public StockInfo StopcInfo { get; set; }

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
            get
            {
                return _price;
            }
            set
            {
                Set(PricePropertyName, ref _price, value);
            }
        }

        /// <summary>
        /// The <see cref="Num" /> property's name.
        /// </summary>
        public const string NumPropertyName = nameof(Num);

        private int _num;

        /// <summary>
        /// Sets and gets the <see cref="Num"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Num
        {
            get
            {
                return _num;
            }
            set
            {
                Set(NumPropertyName, ref _num, value);
            }
        }

        public override void Create(StockDepthResult obj)
        {
            Price = obj.Price;
            Num = obj.Num;
        }

        public override void Update(StockDepthResult obj)
        {
            Num = obj.Num;
        }
    }
}