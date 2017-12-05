using System;
using System.Collections.Generic;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Business;

namespace StockTradingSystem.Core.Model
{
    public sealed class StockAgent
    {
        private readonly IUserAccess _userAccess;
        private readonly IBusiness _business;
        private readonly User _user;

        public StockAgent(IUserAccess userAccess, IBusiness business, User user)
        {
            _userAccess = userAccess ?? throw new ArgumentNullException(nameof(userAccess));
            _business = business ?? throw new ArgumentNullException(nameof(business));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public bool User_create(string passwd, decimal cnyFree)
        {
            if (string.IsNullOrWhiteSpace(passwd))
            {
                throw new ArgumentException("密码不能为null、空或全是空格", nameof(passwd));
            }
            if (cnyFree < 0) throw new ArgumentOutOfRangeException(nameof(cnyFree), "初始账户余额应该大于等于零");
            if (_user.Name == null) throw new ArgumentNullException(nameof(_user.Name));

            return _userAccess.User_create(_user.LoginName, passwd, _user.Name, _user.Type, cnyFree);
        }

        public bool User_login(string passwd)
        {
            CheckUserLogin();
            if (string.IsNullOrWhiteSpace(passwd))
            {
                throw new ArgumentException("密码不能为null、空或全是空格", nameof(passwd));
            }

            var res = _userAccess.User_login(_user.LoginName, passwd, out var userId, out var name, out var type);
            if (!res) return false;
            _user.UserId = userId ?? 0;
            _user.Name = name ?? "";
            _user.Type = type ?? -1;
            return true;
        }

        public bool User_repasswd(string oldPasswd, string newPasswd)
        {
            CheckUserLogin();
            if (string.IsNullOrWhiteSpace(oldPasswd))
            {
                throw new ArgumentException("密码不能为null、空或全是空格", nameof(oldPasswd));
            }

            if (string.IsNullOrWhiteSpace(newPasswd))
            {
                throw new ArgumentException("密码不能为null、空或全是空格", nameof(newPasswd));
            }

            return _userAccess.User_repasswd(_user.UserId, oldPasswd, newPasswd);
        }

        public bool Cancel_Order(long orderId)
        {
            CheckUserLogin();
            return _business.Cancel_Order(_user.UserId, orderId);
        }

        public bool Exec_Order(int stockId, int type, decimal price, int amount)
        {
            CheckUserLogin();
            return _business.Exec_Order(_user.UserId, stockId, type, price, amount);
        }

        public List<StockDepthResult> Stock_depth(int stockId, int type)
        {
            CheckUserLogin();
            var res = _business.Stock_depth(stockId, type, out var stockDepthResult);
            return res ? stockDepthResult : null;
        }

        public UserCnyResult User_cny(long userId)
        {
            CheckUserLogin();
            var res = _business.User_cny(_user.UserId, out var cnyFree, out var cnyFreezed, out var gpMoney);
            return res ? new UserCnyResult(cnyFree ?? 0, cnyFreezed ?? 0, gpMoney ?? 0) : null;
        }

        public List<UserOrderResult> User_order(long userId)
        {
            CheckUserLogin();
            var res = _business.User_order(_user.UserId, out var userOrderResult);
            return res ? userOrderResult : null;
        }

        public List<UserStockResult> User_stock(long userId)
        {
            CheckUserLogin();
            var res = _business.User_stock(_user.UserId, out var userStockResult);
            return res ? userStockResult : null;
        }

        private void CheckUserLogin()
        {
            throw _user.IsLogin ? new Exception("用户已经登录，无法进行当前操作") : new Exception("用户尚未登录，无法进行当前操作");
        }
    }
}