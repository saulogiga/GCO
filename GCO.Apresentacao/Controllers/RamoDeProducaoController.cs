using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using GCO.Processo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GCO.Apresentacao.Controllers
{
    public class RamoDeProducaoController : Controller
    {
        //
        // GET: /RamoDeProducao/

        public ActionResult Index()
        {
            return View();
        }

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

            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            var ramos = processoDeRamoDeProducao.Listar().OrderBy(r => r.Projeto.Nome).OrderBy(r => r.Modulo.Nome).ToList();

            return View(ramos);
        }

        [HttpGet]
        public JsonResult GetRamoDeProducao(string versao, int Projeto, int Modulo, int Repositorio)
        {
            PreencherCombos();
            
            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();
            var editarDados = false;

            var ramos = processoDeRamoDeProducao.Listar().OrderBy(r => r.Projeto.Nome).ToList();

            #region Filtros
            if (versao != string.Empty)
            {
                ramos = ramos.Where(r => (r.Versao != null && r.Versao.Contains(versao))).ToList();
            }
            if (Projeto > 0)
            {
                ramos = ramos.Where(r => r.IdProjeto == Projeto).OrderBy(r => r.Modulo.Nome).ToList();
            }
            if (Modulo > 0)
            {
                ramos = ramos.Where(r => r.IdModulo == Modulo).ToList();
            }
            if (Repositorio  > 0)
            {
                ramos = ramos.Where(r => r.IdRepositorio == Repositorio).ToList();
            }
            #endregion

            var itens = ramos.Select(r => new RamoDeProducao
            {
                IdRamoProducao = r.IdRamoProducao,
                Caminho = r.Caminho,
                DataPublicacao = r.DataPublicacao,
                IdModulo = r.IdModulo,
                Modulo = r.Modulo,
                IdProjeto = r.IdProjeto,
                Projeto = r.Projeto,
                Solicitacao = r.Solicitacao,
                Versao = r.Versao
            });

            if (User.IsInRole("GCO"))
                editarDados = true;

            return this.Json(new { Resultado = itens.OrderBy(r => r.Projeto.Nome), Editar = editarDados }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Novo()
        {
            PreencherCombos();

            return View();
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Novo(RamoDeProducao ramoDeProducao)
        {
            PreencherCombos();

            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            if (processoDeRamoDeProducao.Listar().Where(r => r.IdProjeto == ramoDeProducao.IdProjeto && r.IdModulo == ramoDeProducao.IdModulo).Count() > 0)
            {
                ModelState.AddModelError("IdModulo", "Módulo já cadastrado para este projeto.");

                return View(ramoDeProducao);
            }

            if (ModelState.IsValid)
            {
                processoDeRamoDeProducao.Criar(ramoDeProducao);

                return RedirectToAction("Novo");
            }
            else
            {
                return View(ramoDeProducao);
            }

            
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Editar(int idRamoProducao)
        {
            PreencherCombos();

            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            var ramo = processoDeRamoDeProducao.Obter(idRamoProducao);

            return View(ramo);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Editar(RamoDeProducao ramoDeProducao)
        {
            PreencherCombos();

            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            if (processoDeRamoDeProducao.Listar().Where(r => r.IdProjeto == ramoDeProducao.IdProjeto && r.IdModulo == ramoDeProducao.IdModulo && r.IdRamoProducao != ramoDeProducao.IdRamoProducao).Count() > 0)
            {
                ModelState.AddModelError("IdModulo", "Módulo já cadastrado para este projeto.");

                return View(ramoDeProducao);
            }

            if (ModelState.IsValid)
            {
               
                processoDeRamoDeProducao.Atualizar(ramoDeProducao);

                return RedirectToAction("Listar");
            }
            else
            {
                return View(ramoDeProducao);
            }


        }

        [Authorize(Roles = "GCO")]
        public ActionResult Apagar(int idRamoProducao)
        {

            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            var ramo = processoDeRamoDeProducao.Obter(idRamoProducao);

            return View(ramo);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Apagar(RamoDeProducao ramoDeProducao)
        {
            var processo = new ProcessoDeRamoDeProducao();

            processo.Apagar(ramoDeProducao);

            return RedirectToAction("Listar");
        }

        public ActionResult Detalhes(int idRamoProducao)
        {
            var ProcessoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            var ramo = ProcessoDeRamoDeProducao.Obter(idRamoProducao);

            return View(ramo);
        }

        [Authorize(Roles = "GCO")]
        public ActionResult EditarVersao(int idRamoProducao)
        {
            PreencherCombos();

            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            var ramo = processoDeRamoDeProducao.Obter(idRamoProducao);

            ramo.Versao = string.Empty;
            ramo.DataPublicacao = DateTime.Now;
            ramo.Comentario = string.Empty;
            ramo.Solicitacao = string.Empty;

            return View(ramo);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult EditarVersao(RamoDeProducao ramoDeProducao)
        {
            PreencherCombos();

            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();
                        
            if (ModelState.IsValid)
            {

                processoDeRamoDeProducao.AtualizarVersao(ramoDeProducao);

                return RedirectToAction("Listar");
            }
            else
            {
                return View(ramoDeProducao);
            }


        }

    }
}
