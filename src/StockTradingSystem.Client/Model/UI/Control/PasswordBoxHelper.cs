using System.Windows;
using System.Windows.Controls;

namespace StockTradingSystem.Client.Model.UI.Control
{
    public class PasswordBoxHelper : DependencyObject
    {
        /// <summary>
        /// The HintText attached property's name.
        /// </summary>
        public const string HintTextPropertyName = "HintText";

        /// <summary>
        /// Gets the value of the HintText attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the HintText property of the specified object.</returns>
        public static string GetHintText(DependencyObject obj)
        {
            return (string)obj.GetValue(HintTextProperty);
        }

        /// <summary>
        /// Sets the value of the HintText attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the HintText value of the specified object.</param>
        public static void SetHintText(DependencyObject obj, string value)
        {
            obj.SetValue(HintTextProperty, value);
        }

        /// <summary>
        /// Identifies the HintText attached property.
        /// </summary>
        public static readonly DependencyProperty HintTextProperty = DependencyProperty.RegisterAttached(
            HintTextPropertyName,
            typeof(string),
            typeof(PasswordBoxHelper),
            new PropertyMetadata(string.Empty));

        /// <summary>
        /// The Password attached property's name.
        /// </summary>
        public const string PasswordPropertyName = "Password";

        /// <summary>
        /// Gets the value of the Password attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the Password property of the specified object.</returns>
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        /// <summary>
        /// Sets the value of the Password attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the Password value of the specified object.</param>
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// Identifies the Password attached property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
            PasswordPropertyName,
            typeof(string),
            typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        /// <summary>
        /// The IsAttached attached property's name.
        /// </summary>
        public const string IsAttachedPropertyName = "IsAttached";

        /// <summary>
        /// Gets the value of the IsAttached attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the IsAttached property of the specified object.</returns>
        public static bool GetIsAttached(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAttachedProperty);
        }

        /// <summary>
        /// Sets the value of the IsAttached attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the IsAttached value of the specified object.</param>
        public static void SetIsAttached(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAttachedProperty, value);
        }

        /// <summary>
        /// Identifies the IsAttached attached property.
        /// </summary>
        public static readonly DependencyProperty IsAttachedProperty = DependencyProperty.RegisterAttached(
            IsAttachedPropertyName,
            typeof(bool),
            typeof(PasswordBoxHelper),
            new PropertyMetadata(false, IsAttachedChanged));

        /// <summary>
        /// The IsUpdating attached property's name.
        /// </summary>
        private const string IsUpdatingPropertyName = "IsUpdating";

        /// <summary>
        /// Gets the value of the IsUpdating attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the IsUpdating property of the specified object.</returns>
        private static bool GetIsUpdating(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsUpdatingProperty);
        }

        /// <summary>
        /// Sets the value of the IsUpdating attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the IsUpdating value of the specified object.</param>
        private static void SetIsUpdating(DependencyObject obj, bool value)
        {
            obj.SetValue(IsUpdatingProperty, value);
        }

        /// <summary>
        /// Identifies the IsUpdating attached property.
        /// </summary>
        private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached(
            IsUpdatingPropertyName,
            typeof(bool),
            typeof(PasswordBoxHelper));

        private static void OnPasswordPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var pb = obj as PasswordBox;
            if (pb == null) return;
            pb.PasswordChanged -= PasswordChanged;

            if (!GetIsUpdating(pb))
            {
                pb.Password = (string)e.NewValue;
            }

            pb.PasswordChanged += PasswordChanged;
        }

        private static void IsAttachedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var pb = obj as PasswordBox;
            if (obj == null) return;

            if ((bool)e.OldValue)
            {
                pb.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                pb.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null) return;
            SetIsUpdating(pb, true);
            SetPassword(pb, pb.Password);
            SetIsUpdating(pb, false);
        }
    }
}