using System;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Compromisso
{
    internal class Compromisso : EntidadeBase
    {
        public string Assunto { get; set; }
        public string Local { get; set; }
        public DateTime Data { set; get;}
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Contato.Contato Contato{ get; set; }

        public Compromisso(string assunto, string local, DateTime data, TimeSpan horaInicio, 
            TimeSpan horaTermino, Contato.Contato contato)
        {
            Assunto = assunto;
            Local = local;
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            Contato = contato;
        }
        public override bool Validar()
        {
            
            if (string.IsNullOrEmpty(Assunto))
                return false;
            if (string.IsNullOrEmpty(Local))
                return false;
            if (Data.CompareTo(DateTime.Parse("01/01/1900")) == -1)
                return false;
            if (Contato != null && !Contato.Validar())
                return false;
            if (HoraTermino < HoraInicio)
                return false;
            return true;             
        }
        public override string ToString()
        {
            return "ID " + Numero + " :\n\tAssunto : " + Assunto + "\n\tLocal : " + Local + "\n\tData : "
                + Data + "\n\tHorário Inicial : " + HoraInicio + "\n\tHorário Final: " + HoraTermino 
                + "\n\tContato : " + (Contato == null ? "Sem Contato Atrelado!!!" : Contato.Nome) 
                + "\n\t//////////";
        }
    }
}
