using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTreinamento.Contratos;

namespace WpfTreinamento.Repositorios
{
    public class GerenciadorConexoes
    {
        private List<string> lista;

        private Dictionary<string, IConexao> database;

        private ConexaoSQLSERVER sqlserver;
        private ConexaoMySQL mysql;
        private ConexaoPostgresSQL postgres;

        public GerenciadorConexoes()
        {
            lista = new List<string>()
            {
                "sqlserver",
                "mysql",
                "postgresql"
            };

            sqlserver = new ConexaoSQLSERVER();
            mysql = new ConexaoMySQL();
            postgres = new ConexaoPostgresSQL();

            database = new Dictionary<string, IConexao>();

            database.Add(lista[0], sqlserver);
            database.Add(lista[1], mysql);
            database.Add(lista[2], postgres);
        }

        public IConexao pegaBanco(string tipoconexao)
        {
            return database.TryGetValue(tipoconexao, out IConexao valorconexao) ? valorconexao : mysql;
        }

        public List<string> listaBanco()
        {
            return lista;
        } 
    }
}
