using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using GCO.Processo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCO.Apresentacao.Controllers
{
    public class RepositorioController : Controller
    {
        //
        // GET: /Repositorio/

        public ActionResult Index()
        {
            return View();
        }

        private void PreencherCombos()
        {
            var processoDeTipoDeRepositorio = new ProcessoDeTipoDeRepositorio();

            ViewBag.Repositorio = new SelectList(processoDeTipoDeRepositorio.Listar(), "IdTipoRepositorio", "Nome");
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Listar()
        {
            var processo = new ProcessoDeRepositorio();

            var repositorios = processo.Listar().OrderBy(r => r.Nome).ToList();

            return View(repositorios);
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Novo()
        {
            PreencherCombos();

            return View();
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Novo(Repositorio repositorio)
        {
            var processoDeRepositorio = new ProcessoDeRepositorio();
            var processoDeTipoDeRepositorio = new ProcessoDeTipoDeRepositorio();

            if (ModelState.IsValid)
            {
                var tipo = processoDeTipoDeRepositorio.Obter(repositorio.IdTipoRepositorio);
                processoDeRepositorio.Criar(FabricaDeRepositorio.Criar(repositorio.Nome,repositorio.Caminho, tipo));
            }

            return RedirectToAction("Novo");
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Editar(int idRepositorio)
        {
            PreencherCombos();

            var processo = new ProcessoDeRepositorio();

            var repositorio = processo.Obter(idRepositorio);

            return View(repositorio);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Editar(Repositorio repositorio)
        {
            var processo = new ProcessoDeRepositorio();

            if (ModelState.IsValid)
            {
                processo.Atualizar(repositorio);
            }

            return RedirectToAction("Listar");
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Apagar(int idRepositorio)
        {
            PreencherCombos();

            var processo = new ProcessoDeRepositorio();

            var repositorio = processo.Obter(idRepositorio);

            return View(repositorio);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Apagar(Repositorio repositorio)
        {
            var processo = new ProcessoDeRepositorio();

            processo.Apagar(repositorio);
            
            return RedirectToAction("Listar");
        }

    }
}
