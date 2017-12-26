namespace StockTradingSystem.Core.Access
{
    public interface IUserAccess
    {
        UserCreateResult User_create(string loginName, string passwd, string name, int type, decimal cnyFree);
        UserRepasswdResult User_repasswd(long userId, string oldPasswd, string newPasswd);
        UserLoginResult User_login(string loginName, string passwd,out long? userId, out string name, out int? type);
    }
}