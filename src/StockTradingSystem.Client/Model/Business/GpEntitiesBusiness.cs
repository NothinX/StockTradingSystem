using System.Data.Entity.Core.Objects;
using StockTradingSystem.Core.Access;

namespace StockTradingSystem.Client.Model.Business
{
    internal class GpEntitiesUserAccess : IUserAccess
    {
        public bool User_create(string loginName, string passwd, string name, int type, decimal cnyFree)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.user_create(loginName, passwd, name, type, cnyFree) >= 1;
            }
        }

        public bool User_login(string loginName, string passwd, out long? userId, out string name, out int? type)
        {
            using (var gpEntities = new GPEntities())
            {
                userId = 0;
                name = "";
                type = -1;
                var uid = new ObjectParameter("user_id", userId);
                var n = new ObjectParameter("name", name);
                var t = new ObjectParameter("type", type);
                var res = gpEntities.user_login(loginName, passwd, uid, n, t);
                userId = uid.Value as long?;
                name = n.Value as string;
                type = t.Value as int?;
                return res >= 1;
            }
        }

        public bool User_repasswd(long userId, string oldPasswd, string newPasswd)
        {
            using (var gpEntities = new GPEntities())
            {
                return gpEntities.user_repasswd(userId, oldPasswd, newPasswd) >= 1;
            }
        }
    }
}