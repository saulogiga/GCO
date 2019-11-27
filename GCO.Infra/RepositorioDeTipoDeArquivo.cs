using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Entidade;

namespace GCO.Infra
{
    public class RepositorioDeTipoDeArquivo
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeTipoDeArquivo(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public TipoDeArquivo Obter(int idTipoArquivo)
        {
            return _unitOfWork.TipoDeArquivo.SingleOrDefault(t => t.IdTipoArquivo == idTipoArquivo && t.IsDeleted == false);
        }

        public IQueryable<TipoDeArquivo> Listar()
        {
            return _unitOfWork.TipoDeArquivo.Where(t => t.IsDeleted == false);
        }
    }
}
