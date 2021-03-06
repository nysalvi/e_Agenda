using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class Item : EntidadeBase
    {
        public Item()
        {

        }
        public string Descricao { get; set; }
        public bool Concluido { get; set; }

        public string EstaConcluido => Concluido ? "Concluída" : "Incompleta";
        public Item(string descricao)
        {            
            Descricao = descricao;
            Concluido = false;
        }
        public override string ToString()
        {
            return "ID : " + Numero + " Descrição : " + Descricao + " Status : " + EstaConcluido;
        }
        public override bool Validar()
        {
            if (string.IsNullOrEmpty(Descricao))
                return false;
            return true;
        }
    }
}
