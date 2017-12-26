using System;
using System.Collections.Generic;
using StockTradingSystem.Core.Business;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Design.Model.Business
{
    internal class DesignGpBusiness : IBusiness
    {
        public List<StockDepthResult> Stock_depth(int stockId, int type)
        {
            throw new System.NotImplementedException();
        }

        public List<StockResult> GetAllStocks()
        {
            throw new System.NotImplementedException();
        }

        public StockResult GetStock(int stockId, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public ExecOrderResult Exec_Order(long userId, int stockId, int type, decimal price, int amount)
        {
            throw new System.NotImplementedException();
        }

        public CancelOrderResult Cancel_Order(long userId, long orderId)
        {
            throw new System.NotImplementedException();
        }

        public UserCnyResult User_cny(long userId)
        {
            throw new System.NotImplementedException();
        }

        public List<UserOrderResult> User_order(long userId)
        {
            throw new System.NotImplementedException();
        }

        public List<UserStockResult> User_stock(long userId)
        {
            throw new System.NotImplementedException();
        }
    }
}