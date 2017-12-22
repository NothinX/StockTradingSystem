using StockTradingSystem.Client.Model.Access;
using StockTradingSystem.Client.Model.Business;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Business;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model
{
    public class GpStockAgent : StockAgent
    {
        public GpStockAgent(IUserAccess userAccess, IBusiness business, User user = null) : base(userAccess, business, user)
        {

        }
    }
}