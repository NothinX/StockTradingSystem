using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.Model;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly StockAgent _stockAgent;
        private readonly MainWindowModel _mainWindowModel;

        public RegisterViewModel(IDialogService dialogService, StockAgent stockAgent, MainWindowModel mainWindowModel)
        {
            _dialogService = dialogService;
            _stockAgent = stockAgent;
            _mainWindowModel = mainWindowModel;
        }

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

        /// <summary>
        /// The <see cref="LoginNameFocus" /> property's name.
        /// </summary>
        public const string LoginNameFocusPropertyName = nameof(LoginNameFocus);

        private bool? _loginNameFocus = false;

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
        /// The <see cref="UserNameFocus" /> property's name.
        /// </summary>
        public const string UserNameFocusPropertyName = nameof(UserNameFocus);

        private bool? _userNameFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="UserNameFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? UserNameFocus
        {
            get => _userNameFocus;
            set => Set(UserNameFocusPropertyName, ref _userNameFocus, value, true);
        }

        /// <summary>
        /// The <see cref="LoginPasswordFocus" /> property's name.
        /// </summary>
        public const string LoginPasswordFocusPropertyName = nameof(LoginPasswordFocus);

        private bool? _loginPasswordFocus = false;

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

        /// <summary>
        /// The <see cref="SurePasswordFocus" /> property's name.
        /// </summary>
        public const string SurePasswordFocusPropertyName = nameof(SurePasswordFocus);

        private bool? _surePasswordFocus = false;

        /// <summary>
        /// Sets and gets the <see cref="SurePasswordFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? SurePasswordFocus
        {
            get => _surePasswordFocus;
            set => Set(SurePasswordFocusPropertyName, ref _surePasswordFocus, value, true);
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
            if (LoginNameText == "")
            {
                await _dialogService.ShowMessage("请输入用户名", "错误", "确定", () => LoginNameFocus = true);
            }
            else if (UserNameText == "")
            {
                await _dialogService.ShowMessage("请输入姓名", "错误", "确定", () => UserNameFocus = true);
            }
            else if (LoginPasswordText == "")
            {
                await _dialogService.ShowMessage("请输入密码", "错误", "确定", () => LoginPasswordFocus = true);
            }
            else if (SurePasswordText == "")
            {
                await _dialogService.ShowMessage("请确认密码", "错误", "确定", () => SurePasswordFocus = true);
            }
            else if (LoginPasswordText != SurePasswordText)
            {
                await _dialogService.ShowMessage("两次输入密码不同", "错误", "确定", () =>
                {
                    LoginPasswordText = "";
                    SurePasswordText = "";
                    LoginPasswordFocus = true;
                });
            }
            else
            {
                IUser user = new GpUser { LoginName = LoginNameText, Name = UserNameText, Type = 1 };
                try
                {
                    if (_stockAgent.User_create(user, LoginPasswordText, 10000) == UserCreateResult.Ok)
                    {
                        ClearAllText();
                        LoginNameFocus = true;
                        await _dialogService.ShowMessage("注册成功", "提示");
                        _mainWindowModel.NavigateCommand.Execute("LoginView");
                    }
                    else
                    {
                        LoginPasswordText = "";
                        SurePasswordText = "";
                        await _dialogService.ShowMessage("该用户已存在", "错误", "确定", () => LoginNameFocus = true);
                    }
                }
                catch (Exception e)
                {
                    LoginPasswordText = "";
                    SurePasswordText = "";
                    await _dialogService.ShowError(e, "错误", "确定", () => LoginNameFocus = true);
                }
            }
        }

        #endregion

        private void ClearAllText()
        {
            LoginNameText = "";
            UserNameText = "";
            LoginPasswordText = "";
            SurePasswordText = "";
        }

        public override void Cleanup()
        {
            base.Cleanup();
            ClearAllText();
        }
    }
}