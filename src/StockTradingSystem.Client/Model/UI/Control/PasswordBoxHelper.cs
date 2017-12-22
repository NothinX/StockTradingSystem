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
        /// The Attach attached property's name.
        /// </summary>
        public const string AttachPropertyName = "Attach";

        /// <summary>
        /// Gets the value of the Attach attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the Attach property of the specified object.</returns>
        public static bool GetAttach(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        /// <summary>
        /// Sets the value of the Attach attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the Attach value of the specified object.</param>
        public static void SetAttach(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        /// <summary>
        /// Identifies the Attach attached property.
        /// </summary>
        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached(
            AttachPropertyName,
            typeof(bool),
            typeof(PasswordBoxHelper),
            new PropertyMetadata(false, Attach));

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

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox)) return;
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }

            passwordBox.PasswordChanged += PasswordChanged;
        }

        private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
                return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox)) return;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
    }
}