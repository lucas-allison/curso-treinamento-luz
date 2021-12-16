using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfTreinamento.Modelos;

namespace WpfTreinamento.Contratos
{
    public interface IConexao
    {
        List<Time> ListarTimes();

        string AdicionaTime(Time time);

        string EditaTime(Time time);

        string DeletaTime(int id);
    }
}
