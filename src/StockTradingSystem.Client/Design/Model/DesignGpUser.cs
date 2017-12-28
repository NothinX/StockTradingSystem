using GalaSoft.MvvmLight;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Design.Model
{
    public class DesignGpUser : ObservableObject, IUser
    {
        public long UserId { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; } = "Nothin";
        public int Type { get; set; }
        public bool IsLogin { get; set; }
    }
}