using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Entidade;

namespace GCO.Infra
{
    public class RepositorioDeTipoDeWebConfig
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeTipoDeWebConfig(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public TipoDeWebConfig Obter(int idTipoWebConfig)
        {
            return _unitOfWork.TipoDeWebConfig.SingleOrDefault(t => t.IdTipoWebConfig == idTipoWebConfig && t.IsDeleted == false);
        }

        public IQueryable<TipoDeWebConfig> Listar()
        {
            return _unitOfWork.TipoDeWebConfig.Where(t => t.IsDeleted == false);
        }
    }
}
