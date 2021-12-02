using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using System.Data.SqlClient;
using System.Windows;
using WpfTreinamento.Modelos;
using System.Collections.ObjectModel;
using WpfTreinamento.Contratos;
using MySql.Data.MySqlClient;

namespace WpfTreinamento.Repositorios
{
    public class ConexaoMySQL : IConexao
    {
        #region Declarações
        private MySqlConnection mysqlCon;
        private string table;
        private List<Time> timeList;
        #endregion

        public ConexaoMySQL()
        {
            mysqlCon = new MySqlConnection(@"server=localhost;uid=sa;database=DBTreinamento;pwd=root");
            table = "Time";
        }

        #region Methods

        #region Consultar Itens

        public List<Time> ListarTimes()
        {
            timeList = new List<Time>();
            MySqlCommand cmd = new MySqlCommand($"select * from {table} order by ID", mysqlCon);

            mysqlCon.Open();
            MySqlDataReader sdr = cmd.ExecuteReader();

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
            mysqlCon.Close();

            return timeList;
        }

        #endregion

        #region Adicionar Itens

        public int AdicionaTime(Time time)
        {
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO {table} (Nome, Divisao, Regiao, NomeCampeonato) VALUES (@Nome, @Divisao, @Regiao, @NomeCampeonato)", mysqlCon);
            int result = 0;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                mysqlCon.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível adicionar time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }     
            
            cmd.Dispose();
            mysqlCon.Close();            

            return result;
        }

        #endregion

        #region Editar Itens

        public int EditaTime(Time time)
        {
            MySqlCommand cmd = new MySqlCommand($"UPDATE {table} SET Nome = @Nome, Divisao = @Divisao, Regiao = @Regiao, NomeCampeonato = @NomeCampeonato WHERE ID = @ID", mysqlCon);
            int result = 0;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", time.ID);
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                mysqlCon.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Não foi possível editar time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
            cmd.Dispose();
            mysqlCon.Close();            

            return result;
        }

        #endregion

        #region Remover Itens

        public int DeletaTime(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM {table} Where ID = {id}", mysqlCon);
            int result = 0;

            try
            {
                mysqlCon.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível remover time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            cmd.Dispose();
            mysqlCon.Close();

            return result;
        }

        #endregion

        #endregion
    }
}
