using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCO.Infra
{
    public class RepositorioDeTipoDeUnidadeDeDesenvolvimento : IRepositorioDeTipoDeUnidadeDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeTipoDeUnidadeDeDesenvolvimento(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento)
        {
            _unitOfWork.TipoDeUnidadeDeDesenvolvimento.Add(tipoDeUnidadeDeDesenvolvimento);
        }

        public void Atualizar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento)
        {
            var atual = Obter(tipoDeUnidadeDeDesenvolvimento.IdTipoUnidadeDesenvolvimento);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(tipoDeUnidadeDeDesenvolvimento);
        }

        public void Apagar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento)
        {
            var atual = Obter(tipoDeUnidadeDeDesenvolvimento.IdTipoUnidadeDesenvolvimento);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(tipoDeUnidadeDeDesenvolvimento);
        }

        public TipoDeUnidadeDeDesenvolvimento Obter(int idTipoDeUnidadeDeDesenvolvimento)
        {
            return _unitOfWork.TipoDeUnidadeDeDesenvolvimento.SingleOrDefault(s => s.IdTipoUnidadeDesenvolvimento == idTipoDeUnidadeDeDesenvolvimento && s.IsDeleted == false);
        }

        public IQueryable<TipoDeUnidadeDeDesenvolvimento> Listar()
        {
            return _unitOfWork.TipoDeUnidadeDeDesenvolvimento.Where(s => s.IsDeleted == false);
        }
    }
}
