//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockTradingSystem.Client.Model
{
    using System;
    
    public partial class user_order_Result
    {
        public long order_id { get; set; }
        public System.DateTime create_datetime { get; set; }
        public long user_id { get; set; }
        public int stock_id { get; set; }
        public int type { get; set; }
        public decimal price { get; set; }
        public int undealed { get; set; }
        public int dealed { get; set; }
        public int canceled { get; set; }
    }
}
