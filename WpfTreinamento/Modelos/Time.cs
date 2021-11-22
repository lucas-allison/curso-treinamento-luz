using System.ComponentModel;

namespace WpfTreinamento.Modelos
{
    public class Time : Campeonato, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Notifica(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }

        private int _id;
        private string _nome;
        private string _divisao;
        private string _regiao;

        public int ID { 
            get { return _id; }
            set { _id = value; Notifica("ID"); } 
        }
        public string Nome { 
            get { return _nome; }
            set { _nome = value; Notifica("Nome"); }
        }
        public string Divisao {
            get { return _divisao; }
            set { _divisao = value; Notifica("Divisao"); }
        }
        public string Regiao {
            get { return _regiao; }
            set { _regiao = value; Notifica("Regiao"); }
        }
       
    }
}