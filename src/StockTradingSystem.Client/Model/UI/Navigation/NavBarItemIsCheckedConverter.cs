using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace StockTradingSystem.Client.Model.UI.Navigation
{
    /// <inheritdoc />
    /// <summary>
    /// string属性和IsChecked属性转换器，用于NavBar
    /// </summary>
    [ValueConversion(typeof(string), typeof(bool?))]
    public class NavBarItemIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as string == parameter as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}