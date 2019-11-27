using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using GCO.Processo;
using WebMatrix.WebData;

namespace GCO.Apresentacao.Controllers
{
    public class WebConfigController : Controller
    {

        public void PreencherCombos(int idUnidadeDesenvolvimento)
        {
            var processoDeTipoDeWebConfig = new ProcessoDeTipoDeWebConfig();
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao();

            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });
            var modulos = processoDeRamoDeProducao.Listar().Where(m => m.IdProjeto == unidade.IdProjeto).OrderBy(m => m.Modulo.Nome);

            ViewBag.Modulo = new SelectList(modulos, "IdModulo", "Modulo.Nome");
            ViewBag.TipoDeWebConfig = new SelectList(processoDeTipoDeWebConfig.Listar().OrderBy(t => t.Nome), "IdTipoWebConfig", "Nome");
        }

        #region Listar
        //
        // GET: /WebConfig/

        public ActionResult Listar(int idUnidadeDesenvolvimento)
        {

            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

            var unidadeDeDesenvolvimento = new UnidadeDeDesenvolvimento();
            unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento;
            unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento);

            return View(unidadeDeDesenvolvimento);
        }

        #endregion

        #region Novo

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpGet]
        public ActionResult Novo(int idUnidadeDesenvolvimento)
        {

            PreencherCombos(idUnidadeDesenvolvimento);

            ViewBag.idUnidadeDesenvolvimento = idUnidadeDesenvolvimento;

            return View(new WebConfig());
        }

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Novo(WebConfig webConfig)
        {
            PreencherCombos(webConfig.IdUnidadeDesenvolvimento);

            if (ModelState.IsValid)
            {
                ViewBag.idUnidadeDesenvolvimento = webConfig.IdUnidadeDesenvolvimento;

                var processoDeWebConfig = new ProcessoDeWebConfig();
                processoDeWebConfig.Criar(FabricaDeWebConfig.Criar(webConfig.Valor, webConfig.IdTipoWebConfig, webConfig.IdModulo.Value,webConfig.Acao.Value, webConfig.IdUnidadeDesenvolvimento, WebSecurity.GetUserId(User.Identity.Name)));

                return RedirectToAction("Novo", new { idUnidadeDesenvolvimento = webConfig.IdUnidadeDesenvolvimento });
            }
            else
                return View(webConfig);
        }
        #endregion

        #region Editar
        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        public ActionResult Editar(int idWebConfig)
        {
            var processoDeWebConfig = new ProcessoDeWebConfig();

            var webConfig = processoDeWebConfig.Obter(new WebConfig { IdWebConfig = idWebConfig });

            PreencherCombos(webConfig.IdUnidadeDesenvolvimento);

            ViewBag.idUnidadeDesenvolvimento = webConfig.IdUnidadeDesenvolvimento;
            
            return View(webConfig);
        }

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Editar(WebConfig webConfig)
        {
            var processoDeWebConfig = new ProcessoDeWebConfig();

            PreencherCombos(webConfig.IdUnidadeDesenvolvimento);

            var _webConfig = processoDeWebConfig.Obter(webConfig);

            ViewBag.idUnidadeDesenvolvimento = _webConfig.IdUnidadeDesenvolvimento;

            if (ModelState.IsValid)
            {
                _webConfig.IdTipoWebConfig = webConfig.IdTipoWebConfig;
                _webConfig.Valor = webConfig.Valor;
                _webConfig.Acao = webConfig.Acao;
                _webConfig.IdModulo = webConfig.IdModulo;

                processoDeWebConfig.Atualizar(_webConfig);

            }

            return RedirectToAction("Listar", new { idUnidadeDesenvolvimento = _webConfig.IdUnidadeDesenvolvimento });
        }

        #endregion

        #region Detalhe
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        public ActionResult Detalhe(int idWebConfig)
        {
            var processoDeWebConfig = new ProcessoDeWebConfig();

            var webConfig = processoDeWebConfig.Obter(new WebConfig { IdWebConfig = idWebConfig });

            ViewBag.idUnidadeDesenvolvimento = webConfig.IdUnidadeDesenvolvimento;

            return View(webConfig);
        }

        #endregion

        #region Apagar
        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        public ActionResult Apagar(int idWebConfig)
        {
            var processoDeWebConfig = new ProcessoDeWebConfig();

            var WebConfig = processoDeWebConfig.Obter(new WebConfig { IdWebConfig = idWebConfig });

            ViewBag.idUnidadeDesenvolvimento = WebConfig.IdUnidadeDesenvolvimento;

            return View(WebConfig);
        }

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Apagar(WebConfig WebConfig)
        {
            var processoDeWebConfig = new ProcessoDeWebConfig();

            var _WebConfig = processoDeWebConfig.Obter(WebConfig);

            ViewBag.idUnidadeDesenvolvimento = _WebConfig.IdUnidadeDesenvolvimento;

            try
            {
                processoDeWebConfig.Apagar(_WebConfig);
            }
            catch (Exception exc)
            {
                ViewBag.MensagemErro = "ERRO: " + exc.Message;
                return View(WebConfig);
            }


            return RedirectToAction("Listar", new { idUnidadeDesenvolvimento = _WebConfig.IdUnidadeDesenvolvimento });
        }

        #endregion

    }
}
