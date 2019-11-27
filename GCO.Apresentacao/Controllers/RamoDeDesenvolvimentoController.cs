using GCO.Dominio.Entidade;
using GCO.Processo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCO.Apresentacao.Controllers
{
    public class RamoDeDesenvolvimentoController : Controller
    {
        //
        // GET: /RamoDeDesenvolvimento/

        public ActionResult Index()
        {
            return View();
        }

        private void PreencherCombos(int idProjeto)
        {
            var processoDeRepositorio = new ProcessoDeRepositorio();
            var processoRamoDeProducao = new ProcessoDeRamoDeProducao();
            var processoDeModulo = new ProcessoDeModulo();

            ViewBag.RamoDeProducao = new SelectList(processoRamoDeProducao.Listar().Where(r => r.IdProjeto == idProjeto).OrderBy(r => r.Modulo.Nome), "IdRamoProducao", "Ramo");
            ViewBag.Modulo = new SelectList(processoDeModulo.Listar().OrderBy(p => p.Nome), "IdModulo", "Nome");
            ViewBag.Repositorio = new SelectList(processoDeRepositorio.Listar().OrderBy(p => p.Nome), "IdRepositorio", "Nome");
        }

        //private string GerarCaminho(int idProjeto, int idRepositorio, bool? branch)
        //{

        //    var processoDeRepositorio = new ProcessoDeRepositorio();
        //    var processoDeProjeto = new ProcessoDeProjeto();

        //    var repositorio = processoDeRepositorio.Obter(idRepositorio);
        //    var projeto = processoDeProjeto.Obter(new Projeto { IdProjeto = idProjeto });

        //    if (branch != null && branch == false)
        //    {
        //        if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.TFS)
        //        {
        //            return "Team Project: " + projeto.Cliente.TeamProject.Trim() + "  Caminho: " + repositorio.Caminho.Trim() + "/Main";
        //        }
        //        else if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.SVN)
        //        {
        //            return repositorio.Caminho.Trim() + "/Tag/";
        //        }
        //    }
        //    else
        //    {
        //        if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.TFS)
        //        {
        //            return "Team Project: " + projeto.Cliente.TeamProject.Trim() + "  Caminho: " + repositorio.Caminho.Trim() + "/Branches/";
        //        }
        //        else if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.SVN)
        //        {
        //            return repositorio.Caminho.Trim() + "/Branches/";
        //        }
        //    }
        //    return string.Empty;
        //}

        private void PreencherCombos()
        {
            var processoDeRepositorio = new ProcessoDeRepositorio();
            var processoDeProjeto = new ProcessoDeProjeto();
            var processoDeModulo = new ProcessoDeModulo();

            ViewBag.Projeto = new SelectList(processoDeProjeto.Listar().OrderBy(p => p.Nome), "IdProjeto", "Nome");
            ViewBag.Modulo = new SelectList(processoDeModulo.Listar().OrderBy(p => p.Nome), "IdModulo", "Nome");
            ViewBag.Repositorio = new SelectList(processoDeRepositorio.Listar().OrderBy(p => p.Nome), "IdRepositorio", "Nome");
        }

        public ActionResult Listar()
        {
            PreencherCombos();

            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();

            var ramos = processoDeRamoDeDesenvolvimento.Listar().Where(r => r.Fechado == false && r.UnidadeDeDesenvolvimento.IsDeleted == false && r.UnidadeDeDesenvolvimento.IdStatusUnidadeDesenvolvimento != (int)EnumDeStatusDeUnidadeDeDesenvolvimento.AguardandoPublicacaoEmProducao && r.UnidadeDeDesenvolvimento.IdStatusUnidadeDesenvolvimento != (int)EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado).OrderByDescending(r => r.UnidadeDeDesenvolvimento.NumeroTicket);

            return View(ramos);
        }

        [HttpGet]
        public ActionResult GetRamoDeDesenvolvimento(string versao, int Projeto, int Modulo, int Repositorio)
        {
            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();

            var ramos = processoDeRamoDeDesenvolvimento.Listar().Where(r => r.Fechado == false && r.UnidadeDeDesenvolvimento.IsDeleted == false && r.UnidadeDeDesenvolvimento.IdStatusUnidadeDesenvolvimento == (int)EnumDeStatusDeUnidadeDeDesenvolvimento.EmDesenvolvimento && r.UnidadeDeDesenvolvimento.IdStatusUnidadeDesenvolvimento != (int)EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado).OrderByDescending(r => r.UnidadeDeDesenvolvimento.NumeroTicket).ToList();

            #region Filtros
            if (versao != string.Empty)
            {
                ramos = ramos.Where(r => (r.Versao != null && r.Versao.Contains(versao))).ToList();
            }
            if (Projeto > 0)
            {
                ramos = ramos.Where(r => r.UnidadeDeDesenvolvimento.IdProjeto == Projeto).ToList();
            }
            if (Modulo > 0)
            {
                ramos = ramos.Where(r => r.IdModulo == Modulo).ToList();
            }
            if (Repositorio > 0)
            {
                ramos = ramos.Where(r => r.IdRepositorio == Repositorio).ToList();
            }
            #endregion

            var itens = ramos.Select(r => new RamoDeDesenvolvimento {                
                Caminho = r.Caminho,
                IdModulo = r.IdModulo,
                Modulo = r.Modulo,
                UnidadeDeDesenvolvimento = r.UnidadeDeDesenvolvimento,
                Versao = r.Versao,
                IdRamoDesenvolvimento = r.IdRamoDesenvolvimento
            });

            var Resultado = Newtonsoft.Json.JsonConvert.SerializeObject(itens, new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });  
            
            return Content( Resultado);
            //return this.Json(new { Resultado = itens }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRamoDeProducao(int idRamoDeProducao, int idUnidadeDesenvolvimento, bool? branch)
        {
            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            var ramo = processoDeRamoDeProducao.Obter(idRamoDeProducao);

            //ramo.Caminho = GerarCaminho(ramo, branch);

            return this.Json(new { Resultado = ramo }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Novo(int idUnidadeDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });

            PreencherCombos(unidade.Projeto.IdProjeto);

            return View(new RamoDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento, Branch = true });
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Novo(RamoDeDesenvolvimento ramo)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();
            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = ramo.IdUnidadeDesenvolvimento });

            PreencherCombos(unidade.Projeto.IdProjeto);

            if (processoDeRamoDeDesenvolvimento.Listar().Where(r => r.IdUnidadeDesenvolvimento == unidade.IdUnidadeDesenvolvimento && r.IdModulo == ramo.IdModulo).Count() > 0)
            {
                ModelState.AddModelError("IdModulo", "Módulo já cadastrado para esta unidade de desenvolvimento.");
                return View(ramo);
            }

            if (ModelState.IsValid && ((ramo.Branch && ramo.Caminho != null) || (!ramo.Branch && ramo.Versao != null)))
            {
                //O caminho está sendo gerado no processo
                //if (ramo.Branch) { ramo.Versao = string.Empty; } else { ramo.Caminho = string.Empty; }
                //ramo.Caminho = GerarCaminho(unidade.IdProjeto,ramo.IdRepositorio, ramo.Branch) + ramo.Caminho;                

                processoDeRamoDeDesenvolvimento.Criar(ramo);

                return RedirectToAction("Novo", new { IdUnidadeDesenvolvimento = ramo.IdUnidadeDesenvolvimento });
            }
            else
            {
                if (ramo.Branch && ramo.Caminho == null)
                    ModelState.AddModelError("caminho", "O campo Branch é obrigatório");

                if (!ramo.Branch && ramo.Versao == null)
                    ModelState.AddModelError("Versao", "O campo Versão é obrigatório");

                return View(ramo);
            }
        }


        [Authorize(Roles = "GCO")]
        public ActionResult Editar(int idRamoDesenvolvimento)
        {
            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

            var ramo = processoDeRamoDeDesenvolvimento.Obter(idRamoDesenvolvimento);

            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(ramo.UnidadeDeDesenvolvimento);

            if (ramo.Branch)
            {
                ramo.Versao = string.Empty;
                //ramo.Caminho = ramo.Caminho.Substring(processoDeRamoDeDesenvolvimento.GerarCaminho(ramo.IdRepositorio, unidade.IdProjeto, ramo.Branch).Length + 1);
            }
            else
            {
                ramo.Caminho = string.Empty;
            }

            PreencherCombos(unidade.Projeto.IdProjeto);

            return View(ramo);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Editar(RamoDeDesenvolvimento ramo)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();
            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = ramo.IdUnidadeDesenvolvimento });

            PreencherCombos(unidade.Projeto.IdProjeto);

            if (processoDeRamoDeDesenvolvimento.Listar().Where(r => r.IdUnidadeDesenvolvimento == unidade.IdUnidadeDesenvolvimento && r.IdModulo == ramo.IdModulo && r.IdRamoDesenvolvimento != ramo.IdRamoDesenvolvimento).Count() > 0)
            {
                ModelState.AddModelError("IdModulo", "Módulo já cadastrado para esta unidade de desenvolvimento.");
                return View(ramo);
            }

            if (ModelState.IsValid && ((ramo.Branch && ramo.Caminho != null) || (!ramo.Branch && ramo.Versao != null)))
            {
                if (ramo.Branch)
                {
                    ramo.Versao = string.Empty;
                    //ramo.Caminho = processoDeRamoDeDesenvolvimento.GerarCaminho(ramo.IdRepositorio, unidade.IdProjeto, ramo.Branch) + ramo.Caminho;
                }
                else { ramo.Caminho = string.Empty; }

                processoDeRamoDeDesenvolvimento.Atualizar(ramo);

                return RedirectToAction("Editar", "UnidadeDeDesenvolvimento", new { IdUnidadeDesenvolvimento = ramo.IdUnidadeDesenvolvimento });
            }
            else
            {
                if (ramo.Branch && ramo.Caminho == null)
                    ModelState.AddModelError("caminho", "O campo Branch é obrigatório");

                if (!ramo.Branch && ramo.Versao == null)
                    ModelState.AddModelError("Versao", "O campo Versão é obrigatório");

                return View(ramo);
            }
        }

        public ActionResult Detalhe(int idRamoDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();

            var ramo = processoDeRamoDeDesenvolvimento.Obter(idRamoDesenvolvimento);
            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = ramo.IdUnidadeDesenvolvimento });

            //ramo.Caminho = ramo.Caminho.Substring(processoDeRamoDeDesenvolvimento.GerarCaminho(ramo.IdRepositorio, unidade.IdProjeto, ramo.Branch).Length + 1);

            return View(ramo);
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Apagar(int idRamoDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();

            var ramo = processoDeRamoDeDesenvolvimento.Obter(idRamoDesenvolvimento);
            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = ramo.IdUnidadeDesenvolvimento });

            //ramo.Caminho = ramo.Caminho.Substring(processoDeRamoDeDesenvolvimento.GerarCaminho(ramo.IdRepositorio, unidade.IdProjeto, ramo.Branch).Length + 1);

            return View(ramo);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Apagar(RamoDeDesenvolvimento ramo)
        {
            var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();
            ramo = processoDeRamoDeDesenvolvimento.Obter(ramo.IdRamoDesenvolvimento);

            processoDeRamoDeDesenvolvimento.Apagar(ramo);

            return RedirectToAction("Editar", "UnidadeDeDesenvolvimento", new { idUnidadeDesenvolvimento = ramo.IdUnidadeDesenvolvimento });

        }

        //public ActionResult Detalhes(int idRamoDesenvolvimento)
        //{
        //    var processoDeRamoDeDesenvolvimento = new ProcessoDeRamoDeDesenvolvimento();

        //    var ramo = processoDeRamoDeDesenvolvimento.Obter(idRamoDesenvolvimento);

        //    return View(ramo);
        //}

    }
}
