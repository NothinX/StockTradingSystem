using System;
using System.Globalization;
using System.Windows.Data;

namespace StockTradingSystem.Client.Model.UI.Converter
{
    /// <inheritdoc />
    /// <summary>
    /// 股票升跌状态和Text的属性转换器，用于StockBtn
    /// </summary>
    [ValueConversion(typeof(bool?),typeof(string))]
    public class StockPriceChangeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value as bool?;
            switch (b)
            {
                case true:
                    return "+";
                case null:
                    return " ";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}