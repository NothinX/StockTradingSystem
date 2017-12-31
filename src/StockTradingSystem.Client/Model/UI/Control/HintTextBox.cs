using System.Windows;
using System.Windows.Controls;

namespace StockTradingSystem.Client.Model.UI.Control
{
    public class HintTextBox : TextBox
    {
        static HintTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HintTextBox), new FrameworkPropertyMetadata(typeof(HintTextBox)));
        }

        /// <summary>
        /// The <see cref="HintText" /> dependency property's name.
        /// </summary>
        public const string HintTextPropertyName = "HintText";

        /// <summary>
        /// Gets or sets the value of the <see cref="HintText" />
        /// property. This is a dependency property.
        /// </summary>
        public string HintText
        {
            get
            {
                return (string)GetValue(HintTextProperty);
            }
            set
            {
                SetValue(HintTextProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="HintText" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty HintTextProperty = DependencyProperty.Register(
            HintTextPropertyName,
            typeof(string),
            typeof(HintTextBox),
            new PropertyMetadata(string.Empty));
    }
}
