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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
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

        public FormTimes(Time time)
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
            consultarTime(time);
        }

        #region Consulta Time, Incui botão Alterar e Altera Título

        private void consultarTime(Time time)
        {
            MainWindowVM mainWindowVM = (MainWindowVM)this.DataContext;

            if (time != null)
            {
                mainWindowVM.time = time;
                Titulo.Content = "Alteração de Time";
                BtnEditar.Visibility = Visibility.Visible;
                BtnAdicionar.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Redireciona para tela de Consulta
        
        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        
        #endregion

        #region Limpa os Campos

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            limparCampos();
        }

        public void limparCampos()
        {
            nome_txt.Clear();
            div_txt.Clear();
            regiao_txt.Clear();
        }

        #endregion
    }
}

