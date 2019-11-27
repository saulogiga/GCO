using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Repositorio;
using GCO.Dominio.Entidade;
using System.Data.Entity;

namespace GCO.Infra
{
    public class RepositorioDeScriptDeBanco : IRepositorioDeScriptDeBanco
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeScriptDeBanco(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(ScriptDeBanco script)
        {
            _unitOfWork.ScriptDeBanco.Add(script);
        }

        public void Atualizar(ScriptDeBanco script)
        {
            var atual = Obter(script.IdScript);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(script);
        }

        public void Apagar(ScriptDeBanco script)
        {
            var atual = Obter(script.IdScript);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(atual);
        }

        public ScriptDeBanco Obter(Int64 idScript)
        {
            return _unitOfWork.ScriptDeBanco.Include(s => s.UnidadeDesenvolvimento).Include(s => s.TipoDeScriptDeBanco).Include(s => s.Usuario).SingleOrDefault(s => s.IdScript == idScript && s.IsDeleted == false);
        }

        public IQueryable<ScriptDeBanco> Listar()
        {
            return _unitOfWork.ScriptDeBanco.Include(s => s.UnidadeDesenvolvimento).Include(s => s.TipoDeScriptDeBanco).Include(s => s.Usuario).Where(s => s.IsDeleted == false);
        }
    }
}
