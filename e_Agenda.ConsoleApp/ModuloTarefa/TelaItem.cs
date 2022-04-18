using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.Tarefa
{
    internal class TelaItem : TelaBase, ITela
    {
        public TelaItem(string titulo) : base(titulo)
        {

        }

        public void AdicionarItem(Tarefa tarefa)
        {
            MostrarTitulo("Selecione Uma Tarefa para Criar Um Item : ");

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
            Console.WriteLine("Digite Quantos Itens deseja criar: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < quantidade; i++)
            {
                Item item = ObterItem();
                if (!item.Validar())
                {
                    Notificador.ApresentarMensagem("Item inválido.", "erro");
                    i--;
                    continue;
                }
                Tarefa tarefa = repositorio.SelecionarRegistro(numeroTarefa);
                tarefa.AdicionarItem(item);
            }

            public void InserirRegistro()
        {
            throw new System.NotImplementedException();
        }
        public void EditarRegistro()
        {
            throw new System.NotImplementedException();
        }
        public bool VisualizarRegistro()
        {
            throw new System.NotImplementedException();
        }
        public void ExcluirRegistro()
        {
            throw new System.NotImplementedException();
        }
    }
}
