using System.Collections.Generic;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Core.Business
{
    public interface IUserBusiness
    {
        bool User_cny(long userId, out decimal? cnyFree, out decimal? cnyFreezed, out decimal? gpMoney);
        bool User_order(long userId, out List<UserOrderResult> userOrderResult);
        bool User_stock(long userId, out List<UserStockResult> userStockResult);
    }
}