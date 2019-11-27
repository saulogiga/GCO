using GCO.Dominio.Entidade;
using GCO.Processo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;

namespace GCO.ProcessoDePublish
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer timer;
        private bool executando = false;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (!EventLog.SourceExists("ProcessoDePublish"))
                {
                    EventLog.CreateEventSource("ProcessoDePublish", "Application");
                }
                EventLog.WriteEntry("ProcessoDePublish", "Serviço iniciado.");
                

                timer = new System.Timers.Timer(5000);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Start();

                timer_Elapsed(null, null);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("ProcessoDePublish", "Serviço com problemas: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        protected override void OnStop()
        {
            timer.Stop();
            EventLog.WriteEntry("ProcessoDePublish", "Serviço parado.");
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!executando)
                {
                    executando = true;
                    //EventLog.WriteEntry("ProcessoDePublish", "Executando...");
                    new Thread(new ThreadStart(Executar)).Start();
                }
            }
            catch (Exception ex)
            {
                executando = false;
                EventLog.WriteEntry("ProcessoDePublish", "Timer com problemas: " + ex.Message + "\n" + ex.StackTrace);
                throw;
            }
        }

        public void Executar()
        {
            try
            {
                //EventLog.WriteEntry("ProcessoDePublish", "Publicar");
                var processo = new ProcessoDePublicacaoDeRelease();

                var publicacao = processo.ListarPendentes();

                //EventLog.WriteEntry("ProcessoDePublish", "Itens " + publicacao.Count());

                foreach (PublicacaoDeRelease p in publicacao)
                {
                    Publicar(p);
                }

                executando = false;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("ProcessoDePublish", "Publish com problemas: " + ex.Message + "\n" + ex.StackTrace);
                throw;
            }
        }

        private bool Publicar(PublicacaoDeRelease publicacao)
        {
            EventLog.WriteEntry("ProcessoDePublish", "Publicando...");
            var processo = new ProcessoDePublicacaoDeRelease();
            try
            {
                //******************//

                var servicoDePublicacao = new ServicoDePublicacao.ServiceClient();

                publicacao.DataInicioPublicacao = servicoDePublicacao.ObterHora();
                publicacao.IdPublicacaoStatus = (int)EnumDeStatusDePublicacao.PUBLICANDORELEASE;
                publicacao.Detalhes += "\n\n" + publicacao.DataInicioPublicacao.Value.ToShortTimeString() + " - Compactando os dados...";


                var dirNaoSelecionados = publicacao.DirNaoSelecionados.Split(';');
                var arqNaoSelecionados = publicacao.ArqNaoSelecionados.Split(';');


                var caminho = Directory.GetParent(publicacao.CaminhoOrigem).FullName + @"\";
                var caminhoTransferencia = @"\\192.168.0.78\e$\Publish\";
                var arquivoZip = Guid.NewGuid().ToString() + ".zip";
                var pastaDestino = arquivoZip.Substring(0, 3);


                if (!Directory.Exists(caminho + pastaDestino))
                    Directory.CreateDirectory(caminho + pastaDestino);

                SepararArquivos(caminho + publicacao.PastaOrigem, caminho + pastaDestino, dirNaoSelecionados, arqNaoSelecionados);


                ZipFile.CreateFromDirectory(caminho + "\\" + pastaDestino, caminho + "\\" + arquivoZip);

                Directory.Delete(caminho + "\\" + pastaDestino, true);

                //******************//
                publicacao.Detalhes += "\n" + DateTime.Now.ToShortTimeString() + " - Copiando os arquivos para o servidor de homologação...";
                processo.Atualizar(publicacao);

                #region Copia para o servidor de Homologação

                using (new NetworkConnection(caminhoTransferencia, new NetworkCredential(@"Foxconn\srv_gco", "TK010528@")))
                {
                    if (Directory.Exists(caminhoTransferencia))
                    {
                        System.IO.File.Copy(caminho + "\\" + arquivoZip, caminhoTransferencia + arquivoZip);
                    }
                }

                #endregion


                System.IO.File.Delete(caminho + "\\" + arquivoZip);

                //publicacao.UnidadeDeDesenvolvimento.CaminhoPublish = @"\\192.168.0.78\e$\WebSites\Testes";

                var retorno = servicoDePublicacao.Publicar(publicacao.IdPublicacaoRelease, arquivoZip, publicacao.UnidadeDeDesenvolvimento.CaminhoPublish + "\\" + publicacao.CaminhoDestino, "9A1B374DF2B72403");

                return true;
            }
            catch 
            {
                publicacao.DataFimPublicacao = DateTime.Now;
                publicacao.PublicacaoStatus.IdPublicacaoStatus = (int)EnumDeStatusDePublicacao.ERROPUBLICAÇÃO;
                processo.Atualizar(publicacao);
                
                return false;
            }
        }

        private void SepararArquivos(string caminho, string caminhoSalvar, string[] dirNaoSelecionados, string[] arqNaoSelecionados)
        {
            var arquivos = Directory.EnumerateFiles(caminho);

            FileInfo arquivo;
            DirectoryInfo diretorio;

            foreach (string arq in arquivos)
            {
                arquivo = new FileInfo(arq);

                if (!arqNaoSelecionados.Contains(arquivo.Name))
                    System.IO.File.Copy(arquivo.FullName, caminhoSalvar + "\\" + arquivo.Name, true);
            }

            var diretorios = Directory.EnumerateDirectories(caminho);

            foreach (string dir in diretorios)
            {
                diretorio = new DirectoryInfo(dir);
                if (!dirNaoSelecionados.Contains(diretorio.Name))
                {
                    if (!Directory.Exists(caminhoSalvar + "\\" + diretorio.Name))
                        Directory.CreateDirectory(caminhoSalvar + "\\" + diretorio.Name);

                    if (Directory.EnumerateFileSystemEntries(diretorio.FullName).Count() > 0)
                    {
                        SepararArquivos(diretorio.FullName, caminhoSalvar + "\\" + diretorio.Name, dirNaoSelecionados, arqNaoSelecionados);
                    }
                }
            }
        }
    }
}
