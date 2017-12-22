using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly GpStockAgent _gpStockAgent;
        private readonly IDialogService _iDialogService;

        public LoginViewModel(GpStockAgent gpStockAgent, IDialogService iDialogService)
        {
            _gpStockAgent = gpStockAgent;
            _iDialogService = iDialogService;
        }

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
        public const string LoginPasswordTextPropertyName = nameof(LoginPasswordText);

        private string _loginPasswordText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="LoginPasswordText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string LoginPasswordText
        {
            get => _loginPasswordText;
            set => Set(LoginPasswordTextPropertyName, ref _loginPasswordText, value, true);
        }

        /// <summary>
        /// The <see cref="LoginPasswordCharText" /> property's name.
        /// </summary>
        public const string LoginPasswordCharTextPropertyName = nameof(LoginPasswordCharText);

        private string _loginPasswordCharText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="LoginPasswordCharText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string LoginPasswordCharText
        {
            get => _loginPasswordCharText;
            set => Set(LoginPasswordCharTextPropertyName, ref _loginPasswordCharText, value, true);
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
            if (LoginNameText == "")
            {
                await _iDialogService.ShowMessage("请输入用户名", "错误");
            }
            else if (LoginPasswordText == "")
            {
                await _iDialogService.ShowMessage("请输入密码", "错误");
            }
            else
            {
                if (_gpStockAgent.User == null || _gpStockAgent.User.LoginName != LoginNameText) _gpStockAgent.User = new User(LoginNameText);
                try
                {
                    if (_gpStockAgent.User_login(LoginPasswordText) == UserLoginResult.Ok)
                    {
                        await _iDialogService.ShowMessage("登录成功", "提示");
                        _gpStockAgent.User.IsLogin = true;
                    }
                    else
                    {
                        LoginPasswordCharText = "";
                        LoginPasswordText = "";
                        throw new Exception("账号或密码错误");
                    }
                }
                catch (Exception e)
                {
                    await _iDialogService.ShowError(e, "错误", "确定", null);
                }
            }
        }

        #endregion

        public override void Cleanup()
        {
            base.Cleanup();
            LoginNameText = "";
            LoginPasswordCharText = "";
        }
    }
}