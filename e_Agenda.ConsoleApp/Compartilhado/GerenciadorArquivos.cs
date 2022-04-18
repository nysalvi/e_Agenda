using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using System;
namespace e_Agenda.ConsoleApp.Compartilhado
{
    public class GerenciadorArquivos
    {
        public static void SalvarArquivo<T>(string filename, RepositorioBase<T> repositorioBase) where T : EntidadeBase
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + "src" + filename;            
            if (File.Exists(file)) File.Delete(file);

            File.Create(file).Close();            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RepositorioBase<T>));
            TextWriter textWriter = new StreamWriter(file);            
            xmlSerializer.Serialize(textWriter, repositorioBase);
            textWriter.Close();
        }
        public static RepositorioBase<T>CarregarArquivo<T>(string filename) where T : EntidadeBase
        {            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(RepositorioBase<>));

            string file = AppDomain.CurrentDomain.BaseDirectory + "src" + filename;
            RepositorioBase<T> novoRepositorio;
            if (File.Exists(file))
            {
                TextReader textReader = new StreamReader(file);
                novoRepositorio = (RepositorioBase<T>)xmlSerializer.Deserialize(textReader);
                textReader.Close();
                return novoRepositorio;
            }
            return null;
        }
    }
}
