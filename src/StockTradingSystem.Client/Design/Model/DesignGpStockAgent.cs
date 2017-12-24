using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Business;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Design.Model
{
    public class DesignGpStockAgent : StockAgent
    {
        public DesignGpStockAgent(IUserAccess userAccess, IBusiness business, IUser user) : base(userAccess, business, user)
        {

        }
    }
}