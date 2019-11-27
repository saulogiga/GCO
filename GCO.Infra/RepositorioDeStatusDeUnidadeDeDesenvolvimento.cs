using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCO.Infra
{
    public class RepositorioDeStatusDeUnidadeDeDesenvolvimento : IRepositorioDeStatusDeUnidadeDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeStatusDeUnidadeDeDesenvolvimento(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(StatusDeUnidadeDeDesenvolvimento status)
        {
            _unitOfWork.StatusDeUnidadeDeDesenvolvimento.Add(status);
        }

        public void Atualizar(StatusDeUnidadeDeDesenvolvimento status)
        {
            var atual = Obter(status.IdStatusUnidadeDesenvolvimento);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(status);
        }

        public void Apagar(StatusDeUnidadeDeDesenvolvimento status)
        {
            var atual = Obter(status.IdStatusUnidadeDesenvolvimento);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(status);
        }

        public StatusDeUnidadeDeDesenvolvimento Obter(int idStatus)
        {
            return _unitOfWork.StatusDeUnidadeDeDesenvolvimento.SingleOrDefault(s => s.IdStatusUnidadeDesenvolvimento == idStatus && s.IsDeleted == false);
        }

        public IQueryable<StatusDeUnidadeDeDesenvolvimento> Listar()
        {
            return _unitOfWork.StatusDeUnidadeDeDesenvolvimento.Where(s => s.IsDeleted == false);
        }
    }
}
