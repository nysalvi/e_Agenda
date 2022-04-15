using System;
using e_Agenda.ConsoleApp.Tarefa;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaMenu telaMenuPrincipal = new TelaMenu();

            while (true)
            {
                TelaBase telaSelecionada = telaMenuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    break;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();
            if (telaSelecionada is TelaTarefa)
            {
                TelaTarefa telaCadastroBasico = (TelaTarefa)telaSelecionada;
                if (opcaoSelecionada == "1")
                    telaCadastroBasico.InserirRegistro();
                if (opcaoSelecionada == "2")
                    telaCadastroBasico.EditarRegistro();
                if (opcaoSelecionada == "3")
                    telaCadastroBasico.ExcluirRegistro();
                if (opcaoSelecionada == "4")
                    telaCadastroBasico.VisualizarRegistro();
                if (opcaoSelecionada == "5")
                    telaCadastroBasico.AdicionarItem();
                if (opcaoSelecionada == "6")
                    telaCadastroBasico.ConcluirItem();
                if (opcaoSelecionada == "7")
                    telaCadastroBasico.VisualizarConcluidos();
                if (opcaoSelecionada == "8")
                    telaCadastroBasico.VisualizarPendentes();
                }
                else if (telaSelecionada is ITela)
            {
                ITela telaCadastroBasico = (ITela)telaSelecionada;

                if (opcaoSelecionada == "1")
                    telaCadastroBasico.InserirRegistro();

                if (opcaoSelecionada == "2")
                    telaCadastroBasico.EditarRegistro();

                if (opcaoSelecionada == "3")
                    telaCadastroBasico.ExcluirRegistro();

                if (opcaoSelecionada == "4")
                    telaCadastroBasico.VisualizarRegistro();
                }
            }
        }
    }
}