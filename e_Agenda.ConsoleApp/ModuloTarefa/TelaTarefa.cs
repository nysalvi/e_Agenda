using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    internal class TelaTarefa : TelaBase, ITela
    {
        private RepositorioTarefa repositorio;
        public TelaTarefa(RepositorioTarefa repositorio) : base("Cadastro de Tarefas")
        {
            this.repositorio = repositorio;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Nova Tarefa");
            Tarefa tarefa = Obter();
            string status = repositorio.Inserir(tarefa);
            if (status == "sucesso")
                Notificador.ApresentarMensagem(status, "sucesso");
            else
                Notificador.ApresentarMensagem(status, "erro");
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa");

            bool temTarefaCadastradas = VisualizarRegistro();

            if (temTarefaCadastradas == false)
            {
                Notificador.ApresentarMensagem("Nenhuma Tarefa cadastrada para poder editar.", "atencao");
                return;
            }
            int numeroTarefa = ObterNumeroTarefa();
            if (numeroTarefa == -1)
                return;

            Tarefa tarefaAtualizada = ObterEditar(repositorio.Registros[numeroTarefa]);
            repositorio.Editar(tarefaAtualizada, numeroTarefa);
            
            if (tarefaAtualizada.Validar())
                Notificador.ApresentarMensagem("Tarefa editada com sucesso", "sucesso");
            else
                Notificador.ApresentarMensagem("Não foi possível editar.", "erro");
        }
        public void ExcluirRegistro() 
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastradas = VisualizarRegistro();

            if (!temTarefasCadastradas)
            {
                Notificador.ApresentarMensagem(
                    "Nenhuma Tarefa cadastrada para poder excluir", "atencao");
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            if (numeroTarefa == -1)
            {
                Notificador.ApresentarMensagem("Não foi possível encontrar a Tarefa.", "erro");
                return;
            }
            bool conseguiuExcluir = repositorio.Excluir(numeroTarefa);
            if (!conseguiuExcluir)
                Notificador.ApresentarMensagem("Não foi possível excluir.", "sucesso");
            else
                Notificador.ApresentarMensagem("Tarefa excluído com sucesso!", "sucesso");
        }
        public bool VisualizarRegistro()
        {
            MostrarTitulo("Visualizando Tarefa");

            List<Tarefa> tarefas = repositorio.Registros;

            if (tarefas.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", "atencao");
                return false;
            }
            tarefas.Sort();
            tarefas.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
                Console.WriteLine();
            });            
            Console.ReadLine();
            return true;
        }
        public bool VisualizarPendentes()
        {
            MostrarTitulo("Visualizando Tarefas Pendentes");

            List<Tarefa> tarefas = repositorio.Filtrar(x => x.Percentual != 100);

            if (tarefas.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", "atencao");
                return false;
            }
            tarefas.Sort();
            tarefas.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
                Console.WriteLine();
            });
            Console.ReadLine();
            return true;
        }
        public bool VisualizarConcluidos()
        {
            MostrarTitulo("Visualizando Tarefas Concluídas");

            List<Tarefa> tarefas = repositorio.Filtrar(x => x.Percentual == 100);

            if (tarefas.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", "atencao");
                return false;
            }
            tarefas.Sort();
            tarefas.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
                Console.WriteLine();
            });
            Console.ReadLine();
            return true;
        } 
        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Adicionar um Item");
            Console.WriteLine("Digite 6 para Concluir um Item");
            Console.WriteLine("Digite 7 para Visualizar por Registros Concluídos");
            Console.WriteLine("Digite 8 para Visualizar por Registros Pendentes");
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void AdicionarItem()
        {
            MostrarTitulo("Selecione Uma Tarefa para Criar Um Item : ");

            bool temTarefasCadastradas = VisualizarRegistro();
            if (!temTarefasCadastradas)
            {
                Notificador.ApresentarMensagem(
                    "Nenhuma Tarefa cadastrada para Poder Excluir", "atencao");
                return;
            }
            int numeroTarefa = ObterNumeroTarefa();
            if (numeroTarefa == -1)
            {
                Notificador.ApresentarMensagem("Não foi possível Encontrar a Tarefa.", "Erro");
                return;
            }
            Console.WriteLine("Digite Quantos Itens Deseja Criar: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < quantidade; i++)
            {
                Item item = ObterItem();
                if (!item.Validar())
                {
                    Notificador.ApresentarMensagem("Item Inválido.", "Erro");
                    i--;
                    continue;
                }
                Tarefa tarefa = repositorio.SelecionarRegistro(numeroTarefa);
                tarefa.AdicionarItem(item);
            }
            Notificador.ApresentarMensagem("Itens Adicionados Com SUCESSO !!!", "sucesso");
        }
        public void ConcluirItem()
        {
            MostrarTitulo("Selecione Uma Tarefa para Concluir Um Item : ");

            bool temTarefasCadastradas = VisualizarRegistro();
            if (!temTarefasCadastradas)
            {
                Notificador.ApresentarMensagem(
                    "Nenhuma Tarefa Cadastrada Para Poder Excluir", "Atencao");
                return;
            }
            int numeroTarefa = ObterNumeroTarefa();
            if (numeroTarefa == -1)
                return;

            Tarefa tarefa = repositorio.SelecionarRegistro(numeroTarefa);

            if (!tarefa.VisualizarItem())
                return;

            int numeroItem = ObterNumeroItem(tarefa);
            if (numeroItem == -1)
            {
                Notificador.ApresentarMensagem("Item não Encontrado.", "Erro");
                return;
            }
            tarefa.ConcluirItem(numeroItem);

            Console.ReadLine();
        }
        protected Tarefa Obter()
        {
            Console.Write("Digite o Título da Tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o Nível de Prioridade(1 - Baixo | 2 - Medio | 3 - Alto): ");
            int prioridade = Convert.ToInt32(Console.ReadLine());            

            Console.Write("Digite a Data de criação da Tarefa: ");
            DateTime criacao = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite o Periodo para a Tarefa(1 - Diário | 2 - Semanal | 3 - Mensal): ");
            int periodo =  Convert.ToInt32(Console.ReadLine());

            return new Tarefa(titulo, prioridade, criacao, periodo);            
        }
        protected Tarefa ObterEditar(Tarefa antiga)
        {
            Console.Write("Digite o Título da Tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o Nível de Prioridade(1 - Baixo | 2 - Medio | 3 - Alto): ");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o Periodo para a Tarefa(1 - Diário | 2 - Semanal | 3 - Mensal): ");
            int periodo = Convert.ToInt32(Console.ReadLine());

            return new Tarefa(titulo, prioridade, antiga.Criacao, periodo);
        }
        protected int ObterNumeroTarefa()
        {
            int numeroTarefa;
            bool numeroTarefaEncontrada;
            Console.Write("Digite o Número da Tarefa que Deseja Selecionar: ");
            numeroTarefa = Convert.ToInt32(Console.ReadLine());

            numeroTarefaEncontrada = repositorio.Registros.Find(x => x.Numero == numeroTarefa) != null;

            if (!numeroTarefaEncontrada)
            {
                Notificador.ApresentarMensagem("Número da tarefa não Encontrado, Tente Novamente.",
                    "atencao");
                numeroTarefa = -1;
            }
            return numeroTarefa;
        }
        private int ObterNumeroItem(Tarefa t)
        {
            int numeroItem = -1;
            bool numeroItemEncontrada;
            Console.Write("Digite o número do Item que deseja selecionar: ");
            numeroItem = Convert.ToInt32(Console.ReadLine());
            numeroItemEncontrada = t.itens.Find(x => x.Numero == numeroItem) != null;
            //numeroItemEncontrada = t.itens.Find(x => x.Numero == numeroItem) != null;
            if (!numeroItemEncontrada)
                Notificador.ApresentarMensagem("Número do item não Encontrado, Tente Novamente.",
                    "atencao");
            return numeroItem;
        }
        protected Item ObterItem()
        {
            Console.WriteLine("Digite a Descrição do Item: ");

            string descricao = Console.ReadLine();
            Item item = new Item(descricao);
            if (!item.Validar())
                return null;
            return item;
        }
    }
}
