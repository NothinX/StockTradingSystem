using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using StockTradingSystem.Core.Access;

namespace StockTradingSystem.Client.Model.Access
{
    internal class GpUserAccess : IUserAccess
    {
        public UserCreateResult User_create(string loginName, string passwd, string name, int type, decimal cnyFree)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.user_create(loginName, passwd, name, type, cnyFree).First() == 0 ? UserCreateResult.Ok : UserCreateResult.Repeat;
            }
        }

        public UserLoginResult User_login(string loginName, string passwd, out long? userId, out string name, out int? type)
        {
            using (var gpEntities = new GPEntities())
            {
                userId = 0;
                name = "";
                type = -1;
                var r = new SqlParameter("@r", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var uid = new SqlParameter("@user_id", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                var n = new SqlParameter("@name", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                var t = new SqlParameter("@type", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var res = gpEntities.Database.SqlQuery<object>(
                    $"exec @r = dbo.user_login '{loginName}', '{passwd}', @user_id out, @name out, @type out", r, uid, n, t);
                var data = res.FirstOrDefault();
                userId = uid.Value as long?;
                name = n.Value as string;
                type = t.Value as int?;
                return (int)r.Value == 0 ? UserLoginResult.Ok : UserLoginResult.Wrong;
            }
        }

        public UserRepasswdResult User_repasswd(long userId, string oldPasswd, string newPasswd)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.user_repasswd(userId, oldPasswd, newPasswd).First() == 0 ? UserRepasswdResult.Ok : UserRepasswdResult.Wrong;
            }
        }
    }
}