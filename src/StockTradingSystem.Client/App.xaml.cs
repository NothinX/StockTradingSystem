using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace StockTradingSystem.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 后台进程刷新间隔
        /// </summary>
        public static TimeSpan RefreshTimeSpan { get; set; }
        static App()
        {
            DispatcherHelper.Initialize();
            RefreshTimeSpan = new TimeSpan(0, 0, 1);
        }
    }
}
