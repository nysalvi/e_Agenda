using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Contato
{
    internal class TelaContato : TelaBase, ITela
    {
        private RepositorioContato repositorio;
        public TelaContato(RepositorioContato repositorio) : base("Cadastro de Contatos")
        {
            this.repositorio = repositorio;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Novo Contato");
            Contato contato = Obter();

            string status = repositorio.Inserir(contato);
            if (status == "sucesso")
                Notificador.ApresentarMensagem(status, "sucesso");
            else
                Notificador.ApresentarMensagem(status, "erro");
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Contato");

            bool temContatoCadastrados = VisualizarRegistro();

            if (temContatoCadastrados == false)
            {
                Notificador.ApresentarMensagem("Nenhum Contato cadastrado para poder editar.", "atencao");
                return;
            }
            int numeroContato = ObterNumeroContato();
            if (numeroContato == -1)
                return;            

            Contato contatoAtualizado = Obter();
            repositorio.Editar(contatoAtualizado, numeroContato);

            if (contatoAtualizado.Validar())
                Notificador.ApresentarMensagem("Contato editado com sucesso", "sucesso");
            else
                Notificador.ApresentarMensagem("Não foi possível editar.", "erro");
        }
        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Contato");

            bool temAmigosCadastrados = VisualizarRegistro();

            if (!temAmigosCadastrados)
            {
                Notificador.ApresentarMensagem(
                    "Nenhum Contato cadastrado para poder excluir", "atencao");
                return;
            }

            int numeroContato = ObterNumeroContato();

            if (numeroContato == -1)
            {
                Notificador.ApresentarMensagem("Não foi possível encontrar o Contato.", "erro");
                return;
            }
            bool conseguiuExcluir = repositorio.Excluir(numeroContato);
            if (!conseguiuExcluir)
                Notificador.ApresentarMensagem("Não foi possível excluir.", "sucesso");
            else
                Notificador.ApresentarMensagem("Contato excluído com sucesso!", "sucesso");
        }
        public bool VisualizarRegistro()
        {
            MostrarTitulo("Visualizando Contato");                

            List<Contato> contatos = repositorio.Registros;

            if (contatos.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhum contato disponível.", "atencao");
                return false;
            }

            contatos.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
                Console.WriteLine();
            });
            Console.ReadLine();
            return true;
        }
        protected Contato Obter()
        {
            Console.Write("Digite o Nome do Contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o Email do Contato: ");
            string email = Console.ReadLine();

            Console.Write("Digite o Número do Telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite a Empresa Onde o Contato Trabalha: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o Cargo da Profissão do Contato: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, email, telefone, empresa, cargo);            

        }

        public int ObterNumeroContato()
        {
            int numeroContato;
            bool numeroContatoEncontrado;
            Console.Write("Digite o número do Contato que deseja selecionar: ");
            numeroContato = Convert.ToInt32(Console.ReadLine());

            Contato contato = repositorio.Registros.Find(x => x.Numero == numeroContato);
            numeroContatoEncontrado = contato != null;
            if (!numeroContatoEncontrado)
            {
                Notificador.ApresentarMensagem("Número do contato não encontrado, tente novamente.", 
                    "atencao");
                return -1;
            }
            return repositorio.Registros.IndexOf(contato);
        }
    }
}
