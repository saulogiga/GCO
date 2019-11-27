using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Repositorio;
using GCO.Dominio.Entidade;

namespace GCO.Infra
{
    public class RepositorioDeModulo : IRepositorioDeModulo
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeModulo(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }


        public void Criar(Modulo modulo)
        {
            _unitOfWork.Modulo.Add(modulo);
        }

        public void Atualizar(Modulo modulo)
        {
            var atual = Obter(modulo.IdModulo);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(modulo);
        }

        public void Apagar(Modulo modulo)
        {
            var atual = Obter(modulo.IdModulo);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(modulo);
        }

        public Modulo Obter(int idModulo)
        {
            return _unitOfWork.Modulo.SingleOrDefault(m => m.IdModulo == idModulo && m.IsDeleted == false);
        }

        public IQueryable<Modulo> Listar()
        {
            return _unitOfWork.Modulo.Where(m => m.IsDeleted == false);
        }
    }
}
