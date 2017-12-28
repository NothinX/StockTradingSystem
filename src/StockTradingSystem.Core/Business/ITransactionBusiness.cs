using System.Collections.Generic;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Core.Business
{
    public interface ITransactionBusiness
    {
        List<TransactionResult> GetRecentTrans(int stockId, int num);
    }
}