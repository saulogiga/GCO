using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Repositorio;
using GCO.Dominio.Entidade;

namespace GCO.Infra
{
    public class RepositorioDeServidorDeBanco : IRepositorioDeServidorDeBanco
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeServidorDeBanco(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(ServidorDeBanco servidor)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(ServidorDeBanco servidor)
        {
            throw new NotImplementedException();
        }

        public void Apagar(ServidorDeBanco servidor)
        {
            throw new NotImplementedException();
        }

        public ServidorDeBanco Obter(int idServidor)
        {
            return _unitOfWork.ServidorDeBanco.SingleOrDefault(s => s.IdServidorBanco == idServidor && s.IsDeleted == false);
        }

        public IQueryable<ServidorDeBanco> Listar()
        {
            return _unitOfWork.ServidorDeBanco.Where(s => s.IsDeleted == false);
        }
    }
}
