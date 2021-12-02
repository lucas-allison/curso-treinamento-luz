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

        public GerenciadorConexoes()
        {
            lista = new List<string>()
            {
                "sqlserver",
                "mysql",
                "postgresql"
            };

            database = new Dictionary<string, IConexao>();

            database.Add(lista[0], new ConexaoSQLSERVER());
            database.Add(lista[1], new ConexaoMySQL());
            database.Add(lista[2], new ConexaoPostgresSQL());
        }

        public IConexao pegaBanco(string tipoconexao)
        {
            IConexao valorconexao = null;
            if (lista.Any(conexao => conexao.Contains(tipoconexao)))
            {
                database.TryGetValue(tipoconexao, out valorconexao);
            }
            return valorconexao;
        }
    }
}
