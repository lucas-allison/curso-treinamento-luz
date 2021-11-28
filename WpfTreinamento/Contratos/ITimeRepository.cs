using System.Collections.ObjectModel;
using WpfTreinamento.Modelos;

namespace WpfTreinamento.Contratos
{
    public interface ITimeRepository
    {
        ObservableCollection<Time> ListarTimes();

        int AdicionaTime(Time time);

        int EditaTime(Time time);

        int DeletaTime(int id);
    }
}
