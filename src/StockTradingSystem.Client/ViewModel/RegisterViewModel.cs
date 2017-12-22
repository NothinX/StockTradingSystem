using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace StockTradingSystem.Client.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        #region Property

        /// <summary>
        /// The <see cref="LoginNameText" /> property's name.
        /// </summary>
        public const string LoginNameTextPropertyName = nameof(LoginNameText);

        private string _loginNameText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="LoginNameText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string LoginNameText
        {
            get => _loginNameText;
            set => Set(LoginNameTextPropertyName, ref _loginNameText, value, true);
        }

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

        /// <summary>
        /// The <see cref="SurePasswordText" /> property's name.
        /// </summary>
        public const string SurePasswordTextPropertyName = nameof(SurePasswordText);

        private string _surePasswordText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="SurePasswordText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string SurePasswordText
        {
            get => _surePasswordText;
            set => Set(SurePasswordTextPropertyName, ref _surePasswordText, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _registerCommand;

        /// <summary>
        /// Gets the <see cref="RegisterCommand"/>.
        /// </summary>
        public RelayCommand RegisterCommand => _registerCommand ?? (_registerCommand = new RelayCommand(
                                                ExecuteRegisterCommand));

        private async void ExecuteRegisterCommand()
        {
            var r = new Random();
            var i = r.Next(2);
            if (i == 0)
            {
                await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage("注册失败", "错误");
            }
            else
            {
                await SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage("注册成功", "提示");
            }
        }

        #endregion
    }
}