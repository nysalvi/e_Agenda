using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Contato;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Compromisso
{
    internal class TelaCompromisso : TelaBase, ITela
    {
        private RepositorioCompromisso repositorio;

        private readonly TelaContato telaContato;
        private readonly RepositorioContato repositorioContato;
        public TelaCompromisso(RepositorioCompromisso repositorio, TelaContato telaContato,
            RepositorioContato repositorioContato): base("Cadastro de Amigos")
        {
            this.repositorio = repositorio;
            this.telaContato = telaContato;
            this.repositorioContato = repositorioContato;
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
            int numeroContato = ObterNumeroCompromisso();
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

            int numeroContato = ObterNumeroCompromisso();

            if (numeroContato == -1)
            {
                Notificador.ApresentarMensagem("Não foi possível encontrar o Compromisso.", "erro");
                return;
            }
            bool conseguiuExcluir = repositorio.Excluir(numeroContato);
            if (!conseguiuExcluir)
                Notificador.ApresentarMensagem("Não foi possível excluir.", "sucesso");
            else
                Notificador.ApresentarMensagem("Compromisso excluído com sucesso!", "sucesso");
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
            });
            Console.ReadLine();
            return true;
        }
        public bool VisualizarRegistroFuturo()
        {
            MostrarTitulo("Visualizando Compromissos Futuros");

            List<Compromisso> compromissos = repositorio.Filtrar(x => x.Data > DateTime.Now);

            if (compromissos.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhum Compromisso disponível.", "atencao");
                return false;
            }

            compromissos.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
            });
            Console.ReadLine();
            return true;
        }
        public bool VisualizarRegistroPassado()
        {
            MostrarTitulo("Visualizando Compromissos Passados");

            List<Compromisso> compromissos = repositorio.Filtrar(x=> x.Data < DateTime.Now);

            if (compromissos.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhum Compromisso disponível.", "atencao");
                return false;
            }

            compromissos.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
            });
            Console.ReadLine();
            return true;
        }
        protected Compromisso Obter()
        {
            Console.Write("Digite o Assunto: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite o do Local: ");
            string local = Console.ReadLine();

            Console.WriteLine("Digite a Data do Compromisso: ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Hora : ");
            int hora = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite os Minutos : ");
            int minuto = Convert.ToInt32(Console.ReadLine());

            TimeSpan ts = new TimeSpan(hora, minuto, 0);

            Console.WriteLine("Digite a Hora Final : ");
            int horaFinal = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite os Minutos Finais: ");
            int minutoFinal = Convert.ToInt32(Console.ReadLine());

            TimeSpan tsFinal = new TimeSpan(horaFinal, minutoFinal, 0);

            Console.WriteLine("Digite s para Associar o Compromisso a um Contato : ");
            string haveraContato = Console.ReadLine();
            if (haveraContato != "s")
                return new Compromisso(assunto, local, data, ts, tsFinal, null);
            if (!telaContato.VisualizarRegistro())
            {
                Notificador.ApresentarMensagem("Nao Existem Contatos !!!\n\tCompromisso criado sem um contato " +
                    "associado a ele" ,"atencao");
                return new Compromisso(assunto, local, data, ts, tsFinal, null);
            }
            int posicao = telaContato.ObterNumeroContato();
            if (posicao == -1)
                return null;
            Contato.Contato contato = repositorioContato.SelecionarRegistro(posicao);
            return new Compromisso(assunto, local, data, ts, tsFinal, contato);

        }

        private int ObterNumeroCompromisso()
        {
            int numeroCompromisso = -1;
            bool numeroCompromissoEncontrado;
            Console.Write("Digite o número do Compromisso que deseja selecionar: ");
            numeroCompromisso = Convert.ToInt32(Console.ReadLine());

            numeroCompromissoEncontrado = repositorio.Registros.Find(x => x.Numero == numeroCompromisso) != null;

            if (numeroCompromissoEncontrado == false)
            {
                Notificador.ApresentarMensagem("Número do Compromisso não encontrado, tente novamente.",
                    "atencao");
                numeroCompromisso = -1;
            }
            return numeroCompromisso;
        }

    }
}
