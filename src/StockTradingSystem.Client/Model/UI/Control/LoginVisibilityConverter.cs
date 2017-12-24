using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockTradingSystem.Client.Model.UI.Control
{
    /// <inheritdoc />
    /// <summary>
    /// 登录状态和Visibility的属性转换器，用于AccountButton
    /// </summary>
    [ValueConversion(typeof(bool?), typeof(Visibility), ParameterType = typeof(bool?))]
    public class LoginVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as bool? ?? false) == (parameter as bool? ?? false) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}