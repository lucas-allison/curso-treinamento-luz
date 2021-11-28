using System.Windows;
namespace WpfTreinamento
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowVM mainWindowVM;

        public MainWindow(MainWindowVM _mainWindowVM)
        {
            mainWindowVM = _mainWindowVM;

            InitializeComponent();
            DataContext = mainWindowVM;
        }
    }
}
