using GalaSoft.MvvmLight;

namespace StockTradingSystem.Client.Model.Info
{
    public abstract class Info<T> : ObservableObject
    {
        public abstract void Create(T obj);
        public abstract void Update(T obj);
    }
}