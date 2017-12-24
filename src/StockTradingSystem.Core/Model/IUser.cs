using System.ComponentModel;

namespace StockTradingSystem.Core.Model
{
    public interface IUser : INotifyPropertyChanged
    {
        long UserId { get; set; }
        string LoginName { get; set; }
        string Name { get; set; }
        int Type { get; set; }
        bool IsLogin { get; set; }
    }
}