using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTreinamento.Contratos;

namespace WpfTreinamento.Repositorios
{
    public class ViewModelIntermediate : IViewModelIntermediate
    {
        private readonly MainWindowVM mainWindowVM;

        public ViewModelIntermediate(MainWindowVM _mainWindowVM)
        {
            mainWindowVM = _mainWindowVM;
        }

        public FormTimes FormTimesInstantiate()
        {
            return new FormTimes(mainWindowVM);
        }

        public MainWindow MainWindowInstantiate()
        {
            return new MainWindow(mainWindowVM);
        }
    }
}
