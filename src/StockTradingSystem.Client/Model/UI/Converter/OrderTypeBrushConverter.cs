using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StockTradingSystem.Client.Model.UI.Converter
{

    /// <inheritdoc />
    /// <summary>
    /// 交易类型和Brush的属性转换器，用于StockBtn
    /// </summary>
    [ValueConversion(typeof(int), typeof(Brush))]
    public class OrderTypeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            var b = (int)value;
            switch (b)
            {
                case 0:
                    return new SolidColorBrush(Color.FromArgb(255, 16, 124, 16));
                case 1:
                    return new SolidColorBrush(Color.FromArgb(255, 232, 17, 35));
                default:
                    return new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}