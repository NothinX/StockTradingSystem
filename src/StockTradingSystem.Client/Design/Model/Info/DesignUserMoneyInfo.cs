using StockTradingSystem.Client.Model.Info;

namespace StockTradingSystem.Client.Design.Model.Info
{
    public class DesignUserMoneyInfo : UserMoneyInfo
    {
        public DesignUserMoneyInfo()
        {
            CnyFree = 1234;
            CnyFreezed = 12345;
            GpMoney = 123456;
        }
    }
}