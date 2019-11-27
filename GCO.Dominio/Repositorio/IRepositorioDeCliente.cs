using GCO.Dominio.Entidade;
using System.Linq;
namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeCliente
    {
        void Criar(Cliente cliente);

        void Atualizar(Cliente cliente);

        void Apagar(Cliente cliente);

        Cliente Obter(int idCliente);

        IQueryable<Cliente> Listar();
    }
}
