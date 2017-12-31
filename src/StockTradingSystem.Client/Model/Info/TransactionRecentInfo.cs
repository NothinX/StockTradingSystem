using System;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Info
{
    public class TransactionRecentInfo : Info<TransactionResult>
    {
        /// <summary>
        /// The <see cref="TranId" /> property's name.
        /// </summary>
        public const string TranIdPropertyName = nameof(TranId);

        private long _tranId;

        /// <summary>
        /// Sets and gets the <see cref="TranId"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long TranId
        {
            get
            {
                return _tranId;
            }
            set
            {
                Set(TranIdPropertyName, ref _tranId, value);
            }
        }

        /// <summary>
        /// The <see cref="CreateDateTime" /> property's name.
        /// </summary>
        public const string CreateDateTimePropertyName = nameof(CreateDateTime);

        private DateTime _createDateTime;

        /// <summary>
        /// Sets and gets the <see cref="CreateDateTime"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime CreateDateTime
        {
            get
            {
                return _createDateTime;
            }
            set
            {
                Set(CreateDateTimePropertyName, ref _createDateTime, value);
            }
        }

        /// <summary>
        /// The <see cref="Dealed" /> property's name.
        /// </summary>
        public const string DealedPropertyName = nameof(Dealed);

        private int _dealed;

        /// <summary>
        /// Sets and gets the <see cref="Dealed"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Dealed
        {
            get
            {
                return _dealed;
            }
            set
            {
                Set(DealedPropertyName, ref _dealed, value);
            }
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
        /// The <see cref="TranType" /> property's name.
        /// </summary>
        public const string TranTypePropertyName = nameof(TranType);

        private int _tranType;

        /// <summary>
        /// Sets and gets the <see cref="TranType"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TranType
        {
            get
            {
                return _tranType;
            }
            set
            {
                Set(TranTypePropertyName, ref _tranType, value);
            }
        }

        public override void Create(TransactionResult obj)
        {
            TranId = obj.TranId;
            CreateDateTime = obj.CreateDateTime;
            Dealed = obj.Dealed;
            Price = obj.Price;
            TranType = obj.TranType;
        }

        public override void Update(TransactionResult obj)
        {
            throw new NotImplementedException();
        }
    }
}