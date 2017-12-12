using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class AccountButtonViewModel : ViewModelBase
    {
        #region Property

        /// <summary>
        /// The <see cref="BeforeLoginVisibility" /> property's name.
        /// </summary>
        public const string BeforeLoginIsEnabledPropertyName = nameof(BeforeLoginVisibility);

        private Visibility _beforeLoginVisibility = Visibility.Visible;

        /// <summary>
        /// Sets and gets the <see cref="BeforeLoginVisibility"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Visibility BeforeLoginVisibility
        {
            get => _beforeLoginVisibility;
            set => Set(BeforeLoginIsEnabledPropertyName, ref _beforeLoginVisibility, value, true);
        }

        /// <summary>
        /// The <see cref="AfterLoginVisibility" /> property's name.
        /// </summary>
        public const string AfterLoginIsEnabledPropertyName = nameof(AfterLoginVisibility);

        private Visibility _afterLoginVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the <see cref="AfterLoginVisibility"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Visibility AfterLoginVisibility
        {
            get => _afterLoginVisibility;
            set => Set(AfterLoginIsEnabledPropertyName, ref _afterLoginVisibility, value, true);
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
        /// The <see cref="TotalMoneyText" /> property's name.
        /// </summary>
        public const string TotalMoneyTextPropertyName = nameof(TotalMoneyText);

        private string _totalMoneyText = "总：";

        /// <summary>
        /// Sets and gets the <see cref="TotalMoneyText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string TotalMoneyText
        {
            get => _totalMoneyText;
            set => Set(TotalMoneyTextPropertyName, ref _totalMoneyText, value, true);
        }

        /// <summary>
        /// The <see cref="AvailableMoneyText" /> property's name.
        /// </summary>
        public const string AvailableMoneyTextPropertyName = nameof(AvailableMoneyText);

        private string _availableMoneyText = "可用：";

        /// <summary>
        /// Sets and gets the <see cref="AvailableMoneyText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string AvailableMoneyText
        {
            get => _availableMoneyText;
            set => Set(AvailableMoneyTextPropertyName, ref _availableMoneyText, value, true);
        }

        /// <summary>
        /// The <see cref="IsChecked" /> property's name.
        /// </summary>
        public const string IsCheckedPropertyName = nameof(IsChecked);

        private bool _isChecked;

        /// <summary>
        /// Sets and gets the <see cref="IsChecked"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set => Set(IsCheckedPropertyName, ref _isChecked, value, true);
        }

        #endregion

        #region Command

        private RelayCommand _logoutCommand;

        /// <summary>
        /// Gets the <see cref="LogoutCommand"/>.
        /// </summary>
        public RelayCommand LogoutCommand => _logoutCommand
                                             ?? (_logoutCommand = new RelayCommand(ExecuteLogoutCommand));

        private void ExecuteLogoutCommand()
        {

        }

        private RelayCommand _switchCommand;

        /// <summary>
        /// Gets the <see cref="SwitchCommand"/>.
        /// </summary>
        public RelayCommand SwitchCommand => _switchCommand
                                             ?? (_switchCommand = new RelayCommand(ExecuteSwitchCommand));

        private void ExecuteSwitchCommand()
        {

        }

        #endregion
    }
}