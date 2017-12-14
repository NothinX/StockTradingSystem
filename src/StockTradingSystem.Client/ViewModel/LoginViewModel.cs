using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace StockTradingSystem.Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Property

        /// <summary>
        /// The <see cref="UserNameText" /> property's name.
        /// </summary>
        public const string UserNameTextPropertyName = nameof(UserNameText);

        private string _userNameText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="UserNameText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string UserNameText
        {
            get => _userNameText;
            set => Set(UserNameTextPropertyName, ref _userNameText, value, true);
        }

        /// <summary>
        /// The <see cref="PasswordText" /> property's name.
        /// </summary>
        public const string PasswordTextPropertyName = nameof(PasswordText);

        private string _passwordText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="PasswordText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string PasswordText
        {
            get => _passwordText;
            set => Set(PasswordTextPropertyName, ref _passwordText, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _loginCommand;

        /// <summary>
        /// Gets the <see cref="LoginCommand"/>.
        /// </summary>
        public RelayCommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(
                                                ExecuteLoginCommand));

        private async void ExecuteLoginCommand()
        {
            var r = new Random();
            var i = r.Next(2);
            if (i == 0)
            {
                await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage("登录失败", "错误");
            }
            else
            {
                await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage("登录成功", "提示");
            }
        }

        #endregion
    }
}