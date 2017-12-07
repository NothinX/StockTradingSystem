using GalaSoft.MvvmLight.Views;namespace StockTradingSystem.Client.Model.UI.Navigation
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}