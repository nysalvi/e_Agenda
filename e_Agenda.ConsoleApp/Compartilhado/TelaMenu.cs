using System;
using System.IO;
using e_Agenda.ConsoleApp.ModuloContato;
using e_Agenda.ConsoleApp.ModuloCompromisso;
using e_Agenda.ConsoleApp.ModuloTarefa;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Compartilhado
{
    public class TelaMenu
    {
        private RepositorioContato repositorioContato;
        private readonly TelaContato telaContato;

        private RepositorioCompromisso repositorioCompromisso;
        private readonly TelaCompromisso telaCompromisso;

        private RepositorioTarefa repositorioTarefa;
        private readonly TelaTarefa telaTarefa;        

        public TelaMenu()
        {
            this.repositorioContato = new RepositorioContato();
            this.telaContato = new TelaContato(repositorioContato);

            this.repositorioCompromisso = new RepositorioCompromisso();
            this.telaCompromisso = new TelaCompromisso(repositorioCompromisso, telaContato, repositorioContato);

            this.repositorioTarefa = new RepositorioTarefa();
            this.telaTarefa = new TelaTarefa(repositorioTarefa);
            
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("e-Agenda");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Tarefa");
            Console.WriteLine("Digite 2 para Gerenciar Contato");
            Console.WriteLine("Digite 3 para Gerenciar Compromisso");
            Console.WriteLine("Digite 4 para Popular o Programa");
            Console.WriteLine("Digite 5 para Salvar o Programa");
            Console.WriteLine("Digite 6 para Carregar o Programa");
            Console.WriteLine("Digite 7 para Deletar Dados");
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
            {
                Notificador.ApresentarMensagem("Dados Gerados Com Sucesso !!!", "sucesso");
                PopularAplicacao();
            }
            else if (opcao == "5")
            {
                if (repositorioTarefa.Registros.Count == 0
                    && repositorioContato.Registros.Count == 0
                    && repositorioCompromisso.Registros.Count == 0)
                    Notificador.ApresentarMensagem("Nenhum Dado Para Salvar", "atencao");
                else
                {
                    SalvarAplicacao();
                    Notificador.ApresentarMensagem("Dados Salvos Com Sucesso", "sucesso");
                }
            }
            else if (opcao == "6")
            {
                CarregarAplicacao();
            }
            else if (opcao == "7")
            {
                int status = 0;
                string tarefa = Path.GetDirectoryName("\\Tarefa.xml");
                if (File.Exists(tarefa))
                {
                    File.Delete(tarefa);
                }
                else status++;
                string contato = Path.GetDirectoryName("\\Contato.xml");
                if (File.Exists(contato))
                {
                    File.Delete(contato);
                }
                else status++;
                string compromisso = Path.GetDirectoryName("\\Compromisso.xml");
                if (File.Exists(compromisso))
                {
                    File.Delete(compromisso);
                }
                else status++;
                if (status == 3)
                    Notificador.ApresentarMensagem("Nenhum Dado Para Deletar", "atencao");
                else
                    Notificador.ApresentarMensagem("Dados Deletados Com Sucesso", "sucesso");
            }
            else if (opcao == "s")
                tela = new TelaSair("Sair");
            return tela;
        }
        private void PopularAplicacao()
        {
            //var garcom = new Garcom("Julinho", "230.232.519-98");
            //repositorioGarcom.Inserir(garcom);

            Tarefa tarefa1 = new Tarefa("Casa", 2, DateTime.Parse("11/09/2019"), 2);
            Tarefa tarefa2 = new Tarefa("Mercado", 2, DateTime.Parse("09/04/2018"), 1);
            Tarefa tarefa3 = new Tarefa("Trabalho", 3, DateTime.Parse("20/05/2015"), 3);
            Tarefa tarefa4 = new Tarefa("Transporte", 1, DateTime.Parse("18/06/2017"), 3);
            Tarefa tarefa5 = new Tarefa("Finanças", 3, DateTime.Parse("12/11/2016"), 2);
            Tarefa tarefa6 = new Tarefa("Férias", 1, DateTime.Parse("14/07/2020"), 3);

            repositorioTarefa.Inserir(tarefa1);
            repositorioTarefa.Inserir(tarefa2);
            repositorioTarefa.Inserir(tarefa3);
            repositorioTarefa.Inserir(tarefa4);
            repositorioTarefa.Inserir(tarefa5);
            repositorioTarefa.Inserir(tarefa6);

            Contato contato1 = new Contato("José", "joseE@gmail.com", "(99) 9 9399 - 4855",
                "NDD", "RH");
            Contato contato2 = new Contato("Paulo", "pPaulo@orkut.com", "(59) 9 6641 - 4412",
                "NDD", "Suporte");
            Contato contato3 = new Contato("Ricardo", "Ri_Cardo@outlook.com", 
                "(33) 9 2251 - 3355", "NDD", "Suporte");

            repositorioContato.Inserir(contato1);
            repositorioContato.Inserir(contato2);
            repositorioContato.Inserir(contato3);
        }
        private void SalvarAplicacao()
        {
            RepositorioBase<Compromisso> _repositorioCompromisso = (RepositorioBase<Compromisso>)repositorioCompromisso;
            GerenciadorArquivos.SalvarArquivo<Compromisso>("\\Compromisso.xml", _repositorioCompromisso);

            RepositorioBase<Contato> _repositorioContato = (RepositorioBase<Contato>)repositorioContato;
            GerenciadorArquivos.SalvarArquivo<Contato>("\\Contato.xml", _repositorioContato);

            RepositorioBase<Tarefa> _repositorioTarefa = (RepositorioBase<Tarefa>)repositorioTarefa;
            GerenciadorArquivos.SalvarArquivo("\\Tarefa.xml", _repositorioTarefa);

        }
        private void CarregarAplicacao()
        {
            RepositorioCompromisso _repositorioCompromisso = (RepositorioCompromisso)
                GerenciadorArquivos.CarregarArquivo<Compromisso>("\\Compromisso.xml");
            RepositorioTarefa _repositorioTarefa = (RepositorioTarefa)
                GerenciadorArquivos.CarregarArquivo<Tarefa>("\\Tarefa.xml");
            RepositorioContato _repositorioContato = (RepositorioContato)
                GerenciadorArquivos.CarregarArquivo<Contato>("\\Contato.xml");
            if (_repositorioContato == null && _repositorioCompromisso == null && _repositorioTarefa == null)
            {
                Notificador.ApresentarMensagem("Nenhum Dado Para Carregar", "atencao");
                return;
            }
            repositorioTarefa = _repositorioTarefa;
            repositorioContato = _repositorioContato;
            repositorioCompromisso = _repositorioCompromisso;
            Notificador.ApresentarMensagem("Dados Carregados Com Sucesso", "sucesso");
        }
    }
}
