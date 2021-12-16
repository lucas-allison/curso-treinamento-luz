using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using WpfTreinamento.Modelos;
using WpfTreinamento.Contratos;
using System.Windows;
using System.Data;

namespace WpfTreinamento
{
    public class ConexaoSQLSERVER : IConexao
    {
        #region Declarations

        private SqlConnection connectionString;
        private string table;
        List<Time> timeList;

        #endregion

        public ConexaoSQLSERVER()
        {
           connectionString = new SqlConnection(@"Data Source=DESKTOP-PPGQ64R;Initial Catalog=DBTreinamento;Persist Security Info=True;User ID=sa;Password=root");
           table = "Time";
        }

        #region Methods

        #region Consultar Itens

        public List<Time> ListarTimes()
        {
            timeList = new List<Time>();
            SqlCommand cmd = new SqlCommand($"select * from {table} order by ID", connectionString);

            connectionString.Open();
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
            connectionString.Close();

            return timeList;
        }

        #endregion

        #region Adicionar Itens

        public string AdicionaTime(Time time)
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO {table} VALUES (@Nome, @Divisao, @Regiao, @NomeCampeonato)", connectionString);
            string mensagem;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                connectionString.Open();
                cmd.ExecuteNonQuery();

                mensagem = "Registro Salvo com Sucesso!!";
            }
            catch (Exception ex)
            {
                mensagem = $"Não foi possível adicionar time: {ex.Message}";
            }
            
            cmd.Dispose();
            connectionString.Close();
            
            return mensagem;
        }

        #endregion

        #region Editar Itens

        public string EditaTime(Time time)
        {
            SqlCommand cmd = new SqlCommand($"UPDATE {table} SET Nome = @Nome, Divisao = @Divisao, Regiao = @Regiao, NomeCampeonato = @NomeCampeonato WHERE ID = @ID", connectionString);
            string mensagem;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", time.ID);
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                connectionString.Open();
                cmd.ExecuteNonQuery();

                mensagem = "Registro Editado com Sucesso!!";
            }
            catch (SqlException ex)
            {
                mensagem = $"Não foi possível editar time: {ex.Message}";
            }

            cmd.Dispose();
            connectionString.Close();

            return mensagem;
        }
        #endregion

        #region Remover Itens

        public string DeletaTime(int id)
        {
            SqlCommand cmd = new SqlCommand($"DELETE FROM {table} Where ID = {id}", connectionString);
            string mensagem;

            try
            {
                connectionString.Open();
                cmd.ExecuteNonQuery();

                mensagem = "Registro Removido com Sucesso!!";
            }
            catch (Exception ex)
            {
                mensagem = $"Não foi possível remover time: {ex.Message}";
                //MessageBox.Show($"Não foi possível remover time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            cmd.Dispose();
            connectionString.Close();

            return mensagem;
        }

        #endregion

        #endregion
    }

}
