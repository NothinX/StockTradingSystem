using GalaSoft.MvvmLight;

namespace StockTradingSystem.Client.Model.Info
{
    public abstract class Info : ObservableObject
    {
        public abstract void Update();
    }
}