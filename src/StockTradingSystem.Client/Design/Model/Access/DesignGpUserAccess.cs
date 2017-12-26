using StockTradingSystem.Core.Access;

namespace StockTradingSystem.Client.Design.Model.Access
{
    internal class DesignGpUserAccess : IUserAccess
    {
        public UserCreateResult User_create(string loginName, string passwd, string name, int type, decimal cnyFree)
        {
            throw new System.NotImplementedException();
        }

        public UserRepasswdResult User_repasswd(long userId, string oldPasswd, string newPasswd)
        {
            throw new System.NotImplementedException();
        }

        public UserLoginResult User_login(string loginName, string passwd, out long? userId, out string name, out int? type)
        {
            throw new System.NotImplementedException();
        }
    }
}