using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.ViewModel.Control;
using StockTradingSystem.Core.Access;
using StockTradingSystem.Core.Model;

namespace StockTradingSystem.Client.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly StockAgent _stockAgent;
        private readonly IDialogService _dialogService;
        private readonly MainWindowModel _mainWindowModel;

        public SettingsViewModel(StockAgent stockAgent, IDialogService dialogService, MainWindowModel mainWindowModel)
        {
            _stockAgent = stockAgent;
            _dialogService = dialogService;
            _mainWindowModel = mainWindowModel;
            RValue = Convert.ToDouble(_mainWindowModel.ThemeBrush.Color.R);
            GValue = Convert.ToDouble(_mainWindowModel.ThemeBrush.Color.G);
            BValue = Convert.ToDouble(_mainWindowModel.ThemeBrush.Color.B);
        }

        #region Property

        /// <summary>
        /// The <see cref="OldPasswordText" /> property's name.
        /// </summary>
        public const string OldPasswordTextPropertyName = nameof(OldPasswordText);

        private string _oldPasswordText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="OldPasswordText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string OldPasswordText
        {
            get => _oldPasswordText;
            set => Set(OldPasswordTextPropertyName, ref _oldPasswordText, value, true);
        }

        /// <summary>
        /// The <see cref="NewPasswordText" /> property's name.
        /// </summary>
        public const string NewPasswordTextPropertyName = nameof(NewPasswordText);

        private string _newPasswordText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="NewPasswordText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string NewPasswordText
        {
            get => _newPasswordText;
            set => Set(NewPasswordTextPropertyName, ref _newPasswordText, value, true);
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
        /// The <see cref="OldPasswordFocus" /> property's name.
        /// </summary>
        public const string OldPasswordFocusPropertyName = nameof(OldPasswordFocus);

        private bool? _oldPasswordFocus;

        /// <summary>
        /// Sets and gets the <see cref="OldPasswordFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? OldPasswordFocus
        {
            get => _oldPasswordFocus;
            set => Set(OldPasswordFocusPropertyName, ref _oldPasswordFocus, value, true);
        }

        /// <summary>
        /// The <see cref="NewPasswordFocus" /> property's name.
        /// </summary>
        public const string NewPasswordFocusPropertyName = nameof(NewPasswordFocus);

        private bool? _newPasswordFocus;

        /// <summary>
        /// Sets and gets the <see cref="NewPasswordFocus"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool? NewPasswordFocus
        {
            get => _newPasswordFocus;
            set => Set(NewPasswordFocusPropertyName, ref _newPasswordFocus, value, true);
        }

        /// <summary>
        /// The <see cref="SurePasswordFocus" /> property's name.
        /// </summary>
        public const string SurePasswordFocusPropertyName = nameof(SurePasswordFocus);

        private bool? _surePasswordFocus;

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

        /// <summary>
        /// The <see cref="RValue" /> property's name.
        /// </summary>
        public const string RValuePropertyName = nameof(RValue);

        private double _rValue;

        /// <summary>
        /// Sets and gets the <see cref="RValue"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public double RValue
        {
            get => _rValue;
            set => Set(RValuePropertyName, ref _rValue, value, true);
        }

        /// <summary>
        /// The <see cref="GValue" /> property's name.
        /// </summary>
        public const string GValuePropertyName = nameof(GValue);

        private double _gValue;

        /// <summary>
        /// Sets and gets the <see cref="GValue"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public double GValue
        {
            get => _gValue;
            set => Set(GValuePropertyName, ref _gValue, value, true);
        }

        /// <summary>
        /// The <see cref="BValue" /> property's name.
        /// </summary>
        public const string BValuePropertyName = nameof(BValue);

        private double _bValue;

        /// <summary>
        /// Sets and gets the <see cref="BValue"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public double BValue
        {
            get => _bValue;
            set => Set(BValuePropertyName, ref _bValue, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _modifyCommand;

        /// <summary>
        /// Gets the <see cref="ModifyCommand"/>.
        /// </summary>
        public RelayCommand ModifyCommand => _modifyCommand ?? (_modifyCommand = new RelayCommand(
                                                 ExecuteModifyCommand));

        private async void ExecuteModifyCommand()
        {
            if (OldPasswordText == "")
            {
                await _dialogService.ShowMessage("请输入旧密码", "错误", "确定", () => OldPasswordFocus = true);

            }
            else if (NewPasswordText == "")
            {
                await _dialogService.ShowMessage("请输入新密码", "错误", "确定", () => NewPasswordFocus = true);
            }
            else if (SurePasswordText == "")
            {
                await _dialogService.ShowMessage("请确认新密码", "错误", "确定", () => SurePasswordFocus = true);
            }
            else if (NewPasswordText != SurePasswordText)
            {
                await _dialogService.ShowMessage("两次输入密码不同", "错误", "确定", () =>
                {
                    ClearAllText();
                    OldPasswordFocus = true;
                });
            }
            else
            {
                try
                {
                    if (_stockAgent.User_repasswd(OldPasswordText, NewPasswordText) == UserRepasswdResult.Ok)
                    {
                        ClearAllText();
                        OldPasswordFocus = true;
                        await _dialogService.ShowMessage("修改成功\n点击确定可跳转至登录页面重新登录", "提示", "确定", "取消", b =>
                        {
                            _stockAgent.User.IsLogin = false;
                            Messenger.Default.Send(new GenericMessage<bool>(false), AccountButtonViewModel.UpdateUserMoneyInfo);
                            if (b) _mainWindowModel.NavigateCommand.Execute("LoginView");
                        });
                    }
                    else
                    {
                        ClearAllText();
                        await _dialogService.ShowMessage("旧密码错误，修改失败", "错误", "确定", () => OldPasswordFocus = true);
                    }
                }
                catch (Exception e)
                {
                    ClearAllText();
                    await _dialogService.ShowError(e, "错误", "确定", () => OldPasswordFocus = true);
                }
            }
        }

        private RelayCommand _valueChangedCommand;

        /// <summary>
        /// Gets the ValueChangedCommand.
        /// </summary>
        public RelayCommand ValueChangedCommand => _valueChangedCommand
                                                   ?? (_valueChangedCommand = new RelayCommand(ExecuteValueChangedCommand));

        private void ExecuteValueChangedCommand()
        {
            var r = Convert.ToByte(RValue);
            var g = Convert.ToByte(GValue);
            var b = Convert.ToByte(BValue);
            _mainWindowModel.ThemeBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        private RelayCommand _saveThemeColorCommand;

        /// <summary>
        /// Gets the <see cref="SaveThemeColorCommand"/>.
        /// </summary>
        public RelayCommand SaveThemeColorCommand => _saveThemeColorCommand
                                                     ?? (_saveThemeColorCommand = new RelayCommand(ExecuteSaveThemeColorCommand));

        private async void ExecuteSaveThemeColorCommand()
        {
            try
            {
                var cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var tc = cfa.AppSettings.Settings["ThemeColorRGB"];
                if (tc == null) cfa.AppSettings.Settings.Add(new KeyValueConfigurationElement("ThemeColorRGB", $"{Convert.ToByte(RValue)}#{Convert.ToByte(GValue)}#{Convert.ToByte(BValue)}"));
                else tc.Value = $"{Convert.ToByte(RValue)}#{Convert.ToByte(GValue)}#{Convert.ToByte(BValue)}";
                cfa.Save();
                await _dialogService.ShowMessage("保存成功", "提示");
            }
            catch (Exception e)
            {
                await _dialogService.ShowError($"保存失败\n{e.Message}", "错误", "确定", null);
            }
        }

        private RelayCommand _defaultThemeColorCommand;

        /// <summary>
        /// Gets the <see cref="DefaultThemeColorCommand"/>.
        /// </summary>
        public RelayCommand DefaultThemeColorCommand => _defaultThemeColorCommand
                                                        ?? (_defaultThemeColorCommand = new RelayCommand(ExecuteDefaultThemeColorCommand));

        private void ExecuteDefaultThemeColorCommand()
        {
            RValue = 0;
            GValue = 99;
            BValue = 177;
        }

        #endregion

        private void ClearAllText()
        {
            OldPasswordText = "";
            NewPasswordText = "";
            SurePasswordText = "";
        }
    }
}