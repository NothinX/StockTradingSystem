using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace StockTradingSystem.Client.Model.UI.Converter
{
    /// <inheritdoc />
    /// <summary>
    /// ActualWidth和Column的Wdith的属性转换器，用于ListView
    /// </summary>
    [ValueConversion(typeof(double), typeof(double), ParameterType = typeof(string))]
    public class ListViewColumnWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = parameter as string;
            Debug.Assert(s != null, nameof(s) + " != null");
            var d = double.Parse(s);
            Debug.Assert(value != null, nameof(value) + " != null");
            var v = System.Convert.ToDouble(value);
            return v / d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}