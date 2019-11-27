using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;

namespace GCO.Infra
{
    public class RepositorioDePublicacaoDeStatus : IRepositorioDePublicacaoDeStatus
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDePublicacaoDeStatus(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public PublicacaoDeStatus Obter(int idPublicacaoDeStatus)
        {
            return _unitOfWork.PublicacaoStatus.SingleOrDefault(p => p.IdPublicacaoStatus == idPublicacaoDeStatus);
        }

        public IQueryable<PublicacaoDeStatus> Listar()
        {
            return _unitOfWork.PublicacaoStatus.Where(p => p.IsDeleted == false);
        }
    }
}
