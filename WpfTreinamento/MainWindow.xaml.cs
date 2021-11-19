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
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using WpfTreinamento.Modelos;

namespace WpfTreinamento
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }
        private void abrirTelaFormTimes(object sender, RoutedEventArgs e)
        {
            FormTimes formTime = null;
            Button btn = (Button)sender;
            ListView? listgrid = btn.CommandParameter as ListView;

            if (btn.Content.Equals("Editar"))
            {
                if (listgrid != null && listgrid.SelectedItem != null)
                {
                    Time time = (Time)listgrid.SelectedItem;
                    formTime = new FormTimes(time);
                    formTime.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Necessário Selecionar uma Linha para Editar!!", "Editar Registro", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                formTime = new FormTimes();
                formTime.Show();
                this.Close();
            }
        }
    }
}
