using System;

namespace StockTradingSystem.Core.Model
{
    public sealed class UserOrderResult
    {
        public long OrderId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public long UserId { get; set; }
        public int StockId { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }
        public int Undealed { get; set; }
        public int Dealed { get; set; }
        public int Canceled { get; set; }
    }
}