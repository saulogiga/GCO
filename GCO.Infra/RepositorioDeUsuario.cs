using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GCO.Infra
{
    public class RepositorioDeUsuario : IRepositorioDeUsuario
    {

        private UnitOfWork _unitOfWork;
        public RepositorioDeUsuario(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public Usuario Obter(int idUsuario)
        {
            return _unitOfWork.Usuario.SingleOrDefault(u => u.UserId == idUsuario);
        }

        public IQueryable<Usuario> Listar()
        {
            return _unitOfWork.Usuario;
        }
    }
}
