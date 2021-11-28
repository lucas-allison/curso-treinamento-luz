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
    public class TimeRepository : ITimeRepository
    {
        #region Declarations

        private SqlConnection connectionString = new SqlConnection(@"Data Source=DESKTOP-PPGQ64R;Initial Catalog=DBTreinamento;Persist Security Info=True;User ID=sa;Password=root");
        private string table = "Time";

        #endregion

        #region Methods

        #region Consultar Itens

        public ObservableCollection<Time> ListarTimes()
        {
            ObservableCollection<Time> timeList = new ObservableCollection<Time>();
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

        public int AdicionaTime(Time time)
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO {table} VALUES (@Nome, @Divisao, @Regiao, @NomeCampeonato)", connectionString);
            int result = 0;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                connectionString.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível adicionar time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cmd.Dispose();
                connectionString.Close();
            }

            return result;
        }

        #endregion

        #region Editar Itens

        public int EditaTime(Time time)
        {
            SqlCommand cmd = new SqlCommand($"UPDATE {table} SET Nome = @Nome, Divisao = @Divisao, Regiao = @Regiao, NomeCampeonato = @NomeCampeonato WHERE ID = @ID", connectionString);
            int result = 0;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", time.ID);
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                connectionString.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Não foi possível editar time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cmd.Dispose();
                connectionString.Close();
            }

            return result;
        }

        #endregion

        #region Remover Itens

        public int DeletaTime(int id)
        {
            SqlCommand cmd = new SqlCommand($"DELETE FROM {table} Where ID = {id}", connectionString);
            int result = 0;

            try
            {
                connectionString.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível remover time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cmd.Dispose();
                connectionString.Close();
            }

            return result;
        }

        #endregion

        #endregion
    }

}
