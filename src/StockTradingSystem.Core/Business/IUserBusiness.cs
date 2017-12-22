using System.Collections.Generic;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Core.Business
{
    public interface IUserBusiness
    {
        UserCnyResult User_cny(long userId);
        List<UserOrderResult> User_order(long userId);
        List<UserStockResult> User_stock(long userId);
    }
}