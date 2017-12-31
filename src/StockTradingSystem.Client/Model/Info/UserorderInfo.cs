using System;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Info
{
    public class UserOrderInfo : Info<UserOrderResult>
    {
        public StockInfo StockInfo { get; set; }

        /// <summary>
        /// The <see cref="OrderId" /> property's name.
        /// </summary>
        public const string OrderIdPropertyName = nameof(OrderId);

        private long _orderId;

        /// <summary>
        /// Sets and gets the <see cref="OrderId"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long OrderId
        {
            get
            {
                return _orderId;
            }
            set
            {
                Set(OrderIdPropertyName, ref _orderId, value);
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
        /// The <see cref="OrderType" /> property's name.
        /// </summary>
        public const string OrderTypePropertyName = nameof(OrderType);

        private int _orderType;

        /// <summary>
        /// Sets and gets the <see cref="OrderType"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                Set(OrderTypePropertyName, ref _orderType, value);
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
        /// The <see cref="Undealed" /> property's name.
        /// </summary>
        public const string UndealedPropertyName = nameof(Undealed);

        private int _undealed;

        /// <summary>
        /// Sets and gets the <see cref="Undealed"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Undealed
        {
            get
            {
                return _undealed;
            }
            set
            {
                Set(UndealedPropertyName, ref _undealed, value);
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
        /// The <see cref="Canceled" /> property's name.
        /// </summary>
        public const string CanceledPropertyName = nameof(Canceled);

        private int _canceled;

        /// <summary>
        /// Sets and gets the <see cref="Canceled"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Canceled
        {
            get
            {
                return _canceled;
            }
            set
            {
                Set(CanceledPropertyName, ref _canceled, value);
            }
        }

        public override void Create(UserOrderResult obj)
        {
            OrderId = obj.OrderId;
            CreateDateTime = obj.CreateDatetime;
            OrderType = obj.Type;
            Price = obj.Price;
            Undealed = obj.Undealed;
            Dealed = obj.Dealed;
            Canceled = obj.Canceled;
        }

        public override void Update(UserOrderResult obj)
        {
            Undealed = obj.Undealed;
            Dealed = obj.Dealed;
            Canceled = obj.Canceled;
        }
    }
}