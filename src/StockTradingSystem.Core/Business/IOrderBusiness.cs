namespace StockTradingSystem.Core.Business
{
    public interface IOrderBusiness
    {
        bool Exec_Order(long userId,int stockId,int type,decimal price,int amount);
        bool Cancel_Order(long userId,long orderId);
    }
}