namespace StockTradingSystem.Core.Business
{
    public enum ExecOrderResult
    {
        Ok,
        NotEnoughCnyFree,
        NotEnoughNumFree,
        Wrong 
    }

    public enum CancelOrderResult
    {
        Ok,
        Wrong
    }
}