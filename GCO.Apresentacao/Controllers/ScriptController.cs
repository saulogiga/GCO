using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using GCO.Dominio.Entidade;
using GCO.Processo;
using GCO.Dominio.Fabrica;
using System.Text;
using System.IO;

namespace GCO.Apresentacao.Controllers
{
    public class ScriptController : Controller
    {
        private void PreencherCombosCadastro()
        {
            var processoDeTipoDeScriptDeBanco = new ProcessoDeTipoDeScriptDeBanco();


            ViewBag.TipoDeScriptDeBanco = new SelectList(processoDeTipoDeScriptDeBanco.Listar().OrderBy(t => t.Nome), "IdTipoScript", "Nome");
        }

        [HttpGet]
        public JsonResult ExecutarScript(int IdUnidadeDesenvolvimento)
        {
            var mensagemErro = string.Empty;
            var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidadeDeDesenvolvimento = new UnidadeDeDesenvolvimento();
            unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento = IdUnidadeDesenvolvimento;
            unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento);

            if (unidadeDeDesenvolvimento.ExecutantoScript)
            {
                return null;
            }else if (processoDeScriptDeBanco.ExecutarScript(unidadeDeDesenvolvimento, out mensagemErro) == false)
            {
                throw new Exception(mensagemErro);
            }
            
            
            //RedirectToAction("Listar", new { idUnidadeDesenvolvimento = IdUnidadeDesenvolvimento });
            return this.Json(new { redirectUrl = Url.Action("Listar", new { idUnidadeDesenvolvimento = IdUnidadeDesenvolvimento }), isRedirect = true }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public JsonResult AtualizarScript(int IdUnidadeDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidade = new UnidadeDeDesenvolvimento{IdUnidadeDesenvolvimento = IdUnidadeDesenvolvimento};

            unidade = processoDeUnidadeDeDesenvolvimento.Obter(unidade);

            var scriptsExecutados = unidade.Scripts.Where(s => s.Executado == true && s.IsDeleted == false).Select(s => new ScriptDeBanco { IdScript = s.IdScript});

            var scriptsNaoExecutados = unidade.Scripts.Where(s => s.Executado == false && s.IsDeleted == false);

            
            int idScriptExecutando = 0;
            if ((scriptsNaoExecutados != null) && (scriptsNaoExecutados.Count() > 0))
            {
                idScriptExecutando = Convert.ToInt32(scriptsNaoExecutados.OrderBy(s => s.Ordem).FirstOrDefault<ScriptDeBanco>().IdScript);
            }
            return base.Json(new { Resultado = scriptsExecutados, IdScriptExecutato = idScriptExecutando }, JsonRequestBehavior.AllowGet);
        }


        #region Listar
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        public ActionResult Listar(int idUnidadeDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();

            var unidadeDeDesenvolvimento = new UnidadeDeDesenvolvimento();
            unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento;
            unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento);

            return View(unidadeDeDesenvolvimento);
        }

        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Listar(UnidadeDeDesenvolvimento unidade)
        {
            
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(unidade);

            var nomeArquivo = "Script_" + unidadeDeDesenvolvimento.NumeroTicket +".sql";
            string arq = nomeArquivo;
            string extensao = Path.GetExtension(arq);
            if (extensao.EndsWith(".sql"))
            {

                var arquivo = new StringBuilder();

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);
                Response.Charset = "";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                foreach (ScriptDeBanco script in unidadeDeDesenvolvimento.Scripts.Where(s => s.IsDeleted == false))
                {
                    arquivo.AppendLine(string.Empty);
                    arquivo.AppendLine("GO");
                    arquivo.AppendLine("PRINT '" + script.Nome + "'");
                    arquivo.AppendLine("GO");
                    arquivo.AppendLine(script.Script);
                    arquivo.AppendLine(string.Empty);
                }



                //byte[] byteArray = Encoding.ASCII.GetBytes(arquivo.ToString());
                //MemoryStream ms = new MemoryStream(byteArray);
                //StreamReader sr = new StreamReader(ms);
                StringWriter sr = new StringWriter(arquivo);

                Response.Write(sr.ToString());
                Response.End();

                return View(unidadeDeDesenvolvimento);
            }
            else
            {
                throw new Exception("extensão de arquivo inválida!");
            }
        }

        #endregion

        #region Editar
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [ValidateInput(false)]
        public ActionResult Editar(Int64 idScript)
        {
            var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();

            PreencherCombosCadastro();

            var scriptDeBanco = processoDeScriptDeBanco.Obter(new ScriptDeBanco { IdScript = idScript });
            ProcessoDeUnidadeDeDesenvolvimento processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidade = new UnidadeDeDesenvolvimento{IdUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento};

            if (processoDeUnidadeDeDesenvolvimento.Obter(unidade).ExecutantoScript)
            {
                ViewBag.MensagemErro = "Não é possível incluir nenhum script, outro usuário está executando os scripts desta unidade de desenvolvimento. Tente novamente mais tarde.";
            }

            if (scriptDeBanco.Executado)
            {
                return RedirectToAction("Detalhe", new { idScript = idScript });
            }

            ViewBag.idUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento;

            return View(scriptDeBanco);
        }

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Editar(ScriptDeBanco scriptDeBanco)
        {
            var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();

            PreencherCombosCadastro();

            var _scriptDeBanco = processoDeScriptDeBanco.Obter(scriptDeBanco);

            ViewBag.NomeUsuario = User.Identity.Name;
            ViewBag.idUnidadeDesenvolvimento = _scriptDeBanco.IdUnidadeDesenvolvimento;

            ProcessoDeUnidadeDeDesenvolvimento processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidade = new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = _scriptDeBanco.IdUnidadeDesenvolvimento };
            unidade = processoDeUnidadeDeDesenvolvimento.Obter(unidade);

            if (ModelState.IsValid && !unidade.ExecutantoScript)
            {
                _scriptDeBanco.Comentario = scriptDeBanco.Comentario;
                _scriptDeBanco.IdTipoScript = scriptDeBanco.IdTipoScript;
                _scriptDeBanco.Script = scriptDeBanco.Script;
                _scriptDeBanco.UserId = WebSecurity.GetUserId(User.Identity.Name);

                try
                {
                    processoDeScriptDeBanco.Atualizar(_scriptDeBanco);
                }
                catch (Exception exc)
                {
                    ViewBag.MensagemErro = "ERRO: " + exc.Message;
                    return View(scriptDeBanco);
                }
            }

            return RedirectToAction("Listar", new { idUnidadeDesenvolvimento = _scriptDeBanco.IdUnidadeDesenvolvimento });
        }

        #endregion

        #region Detalhe
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        public ActionResult Detalhe(Int64 idScript)
        {
            var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();

            var scriptDeBanco = processoDeScriptDeBanco.Obter(new ScriptDeBanco { IdScript = idScript });

            ViewBag.idUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento;

            return View(scriptDeBanco);
        }

        #endregion

        #region Novo
        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpGet]
        public ActionResult Novo(int idUnidadeDesenvolvimento)
        {
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });

            PreencherCombosCadastro();
            
            ViewBag.NomeUsuario = User.Identity.Name;
            ViewBag.idUnidadeDesenvolvimento = idUnidadeDesenvolvimento;


            if (processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento).ExecutantoScript)
            {
                ViewBag.MensagemErro = "Não é possível incluir nenhum script, outro usuário está executando os scripts desta unidade de desenvolvimento. Tente novamente mais tarde.";
            }


            return View(new ScriptDeBanco { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento, UserId = WebSecurity.GetUserId(User.Identity.Name), Nome = "Script_TFS_" + unidadeDeDesenvolvimento.NumeroTicket.ToString().PadLeft(4, '0') + "__" + (unidadeDeDesenvolvimento.Scripts.Count + 1).ToString().PadLeft(3, '0') });
        }

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Novo(ScriptDeBanco scriptDeBanco)
        {
            PreencherCombosCadastro();
            ViewBag.NomeUsuario = User.Identity.Name;

            ProcessoDeUnidadeDeDesenvolvimento processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidade = new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento };
            unidade = processoDeUnidadeDeDesenvolvimento.Obter(unidade);

            if (ModelState.IsValid && !unidade.ExecutantoScript)
            {
                ViewBag.idUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento;

                var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();

                try
                {
                    processoDeScriptDeBanco.Criar(FabricaDeScriptDeBanco.Criar(scriptDeBanco.Nome, scriptDeBanco.Comentario, scriptDeBanco.Script, false, scriptDeBanco.IdUnidadeDesenvolvimento, scriptDeBanco.IdTipoScript, WebSecurity.GetUserId(User.Identity.Name)));
                }
                catch (Exception exc)
                {
                    ViewBag.MensagemErro = "ERRO: " + exc.Message;
                    return View(scriptDeBanco);
                }
            
            }

            return RedirectToAction("Novo", new { idUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento });
        }

        

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpGet]
        public ActionResult Importar(int idUnidadeDesenvolvimento)
        {
            
            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });

            PreencherCombosCadastro();

            ViewBag.NomeUsuario = User.Identity.Name;
            ViewBag.idUnidadeDesenvolvimento = idUnidadeDesenvolvimento;

            if (processoDeUnidadeDeDesenvolvimento.Obter(unidadeDeDesenvolvimento).ExecutantoScript)
            {
                ViewBag.MensagemErro = "Não é possível incluir nenhum script, outro usuário está executando os scripts desta unidade de desenvolvimento. Tente novamente mais tarde.";
            }

            ModelState.Remove("Script");

            return View(new ScriptDeBanco { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento, UserId = WebSecurity.GetUserId(User.Identity.Name), Nome = "Script_TFS_" + unidadeDeDesenvolvimento.NumeroTicket.ToString().PadLeft(4, '0') + "__" + (unidadeDeDesenvolvimento.Scripts.Count + 1).ToString().PadLeft(3, '0') });
        }

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Importar(ScriptDeBanco scriptDeBanco, HttpPostedFileBase Arquivo)
        {            
            StreamReader reader = new StreamReader(Arquivo.InputStream,Encoding.GetEncoding(1252));
            scriptDeBanco.Script = reader.ReadToEnd();

            reader.Close();
            reader.Dispose();

            ModelState.Remove("Script");

            PreencherCombosCadastro();
            ViewBag.NomeUsuario = User.Identity.Name;

            ProcessoDeUnidadeDeDesenvolvimento processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidade = new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento };
            unidade = processoDeUnidadeDeDesenvolvimento.Obter(unidade);

            if (ModelState.IsValid && !unidade.ExecutantoScript)
            {
                ViewBag.idUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento;

                var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();

                try
                {
                    processoDeScriptDeBanco.Criar(FabricaDeScriptDeBanco.Criar(scriptDeBanco.Nome, scriptDeBanco.Comentario, scriptDeBanco.Script, false, scriptDeBanco.IdUnidadeDesenvolvimento, scriptDeBanco.IdTipoScript, WebSecurity.GetUserId(User.Identity.Name)));
                }
                catch (Exception exc)
                {
                    ViewBag.MensagemErro = "ERRO: " + exc.Message;
                    return View(scriptDeBanco);
                }

            }

            return RedirectToAction("Importar", new { idUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento });
        }

        #endregion

        #region Apagar
        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        public ActionResult Apagar(Int64 idScript)
        {
            var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();

            var scriptDeBanco = processoDeScriptDeBanco.Obter(new ScriptDeBanco { IdScript = idScript });

            ProcessoDeUnidadeDeDesenvolvimento processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var unidade = new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento };
            unidade = processoDeUnidadeDeDesenvolvimento.Obter(unidade);

            
            if (scriptDeBanco.Executado || unidade.ExecutantoScript)
            {
                return RedirectToAction("Detalhe", new { idScript = idScript });
            }

            ViewBag.idUnidadeDesenvolvimento = scriptDeBanco.IdUnidadeDesenvolvimento;

            return View(scriptDeBanco);
        }

        [ValidateInput(false)]
        [Authorize(Roles = "GCO,Desenvolvedor,AnalistaDeSistema,AnalistaDeProjeto")]
        [HttpPost]
        public ActionResult Apagar(ScriptDeBanco scriptDeBanco)
        {
            var processoDeScriptDeBanco = new ProcessoDeScriptDeBanco();

            var _scriptDeBanco = processoDeScriptDeBanco.Obter(scriptDeBanco);

            ViewBag.NomeUsuario = User.Identity.Name;
            ViewBag.idUnidadeDesenvolvimento = _scriptDeBanco.IdUnidadeDesenvolvimento;


            try
            {
                _scriptDeBanco.UserId = WebSecurity.GetUserId(User.Identity.Name);
                processoDeScriptDeBanco.Apagar(_scriptDeBanco);
            }
            catch (Exception exc)
            {
                ViewBag.MensagemErro = "ERRO: " + exc.Message;
                return View(scriptDeBanco);
            }
            

            return RedirectToAction("Listar", new { idUnidadeDesenvolvimento = _scriptDeBanco.IdUnidadeDesenvolvimento });
        }

        #endregion
    }
}