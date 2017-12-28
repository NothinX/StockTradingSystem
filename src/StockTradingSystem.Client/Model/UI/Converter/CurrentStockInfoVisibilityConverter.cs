using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using StockTradingSystem.Client.Model.Info;

namespace StockTradingSystem.Client.Model.UI.Converter
{
    /// <inheritdoc />
    /// <summary>
    /// CurrentStockInfo和Visibility的属性转换器，用于TradeView
    /// </summary>
    [ValueConversion(typeof(StockInfo),typeof(Visibility), ParameterType = typeof(bool?))]
    public class CurrentStockInfoVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = (bool)(parameter ?? true);
            if (flag) return value is StockInfo ? Visibility.Visible : Visibility.Collapsed;
            return value is StockInfo ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}