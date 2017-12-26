using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Client.ViewModel.Control;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly MainWindowModel _mainWindowModel;
        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;

        public LoginViewModel(MainWindowModel mainWindowModel, StockAgent stockAgent, IDialogService dialogService)
        {
            _mainWindowModel = mainWindowModel;
            _stockAgent = stockAgent;
            _dialogService = dialogService;
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
        /// The <see cref="LoginNameFocus" /> property's name.
        /// </summary>
        public const string LoginNameFocusPropertyName = nameof(LoginNameFocus);

        private bool? _loginNameFocus;

        /// <summary>
        /// Sets and gets the <see cref="LoginNameFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? LoginNameFocus
        {
            get => _loginNameFocus;
            set => Set(LoginNameFocusPropertyName, ref _loginNameFocus, value, true);
        }

        /// <summary>
        /// The <see cref="LoginPasswordFocus" /> property's name.
        /// </summary>
        public const string LoginPasswordFocusPropertyName = nameof(LoginPasswordFocus);

        private bool? _loginPasswordFocus;

        /// <summary>
        /// Sets and gets the <see cref="LoginPasswordFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? LoginPasswordFocus
        {
            get => _loginPasswordFocus;
            set => Set(LoginPasswordFocusPropertyName, ref _loginPasswordFocus, value, true);
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
                await _dialogService.ShowMessage("请输入用户名", "错误", "确定", () => LoginNameFocus = true);
            }
            else if (LoginPasswordText == "")
            {
                await _dialogService.ShowMessage("请输入密码", "错误", "确定", () => LoginPasswordFocus = true);
            }
            else
            {
                if (_stockAgent.User.LoginName != LoginNameText && _stockAgent.User.IsLogin)
                {
                    var flag = true;
                    await _dialogService.ShowMessage("当前你已经登录，需要先注销才能切换账号\n请问要注销吗？", "提示", "确定", "取消", b =>
                    {
                        if (!b) return;
                        _stockAgent.User.IsLogin = false;
                        Messenger.Default.Send(new GenericMessage<bool>(false), AccountButtonViewModel.UpdateUserMoneyInfo);
                        flag = false;
                    });
                    if (flag)
                    {
                        LoginPasswordText = "";
                        return;
                    }
                }
                _stockAgent.User.LoginName = LoginNameText;
                try
                {
                    if (_stockAgent.User_login(LoginPasswordText) == UserLoginResult.Ok)
                    {
                        LoginNameText = "";
                        LoginPasswordText = "";
                        LoginNameFocus = true;
                        Messenger.Default.Send(new GenericMessage<bool>(true), AccountButtonViewModel.UpdateUserMoneyInfo);
                        _mainWindowModel.NavigateCommand.Execute("StockView");
                    }
                    else
                    {
                        LoginNameText = "";
                        LoginPasswordText = "";
                        throw new Exception("账号或密码错误");
                    }
                }
                catch (Exception e)
                {
                    LoginPasswordText = "";
                    await _dialogService.ShowError(e, "错误", "确定", () => LoginNameFocus = true);
                }
            }
        }

        #endregion

        public override void Cleanup()
        {
            base.Cleanup();
            LoginNameText = "";
            LoginPasswordText = "";
        }
    }
}