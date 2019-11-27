using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCO.Dominio.Entidade;
using GCO.Processo;
using GCO.Dominio.Fabrica;

namespace GCO.Apresentacao.Controllers
{
    public class ProjetoController : Controller
    {
        //
        // GET: /Projeto/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ambientes()
        {
            var processoDeProjeto = new ProcessoDeProjeto();

           // return View(processoDeProjeto.Listar().Where(p => p.IdProjetoPai == 2 || p.IdProjetoPai == 37).OrderBy(p => p.Nome).ToList());
            return View(processoDeProjeto.Listar().OrderBy(p => p.Nome).ToList());

        }
    }
}
