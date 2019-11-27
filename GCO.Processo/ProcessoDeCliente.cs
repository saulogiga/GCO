using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeCliente
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public Cliente Criar(Cliente cliente)
        {
            var repositorioDeCliente = new RepositorioDeCliente(_unitOfWork);

            repositorioDeCliente.Criar(cliente);
            _unitOfWork.SaveChanges();
            return cliente;
        }

        public void Atualizar(Cliente cliente)
        {
            var repositorioDeCliente = new RepositorioDeCliente(_unitOfWork);

            repositorioDeCliente.Atualizar(cliente);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Cliente> Listar()
        {
            var repositorioDeCliente = new RepositorioDeCliente(_unitOfWork);
            return repositorioDeCliente.Listar();
        }

        public Cliente Obter(Cliente cliente)
        {
            var repositorioDeCliente = new RepositorioDeCliente(_unitOfWork);
            return repositorioDeCliente.Obter(cliente.IdCliente);
        }
    }
}
