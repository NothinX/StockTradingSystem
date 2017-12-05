namespace StockTradingSystem.Core.Model
{
    public sealed class UserCnyResult
    {
        public decimal CnyFree { get; }
        public decimal CnyFreezed { get; }
        public decimal GpMoney { get; }

        public UserCnyResult(decimal cnyFree, decimal cnyFreezed, decimal gpMoney)
        {
            CnyFree = cnyFree;
            CnyFreezed = cnyFreezed;
            GpMoney = gpMoney;
        }
    }
}