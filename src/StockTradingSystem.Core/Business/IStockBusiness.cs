using System;
using System.Collections.Generic;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Core.Business
{
    public interface IStockBusiness
    {
        List<StockDepthResult> Stock_depth(int stockId, int type);
        List<StockResult> GetAllStocks();
        StockResult GetStock(int stockId, DateTime dateTime);
    }
}