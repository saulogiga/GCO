using GCO.Apresentacao.Models;
using GCO.Dominio.Entidade;
using GCO.Processo;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;


namespace GCO.Apresentacao.Controllers
{
    public class BuildController : Controller
    {
        public string _userName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["_userName"].ToString();
            }
        }

        public string _password
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["_password"].ToString();
            }
        }

        private ServicoDePublicacao.ServiceClient servicoDePublicacao = new ServicoDePublicacao.ServiceClient();

        //
        // GET: /Build/

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "GCO,AnalistaDeProjeto")]
        public ActionResult Release(int idUnidadeDesenvolvimento)
        {

            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var processoDePublicacaoDeRelease = new ProcessoDePublicacaoDeRelease();
            var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });
            var CaminhoRelease = unidadeDeDesenvolvimento.CaminhoBuild;
            var CaminhoPublish = unidadeDeDesenvolvimento.CaminhoPublish;
            var releases = new List<ReleaseModels>();
            var diretorio = new DirectoryInfo(CaminhoRelease);

            var publish = new List<ReleaseModels>();


            var diretorios = servicoDePublicacao.ObterPastas(CaminhoPublish);

            foreach (string dir in diretorios)
            {
                publish.Add(new ReleaseModels { Nome = dir });
            }


            var tfs = new TfsTeamProjectCollection(new Uri(System.Configuration.ConfigurationManager.AppSettings["CaminhoTFS"].ToString()), new NetworkCredential(@"IPLAYERS\srv_tfs", "57lg4PbH@4"));
            tfs.EnsureAuthenticated();

            Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity teste;
            tfs.GetAuthenticatedIdentity(out teste);


            var buildServer = tfs.GetService<IBuildServer>();

            var buildDetailSpec = buildServer.CreateBuildDetailSpec(unidadeDeDesenvolvimento.TeamProject, unidadeDeDesenvolvimento.NomeBuild);
            //var buildDetailSpec = buildServer.CreateBuildDetailSpec(unidadeDeDesenvolvimento.TeamProject);
            buildDetailSpec.QueryOrder = BuildQueryOrder.StartTimeDescending;
            var builds = buildServer.QueryBuilds(buildDetailSpec);


            foreach (IBuildDetail b in builds.Builds)
            {
                releases.Add(new ReleaseModels { Nome = b.BuildNumber.Replace("_" + b.FinishTime.ToString("yyyyMMdd"), ""), DataCriacao = b.FinishTime, Caminho = HttpUtility.JavaScriptStringEncode(System.Configuration.ConfigurationManager.AppSettings["CaminhoBuild"].ToString() + unidadeDeDesenvolvimento.NomeBuild + "\\" + b.BuildNumber), BuildStatus = b.Status.ToString() });
            }


            //foreach (FileSystemInfo dir in diretorio.EnumerateDirectories().OrderByDescending(d => d.Name))
            //{
            //    releases.Add(new ReleaseModels { Nome = dir.Name, DataCriacao = dir.CreationTime, Caminho = HttpUtility.JavaScriptStringEncode(dir.FullName), Tipo = dir.Attributes.ToString()});
            //}




            ViewBag.IdUnidadeDesenvolvimento = unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento;
            ViewBag.Ticket = unidadeDeDesenvolvimento.NumeroTicket;
            ViewBag.Solicitacao = unidadeDeDesenvolvimento.NumeroSolicitacao;
            ViewBag.Projeto = unidadeDeDesenvolvimento.Projeto.Nome;
            ViewBag.Status = unidadeDeDesenvolvimento.StatusUnidadeDesenvolvimento.Nome;
            ViewBag.Publish = publish;

            ViewBag.HistoricoDePublicacao = processoDePublicacaoDeRelease.ListarPorUnidadeDesenvolvimento(idUnidadeDesenvolvimento);

            return View(releases);
        }

        [HttpGet]
        public JsonResult ListarRelease(string caminho)
        {
            //Caso o Output location for SingleFolder, senão considera como PerProject
            if (Directory.Exists(caminho + "\\_PublishedWebsites"))
            {
                caminho += "\\_PublishedWebsites";
            }


            var diretorio = new DirectoryInfo(caminho);
            var releases = new List<ReleaseModels>();

            foreach (FileSystemInfo dir in diretorio.EnumerateDirectories().OrderBy(d => d.Name))
            {
                if (dir.Name != "logs")
                {
                    releases.Add(new ReleaseModels { Nome = dir.Name, DataCriacao = dir.CreationTime, Caminho = HttpUtility.JavaScriptStringEncode(dir.FullName), Tipo = dir.Attributes.ToString() });
                }
            }

            return this.Json(new { Resultado = releases, Release = diretorio.Parent.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPublish(string caminho, string pasta)
        {
            //Caso o Output location for PerProject, senão considera como SingleFolder
            if (Directory.Exists(caminho + "\\_PublishedWebsites"))
            {
                caminho += "\\_PublishedWebsites\\" + pasta;
            }

            var diretorio = new DirectoryInfo(caminho);
            var releases = new List<ReleaseModels>();

            foreach (FileSystemInfo dir in diretorio.EnumerateFileSystemInfos().OrderBy(d => d.Attributes))
            {
                releases.Add(new ReleaseModels { Nome = dir.Name, DataCriacao = dir.CreationTime, Caminho = HttpUtility.JavaScriptStringEncode(dir.FullName), Tipo = dir.Attributes.ToString() });
            }

            return this.Json(new { Resultado = releases, pastaOrigem = diretorio.Name, caminhoDoPublish = caminho }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Publicar(string caminhoOrigem, string pastaOrigem, string caminhoDestino, string ArqNaoSelecionados, string DirNaoSelecionados, int IdUnidadeDesenvolvimento, string release)
        {
            try
            {
                var processoDePublicacaoDeRelease = new ProcessoDePublicacaoDeRelease();
                var publicacaoDeRelease = new PublicacaoDeRelease();

                publicacaoDeRelease.IdPublicacaoStatus = (int)EnumDeStatusDePublicacao.AGUARDANDOPUBLICAÇÃO;
                publicacaoDeRelease.CaminhoOrigem = caminhoOrigem;
                publicacaoDeRelease.PastaOrigem = pastaOrigem;
                publicacaoDeRelease.CaminhoDestino = caminhoDestino;
                publicacaoDeRelease.ArqNaoSelecionados = ArqNaoSelecionados;
                publicacaoDeRelease.DirNaoSelecionados = DirNaoSelecionados;
                publicacaoDeRelease.Release = release;
                publicacaoDeRelease.IdUnidadeDesenvolvimento = IdUnidadeDesenvolvimento;
                publicacaoDeRelease.UserId = WebSecurity.GetUserId(User.Identity.Name);


                processoDePublicacaoDeRelease.Criar(publicacaoDeRelease);

                //HttpContext.Cache.Insert("Status_ID_" + IdUnidadeDesenvolvimento, "Selecionando os arquivos que serão copiados...");

                //var servicoDePublicacao = new ServicoDePublicacao.ServiceClient();
                //var dirNaoSelecionados = DirNaoSelecionados.Split(';');
                //var arqNaoSelecionados = ArqNaoSelecionados.Split(';');
                //var horaInicio = servicoDePublicacao.ObterHora();

                //var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
                //var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = IdUnidadeDesenvolvimento });



                //var caminho = Directory.GetParent(caminhoOrigem).FullName + @"\";
                //var caminhoTransferencia = System.Configuration.ConfigurationManager.AppSettings["CaminhoTransferencia"].ToString();
                //var pastaDestino = Guid.NewGuid().ToString();
                //var arquivoZip = pastaDestino + ".zip";

                //if (!Directory.Exists(caminho + pastaDestino))
                //    Directory.CreateDirectory(caminho + pastaDestino);

                //SepararArquivos(caminho + pastaOrigem, caminho + pastaDestino, dirNaoSelecionados, arqNaoSelecionados);

                //HttpContext.Cache["Status_ID_" + IdUnidadeDesenvolvimento] = "Compactando os dados...";

                //ZipFile.CreateFromDirectory(caminho + "\\" + pastaDestino, caminho + "\\" + arquivoZip);

                //Directory.Delete(caminho + "\\" + pastaDestino, true);

                //HttpContext.Cache["Status_ID_" + IdUnidadeDesenvolvimento] = "Copiando os arquivos para o servidor de homologação...";

                //#region Copia para o servidor de Homologação

                //using (new NetworkConnection(caminhoTransferencia, new NetworkCredential(_userName, _password)))
                //{
                //    if (Directory.Exists(caminhoTransferencia))
                //    {
                //        System.IO.File.Copy(caminho + "\\" + arquivoZip, caminhoTransferencia + arquivoZip);
                //    }
                //}

                //#endregion

                //HttpContext.Cache["Status_ID_" + IdUnidadeDesenvolvimento] = "Publicando os arquivos...";

                //System.IO.File.Delete(caminho + "\\" + arquivoZip);

                //servicoDePublicacao.Publicar(User.Identity.Name, release, arquivoZip, unidadeDeDesenvolvimento.CaminhoPublish + "\\" + caminhoDestino, horaInicio, "9A1B374DF2B72403");

            }
            catch
            {
                return this.Json(new { Resultado = "Não foi possível incluir a publicação." }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                HttpContext.Cache.Remove("Status_ID_" + IdUnidadeDesenvolvimento);
            }

            return this.Json(new { Resultado = "Publicação incluída com sucesso." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AtualizarTela(int IdUnidadeDesenvolvimento)
        {
            var processoDePublicacaoDeRelease = new ProcessoDePublicacaoDeRelease();

            var historico = processoDePublicacaoDeRelease.ListarPorUnidadeDesenvolvimento(IdUnidadeDesenvolvimento);

            var itens = historico.Select(u => new PublicacaoDeRelease
            {
                IdPublicacaoRelease = u.IdPublicacaoRelease,
                IdPublicacaoStatus = u.IdPublicacaoStatus,
                PublicacaoStatus = u.PublicacaoStatus,
                Release = u.Release,
                DataInclusao = u.DataInclusao,
                PastaOrigem = u.PastaOrigem,
                UnidadeDeDesenvolvimento = u.UnidadeDeDesenvolvimento,
                Detalhes = u.Detalhes
            });

            return this.Json(new { Resultado = itens }, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public JsonResult MudarPasta(int idUnidadeDesenvolvimento, string caminho)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });
            var CaminhoPublish = unidadeDeDesenvolvimento.CaminhoPublish;
            var publish = new List<ReleaseModels>();
            var diretorio = new DirectoryInfo(CaminhoPublish + caminho);


            if (diretorio.FullName.Length >= CaminhoPublish.Length)
            {
                caminho = diretorio.FullName.Substring(CaminhoPublish.Length);
            }
            else
            {
                diretorio = new DirectoryInfo(CaminhoPublish);
                caminho = string.Empty;
            }

            //using (new NetworkConnection(CaminhoPublish, new NetworkCredential(_userName, _password)))
            //{

            //    foreach (FileSystemInfo dir in diretorio.EnumerateDirectories())
            //    {
            //        publish.Add(new ReleaseModels { Nome = dir.Name, DataCriacao = dir.CreationTime, Caminho = HttpUtility.JavaScriptStringEncode(dir.FullName), Tipo = dir.Attributes.ToString() });
            //    }
            //}

            var diretorios = servicoDePublicacao.ObterPastas(diretorio.FullName);

            foreach (string dir in diretorios)
            {
                publish.Add(new ReleaseModels { Nome = dir });
            }

            return this.Json(new { Resultado = publish, Caminho = HttpUtility.JavaScriptStringEncode(caminho) }, JsonRequestBehavior.AllowGet);
        }
    }
}
