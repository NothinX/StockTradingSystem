namespace StockTradingSystem.Core.Model
{
    public sealed class UserStockResult
    {
        public int StockId { get; set; }
        public int NumFree { get; set; }
        public int NumFreezed { get; set; }
    }
}