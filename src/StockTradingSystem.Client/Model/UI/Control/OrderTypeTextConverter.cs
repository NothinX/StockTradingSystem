using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace StockTradingSystem.Client.Model.UI.Control
{
    /// <inheritdoc />
    /// <summary>
    /// OrderType和Text的属性转换器，用于UserOrderInfoListView
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class OrderTypeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return (int)value == 0 ? "买入" : "卖出";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}