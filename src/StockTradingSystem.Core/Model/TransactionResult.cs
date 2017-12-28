using System;

namespace StockTradingSystem.Core.Model
{
    public class TransactionResult
    {
        public long TranId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Dealed { get; set; }
        public decimal Price { get; set; }
        public int TranType { get; set; }
    }
}