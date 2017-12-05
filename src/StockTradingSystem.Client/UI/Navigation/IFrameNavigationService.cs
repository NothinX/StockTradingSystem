using GalaSoft.MvvmLight.Views;

namespace StockTradingSystem.Client.UI.Navigation
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}