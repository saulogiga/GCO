using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCO.Infra
{
    public class RepositorioDePerfil : IRepositorioDePerfil
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDePerfil(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public Perfil Obter(int idPerfil)
        {
            return _unitOfWork.Perfil.SingleOrDefault(p => p.RoleId == idPerfil);
        }

        public IQueryable<Perfil> Listar()
        {
            return _unitOfWork.Perfil;
        }
    }
}
