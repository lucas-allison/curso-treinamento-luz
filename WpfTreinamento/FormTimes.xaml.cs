using System.Windows;
using WpfTreinamento.Modelos;

namespace WpfTreinamento
{
    /// <summary>
    /// Lógica interna para FormTimes.xaml
    /// </summary>
    public partial class FormTimes : Window
    {
        public FormTimes()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }
    }
}

