using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using StockTradingSystem.Core.Business;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model.Business
{
    internal class GpBusiness : IBusiness
    {
        public CancelOrderResult Cancel_Order(long userId, long orderId)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.cancel_order(userId, orderId).First() == 0 ? CancelOrderResult.Ok : CancelOrderResult.Wrong;
            }
        }

        public ExecOrderResult Exec_Order(long userId, int stockId, int type, decimal price, int amount)
        {
            using (var gpEntities = new GPEntities())
            {
                switch (gpEntities.exec_order(userId, stockId, type, price, amount).First())
                {
                    case 0: return ExecOrderResult.Ok;
                    case -1: return ExecOrderResult.NotEnoughCnyFree;
                    case -2: return ExecOrderResult.NotEnoughNumFree;
                    case -3: return ExecOrderResult.Wrong;
                    default: return ExecOrderResult.Wrong;
                }
            }
        }

        public List<StockDepthResult> Stock_depth(int stockId, int type)
        {
            using (var gpEntities = new GPEntities())
            {
                var res = gpEntities.stock_depth(stockId, type).ToList();
                return res.Select(x => new StockDepthResult()
                {
                    Price = x.price,
                    Num = x.num ?? 0
                }).ToList();
            }
        }

        public UserCnyResult User_cny(long userId)
        {
            using (var gpEntities = new GPEntities())
            {
                decimal? cnyFree = 0;
                decimal? cnyFreezed = 0;
                decimal? gpMoney = 0;
                var cf = new ObjectParameter("cny_free", cnyFree);
                var cfed = new ObjectParameter("cny_freezed", cnyFreezed);
                var gm = new ObjectParameter("gp_money", gpMoney);
                gpEntities.user_cny(userId, cf, cfed, gm);
                return new UserCnyResult(cf.Value as decimal? ?? 0, cfed.Value as decimal? ?? 0, gm.Value as decimal? ?? 0);
            }
        }

        public List<UserOrderResult> User_order(long userId)
        {
            using (var gpEntities = new GPEntities())
            {
                var res = gpEntities.user_order(userId).ToList();
                return res.Select(x => new UserOrderResult
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
            }
        }

        public List<UserStockResult> User_stock(long userId)
        {
            using (var gpEntities = new GPEntities())
            {
                var res = gpEntities.user_stock(userId).ToList();
                return res.Select(x => new UserStockResult()
                {
                    StockId = x.stock_id,
                    NumFree = x.num_free,
                    NumFreezed = x.num_freezed
                }).ToList();
            }
        }

        public List<StockResult> GetAllStocks()
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.stocks.Select(x => new StockResult { StockId = x.stock_id, Name = x.name, Price = x.price }).ToList();
            }
        }

        public StockResult GetStock(int stockId, DateTime dateTime)
        {
            using (var gpEntities = new GPEntities())
            {
                var s = gpEntities.stocks.FirstOrDefault(x => x.stock_id == stockId);
                Debug.Assert(s != null, nameof(s) + " != null");
                var q = gpEntities.transactions.Where(x => x.stock_id == stockId && x.create_datetime < dateTime)
                    .OrderBy(x => x.create_datetime).ToList();
                var ql = q.LastOrDefault();
                return ql != null ? new StockResult { StockId = stockId, Name = s.name, Price = ql.deal_price } : new StockResult { StockId = stockId, Name = s.name, Price = s.price };
            }
        }
    }
}