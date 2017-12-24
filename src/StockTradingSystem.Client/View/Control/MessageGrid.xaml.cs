using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StockTradingSystem.Client.ViewModel.Control;

namespace StockTradingSystem.Client.View.Control
{
    /// <summary>
    /// MessageGrid.xaml 的交互逻辑
    /// </summary>
    public partial class MessageGrid : UserControl
    {
        public MessageGrid()
        {
            InitializeComponent();
            Loaded += MessageGrid_Loaded;
        }

        private void MessageGrid_Loaded(object sender, RoutedEventArgs e)
        {
            OkBtn.Focus();
        }

        public static MessageGrid Show(Action<Guid, bool> dialogCallback, Guid guid, string message, string title, string okBtnText)
        {
            var mgvm = new MessageGridViewModel(dialogCallback, guid, message, title, okBtnText);
            var mg = new MessageGrid() { DataContext = mgvm };
            return mg;
        }

        public static MessageGrid Show(Action<Guid, bool> dialogCallback, Guid guid, string message, string title, string okBtnText, string cancelBtnText)
        {
            var mgvm = new MessageGridViewModel(dialogCallback, guid, message, title, okBtnText, cancelBtnText);
            var mg = new MessageGrid() { DataContext = mgvm };
            return mg;
        }
    }
}
