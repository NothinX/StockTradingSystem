using System;
using System.Security.Cryptography;
using System.Text;

namespace StockTradingSystem.Core.Model
{
    public class Security
    {
        public static string Pbkdf2(string passwd)
        {
            var salt = Encoding.Unicode.GetBytes("😂😘😍😊😁😭😜");
            var r = new Rfc2898DeriveBytes(passwd, salt, 8192);
            return BitConverter.ToString(r.GetBytes(127)).Replace("-", "");
        }
    }
}