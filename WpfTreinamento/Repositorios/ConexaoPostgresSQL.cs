using System;
using System.Windows;
using WpfTreinamento.Modelos;
using System.Collections.ObjectModel;
using WpfTreinamento.Contratos;
using Npgsql;
using System.Data;
using System.Collections.Generic;

namespace WpfTreinamento.Repositorios
{
    public class ConexaoPostgresSQL : IConexao
    {
        #region Declarations

        private NpgsqlConnection postcon;
        private string table;
        List<Time> timeList;

        #endregion

        public ConexaoPostgresSQL()
        {
            postcon = new NpgsqlConnection(@"host=localhost;database=DBTreinamento;username=postgres;Password=root");
            table = "Time";
        }

        #region Methods

        #region Consultar Itens

        public List<Time> ListarTimes()
        {
            timeList = new List<Time>();
            NpgsqlCommand cmd = new NpgsqlCommand($"select * from \"{table}\".\"{table}\" order by \"ID\"", postcon);

            postcon.Open();
            NpgsqlDataReader sdr = cmd.ExecuteReader();

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
            postcon.Close();

            return timeList;
        }

        #endregion

        #region Adicionar Itens

        public string AdicionaTime(Time time)
        {
            NpgsqlCommand cmd = new NpgsqlCommand($"INSERT INTO \"{table}\".\"{table}\"(\"Nome\", \"Divisao\", \"Regiao\", \"NomeCampeonato\") VALUES (@Nome, @Divisao, @Regiao, @NomeCampeonato)", postcon);
            string mensagem;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                postcon.Open();
                cmd.ExecuteNonQuery();

                mensagem = "Registro Salvo com Sucesso!!";
            }
            catch (Exception ex)
            {
                mensagem = $"Não foi possível adicionar time: {ex.Message}";
                //MessageBox.Show($"Não foi possível adicionar time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            cmd.Dispose();
            postcon.Close();
            
            return mensagem;
        }

        #endregion

        #region Editar Itens

        public string EditaTime(Time time)
        {
            NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE \"{table}\".\"{table}\" SET \"Nome\" = @Nome, \"Divisao\" = @Divisao, \"Regiao\" = @Regiao, \"NomeCampeonato\" = @NomeCampeonato WHERE \"ID\" = @ID", postcon);
            string mensagem;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", time.ID);
                cmd.Parameters.AddWithValue("@Nome", time.Nome);
                cmd.Parameters.AddWithValue("@Divisao", time.Divisao);
                cmd.Parameters.AddWithValue("@Regiao", time.Regiao);
                cmd.Parameters.AddWithValue("@NomeCampeonato", time.NomeCampeonato);
                postcon.Open();
                cmd.ExecuteNonQuery();

                mensagem = "Registro Editado com Sucesso!!";
            }
            catch (Exception ex)
            {
                mensagem = $"Não foi possível editar time: {ex.Message}";
                //MessageBox.Show($"Não foi possível editar time: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            cmd.Dispose();
            postcon.Close();
            
            return mensagem;
        }

        #endregion

        #region Remover Itens

        public string DeletaTime(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand($"DELETE FROM \"{table}\".\"{table}\" Where \"ID\" = {id}", postcon);
            string mensagem;

            try
            {
                postcon.Open();
                cmd.ExecuteNonQuery();

                mensagem = "Registro Removido com Sucesso !!";
            }
            catch (Exception ex)
            {
                mensagem = $"Não foi possível remover time: {ex.Message}";
            }
            
            cmd.Dispose();
            postcon.Close();
            
            return mensagem;
        }

        #endregion

        #endregion
    }
}
