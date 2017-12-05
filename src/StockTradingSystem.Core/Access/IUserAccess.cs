namespace StockTradingSystem.Core.Access
{
    public interface IUserAccess
    {
        bool User_create(string loginName, string passwd, string name, int type, decimal cnyFree);
        bool User_login(string loginName, string passwd, out long? userId, out string name, out int? type);
        bool User_repasswd(long userId, string oldPasswd, string newPasswd);
    }
}