using GalaSoft.MvvmLight;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.Model
{
    public class GpUser : ObservableObject, IUser
    {
        /// <summary>
        /// The <see cref="UserId" /> property's name.
        /// </summary>
        public const string UserIdPropertyName = nameof(UserId);

        private long _userId;

        /// <summary>
        /// Sets and gets the <see cref="UserId"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                Set(UserIdPropertyName, ref _userId, value);
            }
        }

        /// <summary>
        /// The <see cref="LoginName" /> property's name.
        /// </summary>
        public const string LoginNamePropertyName = nameof(LoginName);

        private string _loginName;

        /// <summary>
        /// Sets and gets the <see cref="LoginName"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LoginName
        {
            get
            {
                return _loginName;
            }
            set
            {
                Set(LoginNamePropertyName, ref _loginName, value);
            }
        }

        /// <summary>
        /// The <see cref="Name"/> property's name.
        /// </summary>
        public const string NamePropertyName = nameof(Name);

        private string _name;

        /// <summary>
        /// Sets and gets the <see cref="Name"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(NamePropertyName, ref _name, value);
            }
        }

        /// <summary>
        /// The <see cref="Type" /> property's name.
        /// </summary>
        public const string TypePropertyName = nameof(Type);

        private int _type;

        /// <summary>
        /// Sets and gets the <see cref="Type"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                Set(TypePropertyName, ref _type, value);
            }
        }

        /// <summary>
        /// The <see cref="IsLogin" /> property's name.
        /// </summary>
        public const string IsLoginPropertyName = nameof(IsLogin);

        private bool _isLogin;

        /// <summary>
        /// Sets and gets the <see cref="IsLogin"/> property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                Set(IsLoginPropertyName, ref _isLogin, value);
            }
        }
    }
}