using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using GalaSoft.MvvmLight.Ioc;
using StockTradingSystem.Client.ViewModel;

namespace StockTradingSystem.Client.Model.UI.Control
{
    /// <inheritdoc />
    /// <summary>
    /// 股票升跌状态和Brush的属性转换器，用于StockBtn
    /// </summary>
    [ValueConversion(typeof(bool?), typeof(Brush))]
    public class StockPriceChangeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value as bool?;
            switch (b)
            {
                case true:
                    return new SolidColorBrush(Color.FromArgb(255, 16, 124, 16));
                case null:
                    return SimpleIoc.Default.GetInstance<MainWindowModel>().ThemeBrush;
                default:
                    return new SolidColorBrush(Color.FromArgb(255, 232, 17, 35));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}