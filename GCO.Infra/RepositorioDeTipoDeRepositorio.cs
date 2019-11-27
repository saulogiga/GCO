using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCO.Infra
{
    public class RepositorioDeTipoDeRepositorio : IRepositorioDeTipoDeRepositorio
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeTipoDeRepositorio(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public Dominio.Entidade.TipoDeRepositorio Obter(int idTipoDeRepositorio)
        {
            return _unitOfWork.TipoDeRepositorio.SingleOrDefault(r => r.IdTipoRepositorio == idTipoDeRepositorio && r.IsDeleted == false);
        }

        public IQueryable<Dominio.Entidade.TipoDeRepositorio> Listar()
        {
            return _unitOfWork.TipoDeRepositorio.Where(r => r.IsDeleted == false);
        }
    }
}
