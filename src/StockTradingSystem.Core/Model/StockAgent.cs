using System;
using System.Collections.Generic;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Business;

namespace StockTradingSystem.Core.Model
{
    public class StockAgent
    {
        private readonly IUserAccess _userAccess;
        private readonly IBusiness _business;
        public IUser User { get; set; }

        public StockAgent(IUserAccess userAccess, IBusiness business, IUser user)
        {
            _userAccess = userAccess ?? throw new ArgumentNullException(nameof(userAccess));
            _business = business ?? throw new ArgumentNullException(nameof(business));
            User = user;
        }

        public UserCreateResult User_create(IUser user, string passwd, decimal cnyFree)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(passwd))
            {
                throw new ArgumentException("密码不能为null、空或全是空格", nameof(passwd));
            }
            if (cnyFree < 0) throw new ArgumentOutOfRangeException(nameof(cnyFree), "初始账户余额应该大于等于零");
            if (user.Name == null) throw new ArgumentNullException(nameof(user.Name));

            return _userAccess.User_create(user.LoginName, passwd, user.Name, user.Type, cnyFree);
        }

        public UserLoginResult User_login(string passwd)
        {
            CheckUserLogin(true);
            if (string.IsNullOrWhiteSpace(passwd))
            {
                throw new ArgumentException("密码不能为null、空或全是空格", nameof(passwd));
            }

            var res = _userAccess.User_login(User.LoginName, passwd, out var userId, out var name, out var type);
            User.IsLogin = res == UserLoginResult.Ok;
            User.UserId = userId ?? 0;
            User.Name = name ?? "";
            User.Type = type ?? -1;
            return res;
        }

        public UserRepasswdResult User_repasswd(string oldPasswd, string newPasswd)
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

            return _userAccess.User_repasswd(User.UserId, oldPasswd, newPasswd);
        }

        public CancelOrderResult Cancel_Order(long orderId)
        {
            CheckUserLogin();
            return _business.Cancel_Order(User.UserId, orderId);
        }

        public ExecOrderResult Exec_Order(int stockId, int type, decimal price, int amount)
        {
            CheckUserLogin();
            return _business.Exec_Order(User.UserId, stockId, type, price, amount);
        }

        public List<StockDepthResult> Stock_depth(int stockId, int type)
        {
            CheckUserLogin();
            return _business.Stock_depth(stockId, type);
        }

        public UserCnyResult User_cny()
        {
            CheckUserLogin();
            return _business.User_cny(User.UserId);
        }

        public List<UserOrderResult> User_order()
        {
            CheckUserLogin();
            return _business.User_order(User.UserId);
        }

        public List<UserStockResult> User_stock()
        {
            CheckUserLogin();
            return _business.User_stock(User.UserId);
        }

        /// <summary>
        /// 检查User是否为空
        /// </summary>
        private void CheckUser()
        {
            if (User == null) throw new ArgumentNullException(nameof(User));
        }

        /// <summary>
        /// 检查当前登录状态
        /// </summary>
        /// <param name="checkFlag">目标状态</param>
        private void CheckUserLogin(bool checkFlag = false)
        {
            CheckUser();
            if (User.IsLogin == checkFlag && checkFlag)
            {
                throw new Exception("用户已经登录，无法进行当前操作");
            }

            if (User.IsLogin == checkFlag && !checkFlag)
            {
                throw new Exception("用户尚未登录，无法进行当前操作");
            }
        }
    }
}