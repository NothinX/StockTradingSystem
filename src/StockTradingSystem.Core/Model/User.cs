using System;

namespace StockTradingSystem.Core.Model
{
    public sealed class User
    {
        public long UserId { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsLogin { get; set; } = false;

        public User(string loginName)
        {
            LoginName = loginName ?? throw new ArgumentNullException(nameof(loginName));
        }
    }
}