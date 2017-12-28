using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockTradingSystem.Client.Model.UI.Control
{
    /// <inheritdoc />
    /// <summary>
    /// Undealed和Visibility的属性转换器，用于UserOrderInfoListView
    /// </summary>
    [ValueConversion(typeof(int), typeof(Visibility), ParameterType = typeof(bool?))]
    public class UndealedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            var flag = (bool)(parameter ?? true);
            if (flag) return (int)value != 0 ? Visibility.Visible : Visibility.Collapsed;
            return (int)value != 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}