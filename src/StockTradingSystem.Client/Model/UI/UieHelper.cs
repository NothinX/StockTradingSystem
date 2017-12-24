using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace StockTradingSystem.Client.Model.UI
{
    public static class UieHelper
    {
        /// <summary>
        /// The IsFocused attached property's name.
        /// </summary>
        public const string IsFocusedPropertyName = "IsFocused";

        /// <summary>
        /// Gets the value of the IsFocused attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the IsFocused property of the specified object.</returns>
        public static bool? GetIsFocused(DependencyObject obj)
        {
            return (bool?)obj.GetValue(IsFocusedProperty);
        }

        /// <summary>
        /// Sets the value of the IsFocused attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the IsFocused value of the specified object.</param>
        public static void SetIsFocused(DependencyObject obj, bool? value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        /// <summary>
        /// Identifies the IsFocused attached property.
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            IsFocusedPropertyName,
            typeof(bool?),
            typeof(UieHelper),
            new UIPropertyMetadata(null, OnIsFocusedChanged));

        private static void OnIsFocusedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is UIElement uie)) return;
            if (e.OldValue == null)
            {
                uie.GotFocus += UieOnGotFocus;
                uie.LostFocus += UieOnLostFocus;
            }
            if ((bool?)e.NewValue == true)
            {
                uie.Focus();
                if (obj is TextBoxBase tb)
                {
                    tb.SelectAll();
                }
            }
        }

        private static void UieOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!(sender is UIElement uie)) return;
            SetIsFocused(uie, false);
        }

        private static void UieOnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!(sender is UIElement uie)) return;
            SetIsFocused(uie, true);
        }

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