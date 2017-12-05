using System.Windows;
using System.Windows.Media;

namespace StockTradingSystem.Client.UI
{
    public static class UIHelper
    {
        /// <summary>
        /// Gets the name of the descendant from.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns>The FrameworkElement.</returns>
        public static FrameworkElement GetDescendantFromName(this DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                if (!(VisualTreeHelper.GetChild(parent, i) is FrameworkElement frameworkElement)) continue;
                if (frameworkElement.Name == name)
                {
                    return frameworkElement;
                }

                frameworkElement = GetDescendantFromName(frameworkElement, name);
                if (frameworkElement != null)
                {
                    return frameworkElement;
                }
            }

            return null;
        }
    }
}