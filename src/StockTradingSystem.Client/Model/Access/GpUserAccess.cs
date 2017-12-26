using System.Linq;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Model;
using StockTradingSystem.Core.Security;

namespace StockTradingSystem.Client.Model.Access
{
    internal class GpUserAccess : IUserAccess
    {
        public UserCreateResult User_create(string loginName, string passwd, string name, int type, decimal cnyFree)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.user_create(loginName, Crypto.HashPassword(passwd), name, type, cnyFree).First() == 0 ? UserCreateResult.Ok : UserCreateResult.Repeat;
            }
        }

        public UserRepasswdResult User_repasswd(long userId, string oldPasswd, string newPasswd)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.user_repasswd(userId, oldPasswd, newPasswd).First() == 0 ? UserRepasswdResult.Ok : UserRepasswdResult.Wrong;
            }
        }

        public UserLoginResult User_login(string loginName, string passwd, out long? userId, out string name, out int? type)
        {
            userId = GetUserId(loginName);
            name = null;
            type = null;
            if (userId == null) return UserLoginResult.Wrong;
            var hashedPassword = GetHashedPassword(userId.Value);
            if (hashedPassword == null) return UserLoginResult.Wrong;
            if (!Crypto.VerifyHashedPassword(hashedPassword, passwd)) return UserLoginResult.Wrong;
            GetUserInfo(userId.Value, out name, out type);
            return UserLoginResult.Ok;

        }

        private static long? GetUserId(string loginName)
        {
            using (var gpEntities = new GPEntities())
            {
                var q = gpEntities.users.FirstOrDefault(u => u.login_name == loginName);
                return q?.user_id;
            }
        }

        private static string GetHashedPassword(long userId)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.users.FirstOrDefault(u => u.user_id == userId)?.passwd;
            }
        }

        private static void GetUserInfo(long userId, out string name, out int? type)
        {
            using (var gpEntities = new GPEntities())
            {
                var q = gpEntities.users.FirstOrDefault(u => u.user_id == userId);
                if (q != null)
                {
                    name = q.name;
                    type = q.type;
                }
                else
                {
                    name = null;
                    type = null;
                }
            }
        }
    }
}