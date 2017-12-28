using System;
using System.Globalization;
using System.Windows.Data;
using StockTradingSystem.Client.Model.Info;

namespace StockTradingSystem.Client.Model.UI.Control
{
    /// <inheritdoc />
    /// <summary>
    /// IsChecked和StockInfo的属性转换器，用于StockView
    /// </summary>
    public class IsCheckedStockInfoConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (values[0] as StockInfo)?.StockId == (int)values[1]; 
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}