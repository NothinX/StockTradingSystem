using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using StockTradingSystem.Client.Model.Info;

namespace StockTradingSystem.Client.Model.UI.Converter
{
    /// <inheritdoc />
    /// <summary>
    /// 获取当前股票余额的属性转换器，用于TradeGrid
    /// </summary>
    public class StockBalanceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is StockInfo csi) || !(values[1] is List<UserStockInfo> usil)) return 0;
            var s = usil.FirstOrDefault(x => x.StockId == csi.StockId);
            return s?.AvailableStock ?? 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}