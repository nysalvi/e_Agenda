using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Compartilhado;
namespace e_Agenda.ConsoleApp.ModuloTarefa
{

    internal class Tarefa : EntidadeBase, IComparable<Tarefa>
    {
        public List<Item> itens;
        public int NumeroItem { get; set; }
        public string Titulo { get; set; }
        private int _Prioridade { get; set; }
        public string Prioridade{ get {
            if (_Prioridade == 1)
                return "Baixo";
            else if (_Prioridade == 2)
                return "Medio";
            else if (_Prioridade == 3)
                return "Alto";
            return ""; }
        }
        public DateTime Criacao { get; set; }
        public DateTime Devolucao { get; set; }
        public decimal Percentual { get; set; }
        public int Periodo { get; set; }

        public Tarefa(string titulo, int prioridade, DateTime criacao, int periodo)
        {
            itens = new List<Item>();
            Titulo = titulo;
            _Prioridade = prioridade;
            Criacao = criacao;
            Percentual = 0;
            Periodo = periodo;
            NumeroItem = 0;
            if (Periodo == 1)
                Devolucao = Criacao.AddDays(1);
            else if (Periodo == 2)
                Devolucao = Criacao.AddDays(7);
            else if (Periodo == 3)
                Devolucao = Criacao.AddDays(30);
        }
        public void ConcluirItem(int item)
        {            
            if (itens[item - 1].Concluido)
            {
                Notificador.ApresentarMensagem("Item já está concluido", "atencao");
                return;
            }
            itens[item - 1].Concluido = true;
            AtualizaPorcentagem();
            Notificador.ApresentarMensagem("Item concluído com Sucesso !!!", "sucesso");
        }
        public void AtualizaPorcentagem()
        {
            Percentual = 0;
            itens.ForEach(i =>
            {
                if (i.Concluido)
                    Percentual += (100 / itens.Count);
            });
        }
        public override bool Validar()
        {
            /*
            if (string.IsNullOrEmpty(Titulo))
                return "O título não pode ser VAZIO !!!";
            if (Prioridade != 1 && Prioridade != 2 && Prioridade != 3)
                return "A prioridade deve ser 0-1-2 !!!";
            if (string.IsNullOrEmpty(Periodo) && 
                Periodo != "diario" && Periodo != "semanal" && Periodo != "mensal")
                return "O período deve ser Diário, Semanal ou Mensal";
            if (Criacao.Equals("01/01/1900"))
                return "A data não pode ser menor que 01/01/1900";
            */
            if (string.IsNullOrEmpty(Titulo))
                return false;
            if (_Prioridade != 1 && _Prioridade != 2 && _Prioridade != 3)
                return false;
            if (Periodo != 1 && Periodo != 2 && Periodo != 3)
                return false;
            if (Criacao.CompareTo(DateTime.Parse("01/01/1900")) == -1)
                return false;
            return true;
        }
        public bool VisualizarItem()
        {            
            if (itens.Count == 0)
            {
                Notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", "atencao");
                return false;
            }

            itens.ForEach(x =>
            {
                Console.WriteLine(x.ToString());
                Console.WriteLine();
            });
            return true;
        }
        public void AdicionarItem(Item item)
        {
            item.Numero = ++NumeroItem;
            itens.Add(item);
            AtualizaPorcentagem();
        }
        public override string ToString()
        {
            return "ID " + Numero + " :\n\tTítulo : " + Titulo + "\n\tPrioridade : " + Prioridade 
                + "\n\tData Criação: " + Criacao + "\n\tDevolução : " + Devolucao + 
                "\n\tPercentual Concluído: " + Percentual + "\n\t//////////";
        }

        public int CompareTo(Tarefa other)
        {
            return other._Prioridade.CompareTo(_Prioridade);
        }
    }
}
