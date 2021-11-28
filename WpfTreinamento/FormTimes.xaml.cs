using System.Windows;

namespace WpfTreinamento
{
    /// <summary>
    /// Lógica interna para FormTimes.xaml
    /// </summary>
    public partial class FormTimes : Window
    {
        private readonly MainWindowVM mainWindowVM;

        public FormTimes(MainWindowVM _mainWindowVM)
        {
            mainWindowVM = _mainWindowVM;

            InitializeComponent();
            DataContext = mainWindowVM;
        }
    }
}

