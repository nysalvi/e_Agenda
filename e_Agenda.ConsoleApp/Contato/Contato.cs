using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Contato
{
    internal class Contato : EntidadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Cargo{ get; set; }

        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Empresa = empresa;
            Cargo = cargo;
        }
        public override bool Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                return false;
            if (Email == null || !ValidarEmail())
                return false;
            if (ValidarTelefone())
                return false;
            if (string.IsNullOrEmpty(Empresa))
                return false;
            return true;
        }
        public bool ValidarTelefone()
        {//  (\+\e*)?
            string pais = @" *(\+ *[0-9] *[0-9] *)? *";
            string ddd = @"(([0-9] *[0-9] *)|(\( *[0-9] *[0-9] *\) *))?";
            string numero = @"([0-9] *){5}\-?([0-9] *){4}";

            Regex regex = new Regex("^" + pais + ddd + numero +"$");
            return regex.IsMatch(Nome);
        }
        public bool ValidarEmail()
        {
            EmailAddressAttribute e = new EmailAddressAttribute();
            return e.IsValid(Email);
        }
        public override string ToString()
        {
            return "ID " + Numero + " :\n\tNome : " + Nome + "\n\tEmail : " + Email + "\n\tTelefone : "
                + Telefone + "\n\tEmpresa : " + Empresa + "\n\tCargo : " + Cargo + "\n\t//////////";
        }
    }
}
