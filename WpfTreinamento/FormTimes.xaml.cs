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
        }

        public FormTimes(Time time)
        {
            InitializeComponent();
            consultarTime(time);
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PPGQ64R;Initial Catalog=DBTreinamento;Persist Security Info=True;User ID=sa;Password=root");

        #region Valida Campos
        public bool EstaValido()
        {
            if (nome_txt.Text == string.Empty)
            {
                MessageBox.Show("Necessário preencher o nome", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (div_txt.Text == string.Empty)
            {
                MessageBox.Show("Necessário preencher a divisão", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (regiao_txt.Text == string.Empty)
            {
                MessageBox.Show("Necessário preencher a região", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Limpa Campos
        public void limparCampos()
        {
            nome_txt.Clear();
            div_txt.Clear();
            regiao_txt.Clear();
        }
        #endregion

        #region Consulta Time, Incui botão Alterar e Altera Título
        private void consultarTime(Time time)
        {
            if (time != null)
            {
                search_txt.Text = time.ID.ToString();
                nome_txt.Text = time.Nome;
                div_txt.Text = time.Divisao;
                regiao_txt.Text = time.Regiao;
                Titulo.Content = "Alteração de Time";
                BtnEditar.Visibility = Visibility.Visible;
                BtnAdicionar.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Edita Time
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja Editar este Registro?", "Editar Registro", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No) 
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Time SET Nome = '" + nome_txt.Text + "', " +
                    "Divisao = '" + div_txt.Text + "', " +
                    "Regiao = '" + regiao_txt.Text + "' " +
                    "WHERE ID = '" + search_txt.Text + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Editado com Sucesso!!", "Editar Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Adiciona Time
        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            try
            {
                if (EstaValido())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Time VALUES (@Nome, @Divisao, @Regiao)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Nome", nome_txt.Text);
                    cmd.Parameters.AddWithValue("@Divisao", div_txt.Text);
                    cmd.Parameters.AddWithValue("@Regiao", regiao_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Registro salvo com sucesso!!", "Registro Salvo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            main.LoadGrid();
        }
        #endregion

        #region Redireciona para tela de Consuklta
        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        #endregion 
        

        #region Executa o Limpa Campos
        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            limparCampos();
        }
        #endregion
    }
}

