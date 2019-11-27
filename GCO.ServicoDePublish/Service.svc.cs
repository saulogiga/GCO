using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using GCO.Processo;
using GCO.Dominio.Entidade;

namespace GCO.ServicoDePublish
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService
    {
        private PublicacaoDeRelease publicacao = null;

        public string Publicar(Int64 idPublicacaoRelease, string arquivo, string caminho, string chave)
        {
            var processo = new ProcessoDePublicacaoDeRelease();
            publicacao = processo.Obter(idPublicacaoRelease);

            if (chave == "9A1B374DF2B72403")
            {
                try
                {

                    //Altera para o caminho físico, caso tenha informado o caminho de rede. 
                    caminho = caminho.Replace(Directory.GetParent(caminho).Root.ToString(), Directory.GetParent(caminho).Root.Name.Replace("$", ":"));

                    //if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["LogPublish"].ToString()))
                    //    Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["LogPublish"].ToString());



                    publicacao.Detalhes += "\n\n" + DateTime.Now.ToShortTimeString() + " - Extraindo arquivo " + arquivo;

                    ZipFile.ExtractToDirectory(System.Configuration.ConfigurationManager.AppSettings["CaminhoPublish"].ToString() + arquivo, System.Configuration.ConfigurationManager.AppSettings["CaminhoPublish"].ToString() + "\\" + arquivo.Replace(".zip", ""));

                    publicacao.Detalhes += "\n" + DateTime.Now.ToShortTimeString() + " - Aquivos publicados:";

                    PublicarArquivos(System.Configuration.ConfigurationManager.AppSettings["CaminhoPublish"].ToString() + "\\" + arquivo.Replace(".zip", ""), caminho);

                    publicacao.Detalhes += "\n" + DateTime.Now.ToShortTimeString() + " - Apagando o diretório " + System.Configuration.ConfigurationManager.AppSettings["CaminhoPublish"].ToString() + "\\" + arquivo.Replace(".zip", "");

                    Directory.Delete(System.Configuration.ConfigurationManager.AppSettings["CaminhoPublish"].ToString() + "\\" + arquivo.Replace(".zip", ""), true);

                    //LimparArquivos();

                    publicacao.Detalhes += "\n\n" + "TEMPO DO PUBLISH: " + DateTime.Now.Subtract(publicacao.DataInicioPublicacao.Value.TimeOfDay).ToLongTimeString();
                    publicacao.Detalhes += "\n" + "PUBLISH REALIZADO COM SUCESSO.";

                    publicacao.DataFimPublicacao = DateTime.Now;
                    publicacao.IdPublicacaoStatus = (int)EnumDeStatusDePublicacao.PUBLICAÇÃOREALIZADACOMSUCESSO;
                    processo.Atualizar(publicacao);

                    return string.Empty;
                }
                catch (Exception exc)
                {
                    publicacao.Detalhes += "\n\n" + "ERRO: " + exc.Message;
                    publicacao.Detalhes += "\n" + exc.StackTrace;
                    publicacao.DataFimPublicacao = DateTime.Now;
                    publicacao.IdPublicacaoStatus = (int)EnumDeStatusDePublicacao.ERROPUBLICAÇÃO;
                    processo.Atualizar(publicacao);

                    return "ERRO: " + exc.Message + "\n" + exc.StackTrace;
                }
            }
            else
            {
                return "ERRO: Chave inválida.";
            }

            
        }

        private void PublicarArquivos(string caminho, string caminhoSalvar)
        {
            var arquivos = Directory.EnumerateFiles(caminho);

            FileInfo arquivo;
            DirectoryInfo diretorio;

            foreach (string arq in arquivos)
            {
                arquivo = new FileInfo(arq);
                publicacao.Detalhes += "\n" + "             " + arquivo.FullName;
                System.IO.File.Copy(arquivo.FullName, caminhoSalvar + "\\" + arquivo.Name, true);
            }

            var diretorios = Directory.EnumerateDirectories(caminho);

            foreach (string dir in diretorios)
            {
                diretorio = new DirectoryInfo(dir);

                if (!Directory.Exists(caminhoSalvar + "\\" + diretorio.Name))
                    Directory.CreateDirectory(caminhoSalvar + "\\" + diretorio.Name);

                if (Directory.EnumerateFileSystemEntries(diretorio.FullName).Count() > 0)
                {
                    PublicarArquivos(diretorio.FullName, caminhoSalvar + "\\" + diretorio.Name);
                }

            }
        }

        private void LimparArquivos()
        {
            var arquivos = Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings["CaminhoPublish"].ToString());
            FileInfo arquivo;

            foreach (string arq in arquivos)
            {
                arquivo = new FileInfo(arq);

                if (arquivo.LastAccessTime.Date < DateTime.Now)
                    arquivo.Delete();
            }
        }

        public DateTime ObterHora()
        {
            return DateTime.Now;
        }

        public IEnumerable<string> ObterPastas(string caminho)
        {
            caminho = caminho.Replace(Directory.GetParent(caminho).Root.ToString(), Directory.GetParent(caminho).Root.Name.Replace("$", ":"));

            var diretorio = new DirectoryInfo(caminho);
            var dirs = new List<string>();

            foreach (DirectoryInfo dir in diretorio.EnumerateDirectories())
            {
                dirs.Add(dir.Name);
            }
            return dirs;
        }
    }
}
