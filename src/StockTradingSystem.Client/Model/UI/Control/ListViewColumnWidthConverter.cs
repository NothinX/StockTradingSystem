using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace StockTradingSystem.Client.Model.UI.Control
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

            var d = double.Parse(parameter as string ?? throw new InvalidOperationException());
            Debug.Assert(value != null, nameof(value) + " != null");
            return (double) value / d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}