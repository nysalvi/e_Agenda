using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.Compartilhado
{
    internal class Notificador
    {            
        public static void ApresentarMensagem(string mensagem, string tipoMensagem)
        {
            switch (tipoMensagem.ToLower())
            {
                case "sucesso":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case "atencao":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case "erro":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
            Console.WriteLine();
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
