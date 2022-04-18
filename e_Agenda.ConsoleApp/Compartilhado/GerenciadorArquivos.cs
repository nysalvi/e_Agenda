using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
namespace e_Agenda.ConsoleApp.Compartilhado
{
    internal class GerenciadorArquivos
    {     
        
        public static void SalvarArquivo<T>(string filename, RepositorioBase<T> repositorioBase) where T : EntidadeBase
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RepositorioBase<T>));

            string file = Path.GetDirectoryName(filename);
            TextWriter textWriter = new StreamWriter(file);
            if (File.Exists(file)) File.Delete(file);
            xmlSerializer.Serialize(textWriter, repositorioBase);
            textWriter.Close();
        }
        public static RepositorioBase<T>CarregarArquivo<T>(string filename) where T : EntidadeBase
        {            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(RepositorioBase<>));

            string file = Path.GetDirectoryName(filename);
            TextReader textReader = new StreamReader(file);
            RepositorioBase<T> novoRepositorio;
            if (File.Exists(file))
            {
                novoRepositorio = (RepositorioBase<T>)xmlSerializer.Deserialize(textReader);
                textReader.Close();
                return novoRepositorio;
            }
            textReader.Close();
            return null;
        }
    }
}
