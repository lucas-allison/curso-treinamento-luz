using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfTreinamento.Modelos
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region Declarações

        private ObservableCollection<Time> _timeList;
        public ObservableCollection<Time> timeList { get { return _timeList; } set { _timeList = value; Notifica("timeList"); } }
        private Time _time;
        public Time time { get { return _time; } set { _time = value; Notifica("time"); } }
        public ICommand abrirTelaFT { get; private set; }
        public ICommand abrirTelaMW { get; private set; }
        public ICommand salvar { get; private set; }
        public ICommand deletar { get; private set; }
        public ICommand editar { get; private set; }
        public ICommand limparCampos { get; private set; }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PPGQ64R;Initial Catalog=DBTreinamento;Persist Security Info=True;User ID=sa;Password=root");

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Construtor

        public MainWindowVM()
        {
            LoadGrid();

            abrirTelaFT = new RelayCommand(AbrirTelaFormTimes);

            abrirTelaMW = new RelayCommand((object parametro) =>
            {
                FormTimes formTime = (FormTimes)parametro;
                AbrirTelaMainWindow(formTime);
            });

            salvar = new RelayCommand(AdicionaTime);

            deletar = new RelayCommand(DeletaTime, ValidaBloqueioBotaoDeletar);

            editar = new RelayCommand(EditaTime);

            limparCampos = new RelayCommand(LimparCampos);
        }

        #endregion

        #region Métodos

        #region Carrega Registro da ListView

        public void LoadGrid()
        {
            timeList = new ObservableCollection<Time>();
            time = new Time();
            SqlCommand cmd = new SqlCommand("select * from Time order by ID", con);
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
                    time.NomeCampeonato = sdr["NomeCampeonato"].ToString();

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
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propriedade));
            }
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

        private void AdicionaTime(object parametro)
        {
            FormTimes formTime = (FormTimes)parametro;
            int retorno = 0;

            try
            {
                if (EstaValido(time))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Time VALUES (@Nome, @Divisao, @Regiao, @Campeonato)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Nome", time?.Nome);
                    cmd.Parameters.AddWithValue("@Divisao", time?.Divisao);
                    cmd.Parameters.AddWithValue("@Regiao", time?.Regiao);
                    cmd.Parameters.AddWithValue("@Campeonato", time?.NomeCampeonato);
                    con.Open();
                    retorno = cmd.ExecuteNonQuery();

                    if (retorno > 0)
                    {
                        MessageBox.Show("Registro salvo com sucesso!!", "Registro Salvo", MessageBoxButton.OK, MessageBoxImage.Information);

                        AbrirTelaMainWindow(formTime);
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível adicionar o time", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    cmd.Dispose();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Editar Itens

        private void EditaTime(object parametro)
        {
            FormTimes formTime = (FormTimes)parametro;

            try
            {
                if (MessageBox.Show("Deseja Editar este Registro?", "Editar Registro", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Time SET Nome = '" + time.Nome + "', " +
                    "Divisao = '" + time.Divisao + "', " +
                    "Regiao = '" + time.Regiao + "', " +
                    "NomeCampeonato = '" + time.NomeCampeonato + "' " +
                    "WHERE ID = '" + time.ID + "'", con);
                    con.Open();

                    if (EstaValido(time))
                    {
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Registro Editado com Sucesso!!", "Editar Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                        AbrirTelaMainWindow(formTime);
                    }

                    cmd.Dispose();
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Abrir a Tela de MainWIndow

        private void AbrirTelaMainWindow(FormTimes formTime)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            formTime ??= new FormTimes();
            formTime.Close();
        }

        #endregion

        #region Abrir a Tela de FormTimes

        private void AbrirTelaFormTimes(object parametro)
        {
            MainWindow mainWindow = new MainWindow();
            FormTimes formTime = new FormTimes();

            switch (parametro)
            {
                case object[]:
                    object[] listParametros = (object[])parametro;
                    ListView listgrid = (ListView)listParametros[1];

                    if (listgrid != null && listgrid.SelectedItem != null)
                    {
                        mainWindow = (MainWindow)listParametros[0];
                        PopularCampos(formTime, listgrid);

                        mainWindow.Close();
                        formTime.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Necessário Selecionar uma Linha para Editar!!", "Editar Registro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    break;
                default:
                    mainWindow = (MainWindow)parametro;

                    mainWindow.Close();
                    formTime.ShowDialog();
                    break;
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

            if (string.IsNullOrWhiteSpace(time.NomeCampeonato))
            {
                MessageBox.Show("Necessário preencher o nome do campeonato", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        #endregion

        #region Popula Campos

        private void PopularCampos(FormTimes formTime, ListView listgrid)
        {
            MainWindowVM mainWindowVM = (MainWindowVM)formTime.DataContext;
            mainWindowVM.time = (Time)listgrid.SelectedItem;

            formTime.Titulo.Content = "Alteração de Time";
            formTime.BtnEditar.Visibility = Visibility.Visible;
            formTime.BtnAdicionar.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Limpa Campos

        private void LimparCampos(object parametro)
        {
            FormTimes formTime = (FormTimes)parametro;
            MainWindowVM mainWindowVM = (MainWindowVM)formTime.DataContext;

            int selectedItemId = time.ID;
            mainWindowVM.time = new Time();
            mainWindowVM.time.ID = selectedItemId;
        }

        #endregion

        private bool ValidaBloqueioBotaoDeletar(object propriedade)
        {
            ListView listgrid = (ListView)propriedade;
            return listgrid.Items.Count > 0;
        }

        #endregion
    }
}