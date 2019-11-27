using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Repositorio;

namespace GCO.Infra
{
    public class RepositorioDeTipoDeScriptDeBanco : IRepositorioDeTipoDeScriptDeBanco
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeTipoDeScriptDeBanco(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(Dominio.Entidade.TipoDeScriptDeBanco tipo)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Dominio.Entidade.TipoDeScriptDeBanco tipo)
        {
            throw new NotImplementedException();
        }

        public void Apagar(Dominio.Entidade.TipoDeScriptDeBanco tipo)
        {
            throw new NotImplementedException();
        }

        public Dominio.Entidade.TipoDeScriptDeBanco Obter(int idTipo)
        {
            return _unitOfWork.TipoDeScriptDeBanco.SingleOrDefault(m => m.IdTipoScript == idTipo && m.IsDeleted == false);
        }

        public IQueryable<Dominio.Entidade.TipoDeScriptDeBanco> Listar()
        {
            return _unitOfWork.TipoDeScriptDeBanco.Where(m => m.IsDeleted == false);
        }
    }
}
