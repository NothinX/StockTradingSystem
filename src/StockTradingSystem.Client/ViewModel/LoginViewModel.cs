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
        /// The <see cref="LoginNameText" /> property's name.
        /// </summary>
        public const string UserNameTextPropertyName = nameof(LoginNameText);

        private string _loginNameText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="LoginNameText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string LoginNameText
        {
            get => _loginNameText;
            set => Set(UserNameTextPropertyName, ref _loginNameText, value, true);
        }

        /// <summary>
        /// The <see cref="LoginPasswordText" /> property's name.
        /// </summary>
        public const string PasswordTextPropertyName = nameof(LoginPasswordText);

        private string _loginPasswordText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="LoginPasswordText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string LoginPasswordText
        {
            get => _loginPasswordText;
            set => Set(PasswordTextPropertyName, ref _loginPasswordText, value, true);
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