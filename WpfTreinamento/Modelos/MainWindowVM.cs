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
using System.ComponentModel;

namespace WpfTreinamento.Modelos
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region Declarações

        public ObservableCollection<Time> timeList { get; private set; }
        public Time time { get; set; }
        public ICommand salvar { get; private set; }
        public ICommand deletar { get; private set; }
        public ICommand editar { get; private set; }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PPGQ64R;Initial Catalog=DBTreinamento;Persist Security Info=True;User ID=sa;Password=root");

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Construtor

        public MainWindowVM()
        {
            LoadGrid();

            salvar = new RelayCommand((object parametro) =>
            {
                AdicionaTime();
                Notifica("timeList");
            });

            deletar = new RelayCommand((object parametro) =>
            {
                DeletaTime(parametro);
                Notifica("timeList");
            });

            editar = new RelayCommand((object parametro) =>
            {
                EditaTime();
                Notifica("timeList");
            });
        }

        #endregion

        #region Métodos

        #region Carrega Registro da ListView
        public void LoadGrid()
        {
            timeList = new ObservableCollection<Time>();
            time = new Time();
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

            cmd.Dispose();
            con.Close();
        }

        //Método de Notificação
        private void Notifica(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }
        #endregion

        #region Remover Itens
        private void DeletaTime(object parametro)
        {
            ListView? listgrid = parametro as ListView;
            if (listgrid != null && listgrid.SelectedItem != null)
            {
                try
                {
                    if (MessageBox.Show("Deseja Remover este Registro?", "Remover Registro", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
                    {
                        Time time = (Time)listgrid.SelectedItem;
                        con.Open();
                        SqlCommand cmd = new SqlCommand($"DELETE FROM Time Where ID = {time.ID}", con);
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

        private void AdicionaTime()
        {
            int maxId = 0;
            int retorno = 0;

            try
            {
                if (EstaValido(time))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Time VALUES (@Nome, @Divisao, @Regiao)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Nome", time?.Nome);
                    cmd.Parameters.AddWithValue("@Divisao", time?.Divisao);
                    cmd.Parameters.AddWithValue("@Regiao", time?.Regiao);
                    con.Open();
                    retorno = cmd.ExecuteNonQuery();

                    if (retorno > 0)
                    {
                        MessageBox.Show("Registro salvo com sucesso!!", "Registro Salvo", MessageBoxButton.OK, MessageBoxImage.Information);

                        if (timeList.Any())
                        {
                            maxId = timeList.Max(x => x.ID);
                        }

                        time.ID = maxId + 1;
                        timeList.Add(time);
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível adicionar o time", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Editar Itens
        private void EditaTime()
        {
            try
            {
                if (MessageBox.Show("Deseja Editar este Registro?", "Editar Registro", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
                {
                    if (EstaValido(time))
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Time SET Nome = '" + time.Nome + "', " +
                        "Divisao = '" + time.Divisao + "', " +
                        "Regiao = '" + time.Regiao + "' " +
                        "WHERE ID = '" + time.ID + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro Editado com Sucesso!!", "Editar Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível editar o time", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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

        #region Valida Campos
        public bool EstaValido(Time? time)
        {
            if (string.IsNullOrWhiteSpace(time.Nome))
            {
                MessageBox.Show("Necessário preencher o nome", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(time.Divisao))
            {
                MessageBox.Show("Necessário preencher a divisão", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(time.Regiao))
            {
                MessageBox.Show("Necessário preencher a região", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        #endregion

        #endregion
    }
}
