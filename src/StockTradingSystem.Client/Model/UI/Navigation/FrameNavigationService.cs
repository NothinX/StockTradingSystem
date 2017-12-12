using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using StockTradingSystem.Client.Properties;

namespace StockTradingSystem.Client.Model.UI.Navigation
{
    /// <summary>
    /// The navigation message.
    /// </summary>
    public class FrameNavigationService : IFrameNavigationService
    {
        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly List<string> _historic;

        private Frame _mainFrame;
        private Frame MainFrame => _mainFrame ?? (_mainFrame =
                                        Application.Current.MainWindow.GetDescendantFromName("MainFrame") as Frame);

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameNavigationService"/> class.
        /// </summary>
        public FrameNavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new List<string>();
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
            _historic.Remove(_historic.Last());
            NavigateTo(_historic.Last(), null);
        }

        /// <summary>
        /// The can back.
        /// </summary>
        /// <returns>can back or not</returns>
        public bool CanBack()
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

                MainFrame.Source = _pagesByKey[pageKey];

                Parameter = parameter;
                if (CurrentPageKey != pageKey)
                {
                    for (var i = 0; i < _historic.Count; i++)
                    {
                        if (_historic[i] == pageKey)
                        {
                            _historic.RemoveAt(i);
                        }
                    }
                    _historic.Add(pageKey);
                }
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