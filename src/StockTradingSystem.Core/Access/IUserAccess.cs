namespace StockTradingSystem.Core.Access
{
    public interface IUserAccess
    {
        UserCreateResult User_create(string loginName, string passwd, string name, int type, decimal cnyFree);
        UserLoginResult User_login(string loginName, string passwd, out long? userId, out string name, out int? type);
        UserRepasswdResult User_repasswd(long userId, string oldPasswd, string newPasswd);
    }
}