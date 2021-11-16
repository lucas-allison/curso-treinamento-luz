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
            LoadGrid();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PPGQ64R;Initial Catalog=DBTreinamento;Persist Security Info=True;User ID=sa;Password=root");

        #region Carrega Registro da ListView
        public void LoadGrid()
        {
            ObservableCollection<Time> timeList = new ObservableCollection<Time>();
            SqlCommand cmd = new SqlCommand("select * from Time", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Time time = new Time();
                    time.ID = Convert.ToInt32(sdr["ID"]);
                    time.Nome = sdr["Nome"].ToString();
                    time.Divisao = sdr["Divisao"].ToString();
                    time.Regiao = sdr["Regiao"].ToString();

                    timeList.Add(time);
                }
            }
            else
            {
                Console.WriteLine("Nenhum registro encontrado.");
            }
            listgrid.ItemsSource = timeList;
            listgrid.UpdateLayout();
            listgrid.Items.Refresh();

            if (listgrid.Items.Count == 0)
            {
                BtnRmv.IsEnabled = false;
            }

            cmd.Dispose();
            con.Close();
        }
        #endregion

        #region Remover Itens
        private void BtnRmv_Click(object sender, RoutedEventArgs e)
        {
            ListView? listgrid = ((Button)sender).CommandParameter as ListView;
            if (listgrid != null && listgrid.SelectedItem != null)
            {
                try
                {
                    if (MessageBox.Show("Deseja Remover este Registro?", "Remover Registro", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
                    {
                        Time time = (Time)listgrid.SelectedItem;
                        con.Open();
                        SqlCommand cmd = new SqlCommand($"DELETE FROM FirstTable Where ID = {time.ID}", con);
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Registro Removido", "Deletar Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        cmd.Dispose();
                        con.Close();

                        LoadGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Não Foi Possível Remover Registro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Necessário Selecionar uma Linha para Remover!!", "Remover Registro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Adicionar Itens
        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            FormTimes formTime = new FormTimes();
            formTime.Show();
            //this.Close();
        }
        #endregion

        #region Editar Itens
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            ListView? listgrid = ((Button)sender).CommandParameter as ListView;
            if (listgrid != null && listgrid.SelectedItem != null)
            {
                Time time = (Time)listgrid.SelectedItem;
                FormTimes formTimes = new FormTimes(time);
                formTimes.Show();
            }
            else
            {
                MessageBox.Show("Necessário Selecionar uma Linha para Editar!!", "Editar Registro", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
    }
}
