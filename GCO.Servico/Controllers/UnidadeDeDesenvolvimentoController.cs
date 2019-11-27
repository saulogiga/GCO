using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using GCO.Processo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCO.Servico.Controllers
{
    public class UnidadeDeDesenvolvimentoController : Controller
    {
        //
        // GET: /UnidadeDeDesenvolvimentoDe/

        public ActionResult Index()
        {
            return View();
        }

        
        public int Criar(int idTipo, int idProjeto, string numeroTicket, string solicitacao, string solicitante, string desenvolvedores, string produtos)
        {

            var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            var processoDeUsuario = new ProcessoDeUsuario();
            var processoDeProjeto = new ProcessoDeProjeto();

            var _projeto = processoDeProjeto.Obter(new Projeto { IdProjeto = idProjeto });
            var _solicitante = processoDeUsuario.Listar().FirstOrDefault(u => u.UserName == solicitante);
            Usuario _desenvolvedor = null;
            List<Usuario> _desenvolvedores = null;
            

            if (idTipo != 1 && idTipo != 2)
                throw new Exception("Parâmetro idTipo inválido.");

            if (_solicitante == null)
                throw new Exception("Parâmetro solicitante inválido.");

            if (_projeto == null)
                throw new Exception("Parâmetro idProjeto inválido.");

            if (numeroTicket == string.Empty || numeroTicket == null)
                throw new Exception("Parâmetro numeroTicket é obrigatório.");

            foreach (string usuario in desenvolvedores.Split(';'))
            {
                _desenvolvedor = processoDeUsuario.Listar().FirstOrDefault(u => u.UserName == usuario);
                if (_desenvolvedor != null)
                {
                    if (_desenvolvedores == null)
                        _desenvolvedores = new List<Usuario>();

                    _desenvolvedores.Add(_desenvolvedor);
                }
            }

            var unidade = processoDeUnidadeDeDesenvolvimento.Criar(FabricaDeUnidadeDeDesenvolvimento.Criar(numeroTicket, solicitacao, idTipo, idProjeto, _solicitante.UserId, 1, _desenvolvedores, string.Empty, null, null, produtos));

            return unidade.IdUnidadeDesenvolvimento;
        }

        public bool Atualizar(int idUnidadeDesenvolvimento, string solicitacao)
        {
            try
            {
                var processoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
                var unidadeDeDesenvolvimento = processoDeUnidadeDeDesenvolvimento.Obter(new UnidadeDeDesenvolvimento { IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento });


                if (unidadeDeDesenvolvimento == null)
                    throw new Exception("Unidade de Desenvolvimento inválida.");

                unidadeDeDesenvolvimento.NumeroSolicitacao = solicitacao;
                processoDeUnidadeDeDesenvolvimento.Atualizar(unidadeDeDesenvolvimento);

                return true;
            }
            catch { return false; }

        }

    }
}
