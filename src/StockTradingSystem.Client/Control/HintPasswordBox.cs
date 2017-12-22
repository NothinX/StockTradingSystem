using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockTradingSystem.Client.Control
{
    public class HintPasswordBox : HintTextBox
    {
        static HintPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HintPasswordBox), new FrameworkPropertyMetadata(typeof(HintPasswordBox)));
        }

        private bool _isResponseChange = true;
        private StringBuilder _passwordBuilder;

        public HintPasswordBox()
        {
            InputMethod.SetIsInputMethodEnabled(this, false);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, (s, e) => { }, (s, e) => { e.CanExecute = false; e.Handled = true; }));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, (s, e) => { }, (s, e) => { e.CanExecute = false; e.Handled = true; }));
            ContextMenu = new ContextMenu() { Visibility = Visibility.Collapsed };
            Loaded += HintPasswordBox_Loaded;
            TextChanged += HintPasswordBox_TextChanged;
        }

        private void HintPasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            _isResponseChange = false;
            Text = ConvertToPasswordChar(Password.Length);
            _isResponseChange = true;
        }

        private void HintPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isResponseChange) //响应事件标识，替换字符时，不处理后续逻辑
                return;
            var lastOffset = 0;
            foreach (var c in e.Changes)
            {
                Password = Password.Remove(c.Offset, c.RemovedLength); //从密码文中根据本次Change对象的索引和长度删除对应个数的字符
                Password = Password.Insert(c.Offset, Text.Substring(c.Offset, c.AddedLength));   //将Text新增的部分记录给密码文
                lastOffset = c.Offset + c.AddedLength;
            }
            /*将文本转换为密码字符*/
            _isResponseChange = false;  //设置响应标识为不响应
            Text = ConvertToPasswordChar(Text.Length);  //将输入的字符替换为密码字符
            _isResponseChange = true;   //回复响应标识
            SelectionStart = lastOffset; //设置光标索引
        }

        /// <summary>
        /// 按照指定的长度生成密码字符
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ConvertToPasswordChar(int length)
        {
            if (_passwordBuilder != null)
                _passwordBuilder.Clear();
            else
                _passwordBuilder = new StringBuilder();
            for (var i = 0; i < length; i++)
                _passwordBuilder.Append(PasswordChar);
            return _passwordBuilder.ToString();
        }

        /// <summary>
        /// The <see cref="PasswordChar" /> dependency property's name.
        /// </summary>
        public const string PasswordCharPropertyName = "PasswordChar";

        /// <summary>
        /// Gets or sets the value of the <see cref="PasswordChar" />
        /// property. This is a dependency property.
        /// </summary>
        public char PasswordChar
        {
            get => (char)GetValue(PasswordCharProperty);
            set => SetValue(PasswordCharProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="PasswordChar" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PasswordCharProperty = DependencyProperty.Register(
            PasswordCharPropertyName,
            typeof(char),
            typeof(HintPasswordBox),
            new PropertyMetadata('●'));

        /// <summary>
        /// The <see cref="Password" /> dependency property's name.
        /// </summary>
        public const string PasswordPropertyName = "Password";

        /// <summary>
        /// Gets or sets the value of the <see cref="Password" />
        /// property. This is a dependency property.
        /// </summary>
        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Password" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
            PasswordPropertyName,
            typeof(string),
            typeof(HintPasswordBox),
            new PropertyMetadata(string.Empty));
    }
}
