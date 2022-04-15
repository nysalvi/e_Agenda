using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Compromisso
{
    internal class TelaCompromisso : TelaBase, ITela
    {
        private RepositorioCompromisso repositorio;
        public TelaCompromisso(RepositorioCompromisso repositorio) : base("Cadastro de Amigos")
        {
            this.repositorio = repositorio;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Novo Contato");
            Compromisso compromisso = Obter();

            string status = repositorio.Inserir(compromisso);
            if (status == "sucesso")
                Notificador.ApresentarMensagem(status, "sucesso");
            else
                Notificador.ApresentarMensagem(status, "erro");
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Compromisso");

            bool temContatoCadastrados = VisualizarRegistro();

            if (temContatoCadastrados == false)
            {
                Notificador.ApresentarMensagem("Nenhum Compromisso cadastrado para poder editar.", "atencao");
                return;
            }
            int numeroContato = ObterNumeroContato();
            if (numeroContato == -1)
                return;

            Compromisso contatoAtualizado = Obter();
            repositorio.Editar(contatoAtualizado, numeroContato);

            if (contatoAtualizado.Validar())
                Notificador.ApresentarMensagem("Compromisso editado com sucesso", "sucesso");
            else
                Notificador.ApresentarMensagem("Não foi possível editar.", "erro");
        }
        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temAmigosCadastrados = VisualizarRegistro();

            if (!temAmigosCadastrados)
            {
                Notificador.ApresentarMensagem(
                    "Nenhum Compromisso cadastrado para poder excluir", "atencao");
                return;
            }

            int numeroContato = ObterNumeroContato();

            if (numeroContato == -1)
            {
                Notificador.ApresentarMensagem("Não foi possível encontrar o Compromisso.", "erro");
                return;
            }
            bool conseguiuExcluir = repositorio.Excluir(numeroContato);
            if (!conseguiuExcluir)
                Notificador.ApresentarMensagem("Não foi possível excluir.", "sucesso");
            else
                Notificador.ApresentarMensagem("Amigo excluído com sucesso!", "sucesso");
        }
        public bool VisualizarRegistro()
        {
            MostrarTitulo("Visualizando Compromisso");

            List<Compromisso> compromissos = repositorio.Registros;

            if (compromissos.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhum Compromisso disponível.", "atencao");
                return false;
            }

            compromissos.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
                Console.ReadLine();
            });
            return true;
        }
        protected Compromisso Obter()
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

            return new Compromisso();
            //return new Compromisso(nome, email, telefone, empresa, cargo);

        }

        private int ObterNumeroContato()
        {
            int numeroContato = -1;
            bool numeroContatoEncontrado;
            Console.Write("Digite o número do Compromisso que deseja selecionar: ");
            numeroContato = Convert.ToInt32(Console.ReadLine());

            numeroContatoEncontrado = repositorio.Registros.Find(x => x.Numero == numeroContato) != null;

            if (numeroContatoEncontrado == false)
                Notificador.ApresentarMensagem("Número do Compromisso não encontrado, tente novamente.",
                    "atencao");
            return numeroContato;
        }

    }
}
