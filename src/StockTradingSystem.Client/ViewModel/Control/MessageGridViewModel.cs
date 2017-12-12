using System;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;

namespace StockTradingSystem.Client.ViewModel.Control
{
    public class MessageGridViewModel : ViewModelBase
    {
        private readonly Action<Guid, bool> _dialogCallback;
        private readonly Guid _dialogGuid;

        internal MessageGridViewModel()
        {

        }

        public MessageGridViewModel(Action<Guid, bool> dialogCallback, Guid dialogGuid, string messageText, string titleText, string okBtnText = "确定")
        {
            _dialogCallback = dialogCallback;
            _dialogGuid = dialogGuid;
            _messageText = messageText;
            _titleText = titleText;
            _okBtnText = okBtnText;
            SetBorderBrush();
        }

        public MessageGridViewModel(Action<Guid, bool> dialogCallback, Guid dialogGuid, string messageText, string titleText, string okBtnText = "确定", string cancelBtnText = "取消")
        {
            _dialogCallback = dialogCallback;
            _dialogGuid = dialogGuid;
            _messageText = messageText;
            _titleText = titleText;
            _okBtnText = okBtnText;
            _cancelBtnText = cancelBtnText;
            _cancelBtnVisibility = Visibility.Visible;
            SetBorderBrush();
        }

        private void SetBorderBrush()
        {
            BorderBrush = _titleText == "错误" ? new SolidColorBrush(Color.FromArgb(255, 232, 17, 35)) : SimpleIoc.Default.GetInstance<MainWindowModel>().ThemeBrush;
        }

        #region Property

        /// <summary>
        /// The <see cref="BorderBrush" /> property's name.
        /// </summary>
        public const string BorderBrushPropertyName = nameof(BorderBrush);

        private Brush _borderBrush = SimpleIoc.Default.GetInstance<MainWindowModel>().ThemeBrush;

        /// <summary>
        /// Sets and gets the <see cref="BorderBrush"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Brush BorderBrush
        {
            get => _borderBrush;
            set => Set(BorderBrushPropertyName, ref _borderBrush, value, true);
        }

        /// <summary>
        /// The <see cref="TitleText" /> property's name.
        /// </summary>
        public const string TitleTextPropertyName = nameof(TitleText);

        private string _titleText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="TitleText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string TitleText
        {
            get => _titleText;
            set => Set(TitleTextPropertyName, ref _titleText, value, true);
        }

        /// <summary>
        /// The <see cref="MessageText" /> property's name.
        /// </summary>
        public const string MessageTextPropertyName = nameof(MessageText);

        private string _messageText = string.Empty;

        /// <summary>
        /// Sets and gets the <see cref="MessageText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string MessageText
        {
            get => _messageText;
            set => Set(MessageTextPropertyName, ref _messageText, value, true);
        }

        /// <summary>
        /// The <see cref="OkBtnText" /> property's name.
        /// </summary>
        public const string OkBtnTextPropertyName = nameof(OkBtnText);

        private string _okBtnText = "确定";

        /// <summary>
        /// Sets and gets the <see cref="OkBtnText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string OkBtnText
        {
            get => _okBtnText;
            set => Set(OkBtnTextPropertyName, ref _okBtnText, value, true);
        }

        /// <summary>
        /// The <see cref="CancelBtnText" /> property's name.
        /// </summary>
        public const string CancelBtnTextPropertyName = nameof(CancelBtnText);

        private string _cancelBtnText = "取消";

        /// <summary>
        /// Sets and gets the <see cref="CancelBtnText"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public string CancelBtnText
        {
            get => _cancelBtnText;
            set => Set(CancelBtnTextPropertyName, ref _cancelBtnText, value, true);
        }

        /// <summary>
        /// The <see cref="CancelBtnVisibility" /> property's name.
        /// </summary>
        public const string CancelBtnVisibilityPropertyName = nameof(CancelBtnVisibility);

        private Visibility _cancelBtnVisibility = Visibility.Hidden;

        /// <summary>
        /// Sets and gets the <see cref="CancelBtnVisibility"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public Visibility CancelBtnVisibility
        {
            get => _cancelBtnVisibility;
            set => Set(CancelBtnVisibilityPropertyName, ref _cancelBtnVisibility, value, true);
        }

        #endregion

        #region RelayCommand

        private RelayCommand _okCommand;

        /// <summary>
        /// Gets the <see cref="OkCommand"/>.
        /// </summary>
        public RelayCommand OkCommand => _okCommand
                                         ?? (_okCommand = new RelayCommand(ExecuteOkCommand));

        private void ExecuteOkCommand()
        {
            _dialogCallback(_dialogGuid, true);
        }

        private RelayCommand _canceCommand;

        /// <summary>
        /// Gets the <see cref="CancelCommand"/>.
        /// </summary>
        public RelayCommand CancelCommand => _canceCommand
                    ?? (_canceCommand = new RelayCommand(ExecuteCancelCommand));

        private void ExecuteCancelCommand()
        {
            _dialogCallback(_dialogGuid, false);
        }

        #endregion
    }
}