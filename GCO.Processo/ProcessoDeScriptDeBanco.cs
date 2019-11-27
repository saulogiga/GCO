using GCO.Dominio.Entidade;
using GCO.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GCO.Processo
{
    public class ProcessoDeScriptDeBanco
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void Criar(ScriptDeBanco script)
        {
            var mensagemErro = string.Empty;
            var repositorioDeScriptDeBanco = new RepositorioDeScriptDeBanco(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);

            var unidade = repositorioDeUnidadeDeDesenvolvimento.Obter(script.IdUnidadeDesenvolvimento);

            if (unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Pendente.GetHashCode() || 
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.AguardandoPublicacaoEmProducao.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado.GetHashCode())
                throw new Exception("Não é permitido incluir script de banco com a unidade de desenvolvimento Finalizada ou Pendente.");

            script.Ordem = unidade.Scripts.Count() + 1;

            if (unidade.UnidadeDeDesenvolvimentoDeBanco.Count() > 0)
            {
                unidade.Scripts.Add(script);
                if (ValidarScript(unidade, out mensagemErro) == false)
                {
                    throw new Exception(mensagemErro);
                }
            }

            repositorioDeScriptDeBanco.Criar(script);
            _unitOfWork.SaveChanges();
        }

        public void Atualizar(ScriptDeBanco script)
        {
            var mensagemErro = string.Empty;
            var repositorioDeScriptDeBanco = new RepositorioDeScriptDeBanco(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var _script = repositorioDeScriptDeBanco.Obter(script.IdScript);
            var unidade = repositorioDeUnidadeDeDesenvolvimento.Obter(script.IdUnidadeDesenvolvimento);

            if (unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Pendente.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.AguardandoPublicacaoEmProducao.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado.GetHashCode())
                throw new Exception("Não é permitido alterar script de banco com a unidade de desenvolvimento Finalizada ou Pendente.");

            if (_script.Executado)
                throw new Exception("Não é permitido alterar script de banco já executado.");


            if (unidade.UnidadeDeDesenvolvimentoDeBanco.Count() > 0)
            {
                unidade.Scripts.Single(s => s.IdScript == script.IdScript).Script = script.Script;
                if (ValidarScript(unidade, out mensagemErro) == false)
                {
                    throw new Exception(mensagemErro);
                }
            }

            repositorioDeScriptDeBanco.Atualizar(script);
            _unitOfWork.SaveChanges();
        }

        private bool AtualizarScripts(IEnumerable<ScriptDeBanco> scripts)
        {
            var repositorioDeScriptDeBanco = new RepositorioDeScriptDeBanco(_unitOfWork);
            ScriptDeBanco _script;

            try
            {
                foreach (ScriptDeBanco s in scripts)
                {
                    _script = repositorioDeScriptDeBanco.Obter(s.IdScript);
                    _script.Executado = true;
                    repositorioDeScriptDeBanco.Atualizar(_script);
                }

                _unitOfWork.SaveChanges();

                return true;
            }
            catch { return false; }
        }

        private bool AtualizarScripts(ScriptDeBanco scripts)
        {
            var repositorioDeScriptDeBanco = new RepositorioDeScriptDeBanco(_unitOfWork);
            try
            {
                ScriptDeBanco script = repositorioDeScriptDeBanco.Obter(scripts.IdScript);
                script.Executado = true;
                repositorioDeScriptDeBanco.Atualizar(script);
                this._unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public void Apagar(ScriptDeBanco script)
        {
            var mensagemErro = string.Empty;
            var repositorioDeScriptDeBanco = new RepositorioDeScriptDeBanco(_unitOfWork);
            var _script = repositorioDeScriptDeBanco.Obter(script.IdScript);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);

            if (_script.Executado)
                throw new Exception("Não é permitido alterar script de banco já executado.");

            var unidade = repositorioDeUnidadeDeDesenvolvimento.Obter(script.IdUnidadeDesenvolvimento);
            unidade.Scripts.Single(s => s.IdScript == script.IdScript).IsDeleted = true;

            if (unidade.UnidadeDeDesenvolvimentoDeBanco.Count() > 0 && ValidarScript(unidade, out mensagemErro) == false)
            {
                throw new Exception(mensagemErro);
            }

            repositorioDeScriptDeBanco.Apagar(script);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<ScriptDeBanco> Listar()
        {
            var repositorioDeScriptDeBanco = new RepositorioDeScriptDeBanco(_unitOfWork);
            return repositorioDeScriptDeBanco.Listar();
        }

        public ScriptDeBanco Obter(ScriptDeBanco script)
        {
            var repositorioDeScriptDeBanco = new RepositorioDeScriptDeBanco(_unitOfWork);
            return repositorioDeScriptDeBanco.Obter(script.IdScript);
        }

        public bool ValidarScript(UnidadeDeDesenvolvimento unidade, out string mensagem)
        {
            return ExecutarScript(unidade,out mensagem,false);
        }

        public bool ExecutarScript(UnidadeDeDesenvolvimento unidade, out string mensagem)
        {
            ProcessoDeUnidadeDeDesenvolvimento desenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();
            UnidadeDeDesenvolvimento desenvolvimento2 = desenvolvimento.Obter(unidade);
            desenvolvimento2.ExecutantoScript = true;
            desenvolvimento.Atualizar(desenvolvimento2);
            bool flag = this.ExecutarScript(unidade, out mensagem, true);
            desenvolvimento2.ExecutantoScript = false;
            desenvolvimento.Atualizar(desenvolvimento2);
            return flag;
        }


        private bool ExecutarScript(UnidadeDeDesenvolvimento unidade, out string mensagem, bool executar)
        {
            if (unidade.UnidadeDeDesenvolvimentoDeBanco.Count() == 0)
            {
                mensagem = string.Empty;
                return false;
            }

            var unitOfWorkOfScript = new List<UnitOfWorkOfScript>();

            foreach (UnidadeDeDesenvolvimentoDeBanco banco in unidade.UnidadeDeDesenvolvimentoDeBanco)
            {
                unitOfWorkOfScript.Add(new UnitOfWorkOfScript(banco.ServidorDeBanco.StringConexao, banco.NomeBanco));
            }


            var scripts = unidade.Scripts.Where(s => s.IsDeleted == false && s.Executado == false).OrderBy(s => s.Ordem);

            try
            {
                if (!executar)
                {
                    unitOfWorkOfScript.ForEach(u => u.BeginTransaction());
                    //unitOfWorkOfScript.BeginTransaction();
                }
                foreach (ScriptDeBanco script in scripts)
                {
                    if (executar)
                    {
                        unitOfWorkOfScript.ForEach(u => u.ExecutarScript(script));
                        this.AtualizarScripts(script);
                    }
                    else
                    {
                        unitOfWorkOfScript.ForEach(u => u.ValidarScript(script));
                    }
                }
                mensagem = string.Empty;
                return true;
            }
            catch (Exception exception)
            {
                mensagem = exception.Message;
                return false;
            }
            finally
            {
                if (!executar)
                {
                    unitOfWorkOfScript.ForEach(u => u.RollBackTransaction());
                    //unitOfWorkOfScript.RollBackTransaction();
                }
            }
        }
    
    }
}
