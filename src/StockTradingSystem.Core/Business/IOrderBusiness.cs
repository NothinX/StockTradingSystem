namespace StockTradingSystem.Core.Business
{
    public interface IOrderBusiness
    {
        ExecOrderResult Exec_Order(long userId, int stockId, int type, decimal price, int amount);
        CancelOrderResult Cancel_Order(long userId, long orderId);
    }
}