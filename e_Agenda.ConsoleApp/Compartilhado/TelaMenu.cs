using System;
using e_Agenda.ConsoleApp.Contato;
using e_Agenda.ConsoleApp.Compromisso;
using e_Agenda.ConsoleApp.Tarefa;

namespace e_Agenda.ConsoleApp.Compartilhado
{
    public class TelaMenu
    {
        private readonly RepositorioContato repositorioContato;
        private readonly TelaContato telaContato;

        private readonly RepositorioCompromisso repositorioCompromisso;
        private readonly TelaCompromisso telaCompromisso;

        private readonly RepositorioTarefa repositorioTarefa;
        private readonly TelaTarefa telaTarefa;

        public TelaMenu()
        {
            this.repositorioContato = new RepositorioContato();
            this.telaContato = new TelaContato(repositorioContato);

            this.repositorioCompromisso = new RepositorioCompromisso();
            this.telaCompromisso = new TelaCompromisso(repositorioCompromisso);

            this.repositorioTarefa = new RepositorioTarefa();
            this.telaTarefa = new TelaTarefa(repositorioTarefa);

            PopularAplicacao();
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("e-Agenda");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Tarefa");
            Console.WriteLine("Digite 2 para Gerenciar Contato");
            Console.WriteLine("Digite 3 para Gerenciar Compromisso");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaTarefa;

            else if (opcao == "2")
                tela = telaContato;

            else if (opcao == "3")
                tela = telaCompromisso;

            else if (opcao == "4")
                tela = null;

            else if (opcao == "5")
                tela = null;

            return tela;
        }
        private void PopularAplicacao()
        {
            //var garcom = new Garcom("Julinho", "230.232.519-98");
            //repositorioGarcom.Inserir(garcom);

            Tarefa.Tarefa tarefa1 = new Tarefa.Tarefa("Casa", 2, DateTime.Parse("11/09/2019"), 2);
            Tarefa.Tarefa tarefa2 = new Tarefa.Tarefa("Mercado", 2, DateTime.Parse("09/04/2018"), 1);
            Tarefa.Tarefa tarefa3 = new Tarefa.Tarefa("Trabalho", 3, DateTime.Parse("20/05/2015"), 3);
            Tarefa.Tarefa tarefa4 = new Tarefa.Tarefa("Transporte", 1, DateTime.Parse("18/06/2017"), 3);
            Tarefa.Tarefa tarefa5 = new Tarefa.Tarefa("Finanças", 3, DateTime.Parse("12/11/2016"), 2);
            Tarefa.Tarefa tarefa6 = new Tarefa.Tarefa("Férias", 1, DateTime.Parse("14/07/2020"), 3);

            repositorioTarefa.Inserir(tarefa1);
            repositorioTarefa.Inserir(tarefa2);
            repositorioTarefa.Inserir(tarefa3);
            repositorioTarefa.Inserir(tarefa4);
            repositorioTarefa.Inserir(tarefa5);
            repositorioTarefa.Inserir(tarefa6);

            Contato.Contato contato1 = new Contato.Contato("José", "joseE@gmail.com", "(99) 9 9399 - 4855",
                "NDD", "RH");
            Contato.Contato contato2 = new Contato.Contato("Paulo", "pPaulo@orkut.com", "(59) 9 6641 - 4412",
                "NDD", "Suporte");
            Contato.Contato contato3 = new Contato.Contato("Ricardo", "Ri_Cardo@outlook.com", 
                "(33) 9 2251 - 3355", "NDD", "Suporte");

            repositorioContato.Inserir(contato1);
            repositorioContato.Inserir(contato2);
            repositorioContato.Inserir(contato3);
        }
    }
}
