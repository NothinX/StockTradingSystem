using System.Collections.Generic;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Core.Business
{
    public interface IStockBusiness
    {
        bool Stock_depth(int stockId, int type, out List<StockDepthResult> stockDepthResult);
    }
}