using System.Collections.Generic;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Core.Business
{
    public interface IStockBusiness
    {
        List<StockDepthResult> Stock_depth(int stockId, int type);
    }
}