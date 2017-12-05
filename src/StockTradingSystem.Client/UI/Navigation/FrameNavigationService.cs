using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using StockTradingSystem.Client.Properties;
using StockTradingSystem.Client.ViewModel;

namespace StockTradingSystem.Client.UI.Navigation
{
    /// <summary>
    /// The navigation message.
    /// </summary>
    public class FrameNavigationService : IFrameNavigationService
    {
        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly Stack<string> _historic;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameNavigationService"/> class.
        /// </summary>
        public FrameNavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new Stack<string>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the key corresponding to the currently displayed page.
        /// </summary>
        /// <value>
        /// The current page key.
        /// </value>
        public string CurrentPageKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public object Parameter { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// The go back.
        /// </summary>
        public void GoBack()
        {
            if (_historic.Count <= 1) return;
            _historic.Pop();
            Messenger.Default.Send(new GenericMessage<bool>(CanBack()),MainViewModel.Canback);
            NavigateTo(_historic.Pop(), null);
        }

        /// <summary>
        /// The can back.
        /// </summary>
        /// <returns>can back or not</returns>
        private bool CanBack()
        {
            return _historic.Count > 1;
        }

        /// <inheritdoc />
        /// <summary>
        /// The navigate to.
        /// </summary>
        /// <param name="pageKey">
        /// The page key.
        /// </param>
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        /// <inheritdoc />
        /// <summary>
        /// The navigate to.
        /// </summary>
        /// <param name="pageKey">
        /// The page key.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        string.Format(Resources.NoSuchPageException, pageKey), nameof(pageKey));
                }

                // Set the frame source, which initiates navigation
                if (Application.Current.MainWindow.GetDescendantFromName("MainFrame") is Frame frame)
                {
                    frame.Source = _pagesByKey[pageKey];
                }
                Parameter = parameter;
                _historic.Push(pageKey);
                Messenger.Default.Send(new GenericMessage<bool>(CanBack()),MainViewModel.Canback);
                CurrentPageKey = pageKey;
            }
        }

        /// <summary>
        /// Configures the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pageType">Type of the page.</param>
        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = pageType;
                }
                else
                {
                    _pagesByKey.Add(key, pageType);
                }
            }
        }
    }
}