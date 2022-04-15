using System;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Compromisso
{
    internal class Compromisso : EntidadeBase
    {
        public string Assunto { get; set; }
        public string Local { get; set; }
        public DateTime Data { set; get;}
        public string _Data => Data.ToString("HH:mm");

        public string Empresa { get; set; }
        public string Cargo { get; set; }

        public Compromisso()
        {
            
        }
        public override bool Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                return false;
            if (string.IsNullOrEmpty(Email))
                return false;
            if (string.IsNullOrEmpty(Telefone) || Telefone.Length < 9)
                return false;
            if (string.IsNullOrEmpty(Empresa))
                return false;
            return true;
        }
        public override string ToString()
        {
            return "ID " + Numero + " :\n\tNome : " + Nome + "\n\tEmail : " + Email + "\n\tTelefone : "
                + Telefone + "\n\tEmpresa : " + Empresa + "\n\tCargo : " + Cargo + "\n\t//////////";
        }
    }
}
