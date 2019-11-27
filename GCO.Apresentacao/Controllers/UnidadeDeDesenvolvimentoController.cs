using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GCO.Processo;
using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using WebMatrix.WebData;

namespace GCO.Apresentacao.Controllers
{

    public class UnidadeDeDesenvolvimentoController : Controller
    {

        private void PreencherCombos()
        {
            var processoDeUsuario = new ProcessoDeUsuario();
            var processoDeStatusDeUnidadeDeDesenvolvimento = new ProcessoDeStatusDeUnidadeDeDesenvolvimento();
            var processoDeTipoDeUnidadeDeDesenvolvimento = new ProcessoDeTipoDeUnidadeDeDesenvolvimento();
            var processoDeProjeto = new ProcessoDeProjeto();
            var processoDeServidorDeBanco = new ProcessoDeServidorDeBanco();


            ViewBag.Solicitante = new SelectList(processoDeUsuario.Listar().OrderBy(s => s.UserName), "UserId", "UserName");
            ViewBag.StatusDeUnidadeDeDesenvolvimento = new SelectList(processoDeStatusDeUnidadeDeDesenvolvimento.Listar().OrderBy(s => s.IdStatusUnidadeDesenvolvimento), "IdStatusUnidadeDesenvolvimento", "Nome");
            ViewBag.TipoDeUnidadeDeDesenvolvimento = new SelectList(processoDeTipoDeUnidadeDeDesenvolvimento.Listar().OrderBy(t => t.Nome), "IdTipoUnidadeDesenvolvimento", "Nome");
            ViewBag.Projeto = new SelectList(processoDeProjeto.Listar().OrderBy(p => p.Nome), "IdProjeto", "Nome");
            ViewBag.ServidorDeBanco = processoDeServidorDeBanco.Listar().OrderBy(s => s.NomeServidor);
            ViewBag.Desenvolvedores = processoDeUsuario.Listar().OrderBy(u => u.UserName);
        }

        private int? GetIdServidorBanco()
        {
            var obj = Request.Form.GetValues("ddlServidorBanco");

            if (obj != null && obj[0] != string.Empty && obj[0] != "0")
            {
                return Convert.ToInt32(obj[0]);
            }
            else
            {
                return null;
            }
        }
        private string GetNomeBanco()
        {
            var obj = Request.Form.GetValues("txtNomeBanco");

            if (obj != null && obj[0] != string.Empty)
            {
                return obj[0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        private ICollection<Usuario> GetUsuarios()
        {
            try
            {
                var processo = new ProcessoDeUsuario();
                var usuarios = processo.Listar();
                var usuariosSelecionados = new List<Usuario>();


                foreach (Usuario u in usuarios)
                {
                    var obj = Request.Form.GetValues("chkDesenvolvedor_" + u.UserId.ToString());
                    if (obj != null && obj[0] != string.Empty)
                    {
                        usuariosSelecionados.Add(u);
                    }
                }

                return usuariosSelecionados;
            }
            catch 
            {
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetUnidadeDeDesenvolvimento(string ticketSolicitacao, int Projeto, int Tipo, int Status)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Listar();
            var editarDados = false;

            #region Filtros
            if (ticketSolicitacao != string.Empty)
            {
                unidadeDeDesenvolvimento = unidadeDeDesenvolvimento.Where(u => (u.NumeroSolicitacao != null && u.NumeroSolicitacao.Contains(ticketSolicitacao)) || u.NumeroTicket != null && u.NumeroTicket.Contains(ticketSolicitacao));
            }
            if (Projeto > 0)
            {
                unidadeDeDesenvolvimento = unidadeDeDesenvolvimento.Where(u => u.IdProjeto == Projeto);
            }

            if (Tipo > 0)
            {
                unidadeDeDesenvolvimento = unidadeDeDesenvolvimento.Where(u => u.IdTipoUnidadeDesenvolvimento == Tipo);
            }

            if (Status > 0)
            {
                unidadeDeDesenvolvimento = unidadeDeDesenvolvimento.Where(u => u.IdStatusUnidadeDesenvolvimento == Status);
            }
            else
            {
                unidadeDeDesenvolvimento = unidadeDeDesenvolvimento.Where(u => u.IdStatusUnidadeDesenvolvimento != (int)EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado);
            }

            #endregion

            //Desenvolvedores só podem visualizar suas atividades
            if (User.IsInRole("Desenvolvedor"))
                unidadeDeDesenvolvimento = unidadeDeDesenvolvimento.Where(ud => ud.Desenvolvedores.Any(u => u.UserId == WebSecurity.GetUserId(User.Identity.Name))).OrderByDescending(u => u.IdUnidadeDesenvolvimento);


            var itens = unidadeDeDesenvolvimento.Select(u => new UnidadeDeDesenvolvimento
            {
                IdUnidadeDesenvolvimento = u.IdUnidadeDesenvolvimento,
                NumeroTicket = u.NumeroTicket,
                NumeroSolicitacao = u.NumeroSolicitacao,
                DataPublicacao = u.DataPublicacao,
                StatusUnidadeDesenvolvimento = u.StatusUnidadeDesenvolvimento,
                TipoDeUnidadeDeDesenvolvimento = u.TipoDeUnidadeDeDesenvolvimento,
                Projeto = u.Projeto,
                CaminhoBuild = u.CaminhoBuild,
                CaminhoPublish = u.CaminhoPublish
            });

            if (User.IsInRole("GCO"))
                editarDados = true;

            return this.Json(new { Resultado = itens.OrderByDescending(u => u.IdUnidadeDesenvolvimento), Editar = editarDados }, JsonRequestBehavior.AllowGet);
        }

        #region Listar

        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        public ActionResult Listar()
        {
            PreencherCombos();

            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

            //Desenvolvedores só podem visualizar suas atividades
            if (User.IsInRole("Desenvolvedor"))
            {
                var unidade = processoDeUnidadeDeDesenvolvimento.Listar().Where(ud => ud.Desenvolvedores.Any(u => u.UserId == WebSecurity.GetUserId(User.Identity.Name))).Where(ud => ud.IdStatusUnidadeDesenvolvimento != (int)EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado).OrderByDescending(u => u.IdUnidadeDesenvolvimento);
                return View(unidade);
            }
            else
            {
                return View(processoDeUnidadeDeDesenvolvimento.Listar().Where(ud => ud.IdStatusUnidadeDesenvolvimento != (int)EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado).OrderByDescending(u => u.IdUnidadeDesenvolvimento));
            }
        }

        #endregion

        #region Novo e Edição

        [Authorize(Roles = "GCO")]
        public ActionResult Novo()
        {
            PreencherCombos();

            return View();
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Novo(UnidadeDeDesenvolvimento unidade)
        {
            PreencherCombos();


            if (ModelState.IsValid)
            {
                var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

                unidade.Desenvolvedores = GetUsuarios();

                unidade.CaminhoBuild = System.Configuration.ConfigurationManager.AppSettings["CaminhoBuild"].ToString() + unidade.NomeBuild;

                if (unidade.UnidadeDeDesenvolvimentoDeBanco != null && unidade.UnidadeDeDesenvolvimentoDeBanco.Count() > 0)
                {
                    var bancos = new List<UnidadeDeDesenvolvimentoDeBanco>();
                    var processoDeServidorDeBanco = new ProcessoDeServidorDeBanco();

                    foreach (UnidadeDeDesenvolvimentoDeBanco banco in unidade.UnidadeDeDesenvolvimentoDeBanco)
                    {

                        if (processoDeServidorDeBanco.ValidarBanco(banco.IdServidorBanco, banco.NomeBanco) == false)
                        {
                            ViewBag.MensagemErro = "ERRO: Banco de dados " + banco.NomeBanco + " não é válido.";
                            return View(unidade);
                        }
                        else if (unidade.UnidadeDeDesenvolvimentoDeBanco.Where(b => b.NomeBanco == banco.NomeBanco).Count() > 1)
                        {
                            ViewBag.MensagemErro = "ERRO: Banco de dados duplicado.";
                            return View(unidade);
                        }

                        bancos.Add(new UnidadeDeDesenvolvimentoDeBanco { IdServidorBanco = banco.IdServidorBanco, NomeBanco = banco.NomeBanco, IdTipoServidor = EnumDeTipoDeServidor.HomologacaoDeBanco.GetHashCode() });
                    }
                    unidade = processoDeUnidadeDeDesenvolvimento.Criar(FabricaDeUnidadeDeDesenvolvimento.Criar(unidade.NumeroTicket, unidade.NumeroSolicitacao, unidade.IdTipoUnidadeDesenvolvimento, unidade.IdProjeto, unidade.IdSolicitante, unidade.IdStatusUnidadeDesenvolvimento, unidade.Desenvolvedores, bancos, unidade.TeamProject, unidade.CaminhoBuild, unidade.CaminhoPublish, unidade.NomeBuild));
                }
                else
                {
                    unidade = processoDeUnidadeDeDesenvolvimento.Criar(FabricaDeUnidadeDeDesenvolvimento.Criar(unidade.NumeroTicket, unidade.NumeroSolicitacao, unidade.IdTipoUnidadeDesenvolvimento, unidade.IdProjeto, unidade.IdSolicitante, unidade.IdStatusUnidadeDesenvolvimento, unidade.Desenvolvedores, unidade.TeamProject, unidade.CaminhoBuild, unidade.CaminhoPublish, unidade.NomeBuild));
                }

                return RedirectToAction("Novo", "RamoDeDesenvolvimento", new { idUnidadeDesenvolvimento = unidade.IdUnidadeDesenvolvimento });
            }
            else
            {
                return View(unidade);
            }
        }

        [Authorize(Roles = "GCO")]
        public ActionResult Editar(int idUnidadeDesenvolvimento)
        {
            PreencherCombos();

            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

            var unidadeDeDesenvolvimento = new UnidadeDeDesenvolvimento();
            unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento;
            unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento);

            return View(unidadeDeDesenvolvimento);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult Editar(UnidadeDeDesenvolvimento unidadeDeDesenvolvimento)
        {
            PreencherCombos();

            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var _unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento);

            if (ModelState.IsValid)
            {
                _unidadeDeDesenvolvimento.IdTipoUnidadeDesenvolvimento = unidadeDeDesenvolvimento.IdTipoUnidadeDesenvolvimento;
                _unidadeDeDesenvolvimento.IdProjeto = unidadeDeDesenvolvimento.IdProjeto;
                _unidadeDeDesenvolvimento.NumeroTicket = unidadeDeDesenvolvimento.NumeroTicket;
                _unidadeDeDesenvolvimento.TeamProject = unidadeDeDesenvolvimento.TeamProject;
                _unidadeDeDesenvolvimento.NumeroSolicitacao = unidadeDeDesenvolvimento.NumeroSolicitacao;
                _unidadeDeDesenvolvimento.DataPublicacao = unidadeDeDesenvolvimento.DataPublicacao;
                _unidadeDeDesenvolvimento.IdSolicitante = unidadeDeDesenvolvimento.IdSolicitante;
                _unidadeDeDesenvolvimento.IdStatusUnidadeDesenvolvimento = unidadeDeDesenvolvimento.IdStatusUnidadeDesenvolvimento;
                _unidadeDeDesenvolvimento.CaminhoBuild = System.Configuration.ConfigurationManager.AppSettings["CaminhoBuild"].ToString() + unidadeDeDesenvolvimento.NomeBuild;
                _unidadeDeDesenvolvimento.NomeBuild = unidadeDeDesenvolvimento.NomeBuild;
                _unidadeDeDesenvolvimento.CaminhoPublish = unidadeDeDesenvolvimento.CaminhoPublish;
                _unidadeDeDesenvolvimento.Desenvolvedores = GetUsuarios();

                if (_unidadeDeDesenvolvimento.Scripts != null && _unidadeDeDesenvolvimento.Scripts.Where(s => s.Executado == true).Count() == 0)
                {
                    _unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.Clear();
                    if (unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco != null && unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.Count() > 0)
                    {
                        var bancos = new List<UnidadeDeDesenvolvimentoDeBanco>();
                        var processoDeServidorDeBanco = new ProcessoDeServidorDeBanco();

                        foreach (UnidadeDeDesenvolvimentoDeBanco banco in unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco)
                        {

                            if (processoDeServidorDeBanco.ValidarBanco(banco.IdServidorBanco, banco.NomeBanco) == false)
                            {
                                _unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco = unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco;
                                ViewBag.MensagemErro = "ERRO: Banco de dados " + banco.NomeBanco + " não é válido.";
                                return View(_unidadeDeDesenvolvimento);
                            }
                            else if (unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.Where(b => b.NomeBanco == banco.NomeBanco).Count() > 1)
                            {
                                _unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco = unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco;
                                ViewBag.MensagemErro = "ERRO: Banco de dados duplicado.";
                                return View(_unidadeDeDesenvolvimento);
                            }

                            bancos.Add(new UnidadeDeDesenvolvimentoDeBanco { IdServidorBanco = banco.IdServidorBanco, NomeBanco = banco.NomeBanco, IdTipoServidor = EnumDeTipoDeServidor.HomologacaoDeBanco.GetHashCode() });
                            _unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco = bancos;
                        }
                    }
                }

                processoDeUnidadeDeDesenvolvimento.Atualizar(_unidadeDeDesenvolvimento);

               
            }

            if (_unidadeDeDesenvolvimento.RamoDeDesenvolvimento.Where(r => r.Fechado == false).Count() > 0 && _unidadeDeDesenvolvimento.IdStatusUnidadeDesenvolvimento == (int)EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado)
            {
                return RedirectToAction("FecharVersao", new { idUnidadeDesenvolvimento = _unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento });
            }
            else
            {
                return RedirectToAction("Listar");
            }
        }
        #endregion

        #region Detalhes
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpGet]
        public ActionResult Detalhes(int idUnidadeDesenvolvimento = 0)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidadeDeDesenvolvimento = new UnidadeDeDesenvolvimento();
            var processoDeUsuario = new ProcessoDeUsuario();
            var processoDeStatusDeUnidadeDeDesenvolvimento = new ProcessoDeStatusDeUnidadeDeDesenvolvimento();
            var processoDeTipoDeUnidadeDeDesenvolvimento = new ProcessoDeTipoDeUnidadeDeDesenvolvimento();
            var processoDeProjeto = new ProcessoDeProjeto();
            var processoDeServidorDeBanco = new ProcessoDeServidorDeBanco();


            ViewBag.Solicitante = new SelectList(processoDeUsuario.Listar().OrderBy(s => s.UserName), "UserId", "UserName");
            ViewBag.StatusDeUnidadeDeDesenvolvimento = new SelectList(processoDeStatusDeUnidadeDeDesenvolvimento.Listar().OrderBy(s => s.IdStatusUnidadeDesenvolvimento), "IdStatusUnidadeDesenvolvimento", "Nome");
            ViewBag.TipoDeUnidadeDeDesenvolvimento = new SelectList(processoDeTipoDeUnidadeDeDesenvolvimento.Listar().OrderBy(t => t.Nome), "IdTipoUnidadeDesenvolvimento", "Nome");
            ViewBag.Projeto = new SelectList(processoDeProjeto.Listar().OrderBy(p => p.Nome), "IdProjeto", "Nome");
            ViewBag.ServidorDeBanco = processoDeServidorDeBanco.Listar().OrderBy(s => s.NomeServidor);
            ViewBag.Desenvolvedores = processoDeUsuario.Listar().OrderBy(u => u.UserName);

            unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento;
            unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento);

            //if (unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.Count() > 0)
            //{
            //    ViewBag.NomeBanco = unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.First().NomeBanco;
            //    ViewBag.ServidorBanco = unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.First().ServidorDeBanco.NomeServidor;
            //}
            //else
            //{
            //    ViewBag.NomeBanco = string.Empty;
            //    ViewBag.ServidorBanco = string.Empty;
            //}

            return View(unidadeDeDesenvolvimento);
        }
        #endregion

        #region Apagar
        [Authorize(Roles = "GCO")]
        public ActionResult Apagar(int idUnidadeDesenvolvimento)
        {
            PreencherCombos();

            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

            var unidadeDeDesenvolvimento = new UnidadeDeDesenvolvimento();
            unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento;
            unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento);

            if (unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.Count() > 0)
            {
                ViewBag.NomeBanco = unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.First().NomeBanco;
                ViewBag.ServidorBanco = unidadeDeDesenvolvimento.UnidadeDeDesenvolvimentoDeBanco.First().ServidorDeBanco.NomeServidor;
            }
            else
            {
                ViewBag.NomeBanco = string.Empty;
                ViewBag.ServidorBanco = string.Empty;
            }

            return View(unidadeDeDesenvolvimento);
        }

        [HttpPost]
        [Authorize(Roles = "GCO")]
        [ValidateInput(false)]
        public ActionResult Apagar(UnidadeDeDesenvolvimento unidadeDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            processoDeUnidadeDeDesenvolvimento.Deletar(unidadeDesenvolvimento);

            return RedirectToAction("Listar");
        }
        #endregion

        [Authorize(Roles = "GCO")]
        public ActionResult FecharVersao(int idUnidadeDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

            var unidade = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });

            unidade.RamoDeDesenvolvimento = unidade.RamoDeDesenvolvimento.Where(r => r.Fechado == false && r.IsDeleted == false).ToList();

            return View(unidade);
        }

        [Authorize(Roles = "GCO")]
        [HttpPost]
        public ActionResult FecharVersao(UnidadeDeDesenvolvimento unidade)
        {
            var processo = new ProcessoDeRamoDeDesenvolvimento();

            foreach (RamoDeDesenvolvimento r in unidade.RamoDeDesenvolvimento)
            {
                processo.FecharRamoDeDesenvolvimento(r.IdRamoDesenvolvimento, r.Versao);
            }

            return RedirectToAction("Listar");
        }
    }
}
