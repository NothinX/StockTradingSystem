using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using StockTradingSystem.Client.Model.Info;
using StockTradingSystem.Client.ViewModel.Control;

namespace StockTradingSystem.Client.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        public static readonly string UpdateCurrentUserStockInfoToken = "UpdateCurrentUserStockInfoToken";

        private readonly UserStockInfoViewModel _userStockInfoViewModel;

        public AccountViewModel(UserStockInfoViewModel userStockInfoViewModel)
        {
            _userStockInfoViewModel = userStockInfoViewModel;
            Messenger.Default.Register<GenericMessage<int>>(this, UpdateCurrentUserStockInfoToken, i => UpdateCurrentUserStockInfo(i.Content));
        }

        public void UpdateCurrentUserStockInfo(int sid)
        {
            lock (this)
            {
                var usi = _userStockInfoViewModel.UserStockInfoList.FirstOrDefault(x => x.StockId == sid);
                if (usi != null) Set(CurrentUserStockInfoPropertyName, ref _currentUserStockInfo, usi, true);
            }
        }

        #region Property

        /// <summary>
        /// The <see cref="CurrentUserStockInfo" /> property's name.
        /// </summary>
        public const string CurrentUserStockInfoPropertyName = nameof(CurrentUserStockInfo);

        private UserStockInfo _currentUserStockInfo;

        /// <summary>
        /// Sets and gets the <see cref="CurrentUserStockInfo"/> property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// This property's value is broadcasted by the MessengerInstance when it changes.
        /// </summary>
        public UserStockInfo CurrentUserStockInfo
        {
            get => _currentUserStockInfo;
            set
            {
                Set(CurrentUserStockInfoPropertyName, ref _currentUserStockInfo, value, true);
                Messenger.Default.Send(new GenericMessage<int>(CurrentUserStockInfo.StockId), StockInfoViewModel.UpdateCurrentStockInfoToken);
            }
        }

        #endregion

        #region Command



        #endregion

    }
}