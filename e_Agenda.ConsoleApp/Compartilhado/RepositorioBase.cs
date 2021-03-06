using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace e_Agenda.ConsoleApp.Compartilhado
{
    [XmlInclude(typeof(RepositorioBase<EntidadeBase>))]
    //[Serializable]            
    public class RepositorioBase<T> : IXmlSerializable where T : EntidadeBase 
    {
        protected List<T> registros;
        public List<T> Registros => registros;
        protected int contador;
        protected string file;

        public RepositorioBase()
        {
            registros = new List<T>();
        }
        public RepositorioBase(string filename)
        {
            registros = new List<T>();
            file = AppDomain.CurrentDomain.BaseDirectory + "src" + filename;
        }
        public virtual string Inserir(T entidade)
        {
            if (!entidade.Validar())
                return "erro";
            entidade.Numero = ++contador;
            registros.Add(entidade);    
            return "sucesso";
        }
        public void Editar(T entidade, int posicao)
        {                        
            //registros.Remove(entidade);
            entidade.Numero = posicao;
            //registros.Add(entidade);
            registros[posicao] = entidade;
        }
        public bool Excluir(int posicao)
        {
            T removido = registros.Find(x => x.Numero == posicao);
            if (removido != null)
            {
                registros.Remove(removido);
                return true;
            }
            return false;

        }
        public List<T> Filtrar(Predicate<T> condicao)
        {
            return registros.FindAll(condicao);
        }
        
        public T SelecionarRegistro(int idSelecionado)
        {
            foreach (T registro in registros)
            {
                if (idSelecionado == registro.Numero)
                    return registro;
            }
            return null;
        }
        public bool Comparar(Predicate<T> condicao)
        {            
            foreach (T entidade in registros)
                if (condicao(entidade))
                    return true;

            return false;
        }
        public XmlSchema GetSchema()
        {
            //throw new NotImplementedException();
            return null;
        }
        public void ReadXml(XmlReader reader)
        {
            
        }
        public void WriteXml(XmlWriter writer)
        {
            
        }
    }
}
