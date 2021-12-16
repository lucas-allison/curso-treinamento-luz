using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using WpfTreinamento.Contratos;
using System.ComponentModel;
using WpfTreinamento.Modelos;
using WpfTreinamento.Repositorios;

namespace WpfTreinamento
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region Declarações

        private ObservableCollection<Time> _timeList;
        private Time _time;
        public ObservableCollection<Time> timeList { get { return _timeList; } set { _timeList = value; Notifica(nameof(timeList)); } }
        public ObservableCollection<string> listaBanco { get; set; }
        public Time time { get { return _time; } set { _time = value; Notifica(nameof(time)); } }
        public ICommand abrirTelaFT { get; private set; }
        public ICommand abrirTelaMW { get; private set; }
        public ICommand salvar { get; private set; }
        public ICommand deletar { get; private set; }
        public ICommand editar { get; private set; }
        public ICommand limparCampos { get; private set; }
        public ICommand carregaConexaoSelecionada { get; private set; }
        public IConexao conexao { get; private set; }
        public GerenciadorConexoes gerenciaConexao { get; set; }

        private readonly ViewModelIntermediate viewModelIntermediate;

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Construtor

        public MainWindowVM()
        {
            gerenciaConexao = new GerenciadorConexoes();
            listaBanco = new ObservableCollection<string>(gerenciaConexao.listaBanco());
            conexao = gerenciaConexao.pegaBanco(listaBanco[0]);
            viewModelIntermediate = new ViewModelIntermediate(this);

            LoadGrid();
            RegistrarComandos();
        }

        #endregion

        #region Métodos

        #region Carrega Registro da ListView

        private void LoadGrid()
        {
            timeList = new ObservableCollection<Time>(conexao.ListarTimes());

            if (timeList.Count == 0)
            {
                Console.WriteLine("Nenhum registro encontrado.");
            }
        }

        #endregion

        #region Remover Itens

        private void DeletaTime(object parametro)
        {
            ListView? listgrid = parametro as ListView;
            if (listgrid != null && listgrid.SelectedItem != null)
            {
                if (MessageBox.Show("Deseja Remover este Registro?", "Remover Registro", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
                {
                    Time time = (Time)listgrid.SelectedItem;
                                        
                    MessageBox.Show(conexao.DeletaTime(time.ID), "Deletar Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    LoadGrid();
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

            if (EstaValido(time))
            {            
                MessageBox.Show(conexao.AdicionaTime(time), "Registro Salvo", MessageBoxButton.OK, MessageBoxImage.Information);
                AbrirTelaMainWindow(formTime);

                LoadGrid();                
            }
        }

        #endregion

        #region Editar Itens

        private void EditaTime(object parametro)
        {
            FormTimes formTime = (FormTimes)parametro;

            if (MessageBox.Show("Deseja Editar este Registro?", "Editar Registro", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
            {
                if (EstaValido(time))
                {                                        
                    MessageBox.Show(conexao.EditaTime(time), "Editar Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    AbrirTelaMainWindow(formTime);
                }
            }
        }

        #endregion

        #region Abrir a Tela de MainWIndow

        private void AbrirTelaMainWindow(FormTimes formTime)
        {
            MainWindow mainWindow = viewModelIntermediate.MainWindowInstantiate();
            formTime ??= viewModelIntermediate.FormTimesInstantiate();

            mainWindow.Show();
            formTime.Close();
        }

        #endregion

        #region Abrir a Tela de FormTimes

        private void AbrirTelaFormTimes(object parametro)
        {
            MainWindow mainWindow;
            time = new Time();
            FormTimes formTime = viewModelIntermediate.FormTimesInstantiate();
            FormTimes formTime2 = new FormTimes(this);
            formTime2.DataContext = time;

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

        private bool EstaValido(Time? time)
        {
            if (time != null)
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
            }
            else
            {
                MessageBox.Show("Time inválido", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
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

        #region Utilitários

        private void RegistrarComandos()
        {
            salvar = new RelayCommand(AdicionaTime);
            deletar = new RelayCommand(DeletaTime, ValidaBloqueioBotaoDeletar);
            editar = new RelayCommand(EditaTime);
            limparCampos = new RelayCommand(LimparCampos);
            abrirTelaFT = new RelayCommand(AbrirTelaFormTimes);
            abrirTelaMW = new RelayCommand((object parametro) =>
            {
                FormTimes formTime = (FormTimes)parametro;
                AbrirTelaMainWindow(formTime);
            });

        }

        private bool ValidaBloqueioBotaoDeletar(object propriedade)
        {
            ListView listgrid = (ListView)propriedade;
            return listgrid.Items.Count > 0;
        }
        private void Notifica(string propriedade)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propriedade));
            }
        }

        #endregion

        #endregion
    }
}