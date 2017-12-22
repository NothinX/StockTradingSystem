using StockTradingSystem.Core.Business;
using StockTradingSystem.Core.Model;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace StockTradingSystem.Client.Model.Access
{
    internal class GpEntitiesBusiness : IBusiness
    {
        public bool Cancel_Order(long userId, long orderId)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.cancel_order(userId, orderId).First() == 0;
            }
        }

        public bool Exec_Order(long userId, int stockId, int type, decimal price, int amount)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.exec_order(userId, stockId, type, price, amount).First() == 0;
            }
        }

        public bool Stock_depth(int stockId, int type, out List<StockDepthResult> stockDepthResult)
        {
            using (var gpEntities = new GPEntities())
            {
                var res = gpEntities.stock_depth(stockId, type).ToList();
                stockDepthResult = res.Select(x => new StockDepthResult()
                {
                    Price = x.price,
                    Num = x.num ?? 0
                }).ToList();
                return true;
            }
        }

        public bool User_cny(long userId, out decimal? cnyFree, out decimal? cnyFreezed, out decimal? gpMoney)
        {
            using (var gpEntities = new GPEntities())
            {
                cnyFree = 0;
                cnyFreezed = 0;
                gpMoney = 0;
                var cf = new ObjectParameter("cny_free", cnyFree);
                var cfed = new ObjectParameter("cny_freezed", cnyFreezed);
                var gm = new ObjectParameter("gp_money", gpMoney);
                var res = gpEntities.user_cny(userId, cf, cfed, gm);
                cnyFree = cf.Value as decimal?;
                cnyFreezed = cfed.Value as decimal?;
                gpMoney = gm.Value as decimal?;
                return res.First() == 0;
            }
        }

        public bool User_order(long userId, out List<UserOrderResult> userOrderResult)
        {
            using (var gpEntities = new GPEntities())
            {
                var res = gpEntities.user_order(userId).ToList();
                userOrderResult = res.Select(x => new UserOrderResult
                {
                    OrderId = x.order_id,
                    CreateDatetime = x.create_datetime,
                    UserId = x.user_id,
                    StockId = x.stock_id,
                    Type = x.type,
                    Price = x.price,
                    Undealed = x.undealed,
                    Dealed = x.dealed,
                    Canceled = x.canceled
                }).ToList();
                return true;
            }
        }

        public bool User_stock(long userId, out List<UserStockResult> userStockResult)
        {
            using (var gpEntities = new GPEntities())
            {
                var res = gpEntities.user_stock(userId).ToList();
                userStockResult = res.Select(x => new UserStockResult()
                {
                    StockId = x.stock_id,
                    NumFree = x.num_free,
                    NumFreezed = x.num_freezed
                }).ToList();
                return true;
            }
        }
    }
}