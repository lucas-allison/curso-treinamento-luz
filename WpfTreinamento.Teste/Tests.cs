using NUnit.Framework;
using System.Collections.ObjectModel;
using WpfTreinamento.Modelos;

namespace WpfTreinamento.Teste
{
    [TestFixture]
    public class Tests
    {
        private MainWindowVM vm;

        [SetUp]
        public void IniciaTeste()
        {
            vm = new MainWindowVM();
        }

        //Valida se o dado mocado est� no banco de dados
        [Test]
        public void ValidaMockNoBanco()
        {

            vm.time.Nome.Contains("Teste MOCK");

            Assert.IsNotNull(vm);
        }

        //Valida se a fun��o de adicionar do banco de dados est� funcionando
        [Test]
        public void AdicionaMock()
        {
            vm.time = new Time()
            {
                ID = 10,
                Nome = "Teste MOCK",
                Divisao = "1",
                Regiao = "2",
                NomeCampeonato = "3"
            };

            vm.salvar.Execute(this);
        } 
        
        //Valida se a fun��o de remover do banco de dados est� funcionando
        [TearDown]
        public void DeletaMock()
        {
            vm.time = new Time()
            {
                ID = 1,
                Nome = "Teste MOCK",
                Divisao = "1",
                Regiao = "2",
                NomeCampeonato = "3"
            };

            vm.deletar.Execute(this);
        }
    }
}