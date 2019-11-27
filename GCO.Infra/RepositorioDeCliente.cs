using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCO.Infra
{
    public class RepositorioDeCliente : IRepositorioDeCliente
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeCliente(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }


        public void Criar(Cliente cliente)
        {
            _unitOfWork.Cliente.Add(cliente);
        }

        public void Atualizar(Cliente cliente)
        {
            var atual = Obter(cliente.IdCliente);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(cliente);
        }

        public void Apagar(Cliente cliente)
        {
            var atual = Obter(cliente.IdCliente);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(cliente);
        }

        public Cliente Obter(int idCliente)
        {
            return _unitOfWork.Cliente.SingleOrDefault(m => m.IdCliente == idCliente && m.IsDeleted == false);
        }

        public IQueryable<Cliente> Listar()
        {
            return _unitOfWork.Cliente.Where(m => m.IsDeleted == false);
        }
    }
}
