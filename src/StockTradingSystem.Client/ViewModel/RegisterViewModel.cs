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