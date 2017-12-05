using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using StockTradingSystem.Client.UI.Navigation;
using StockTradingSystem.Client.ViewModel;namespace StockTradingSystem.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private static void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<IFrameNavigationService>()?.NavigateTo("MainView");
        }
    }
}