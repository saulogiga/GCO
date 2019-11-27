using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeTipoDeRepositorio
    {
        TipoDeRepositorio Obter(int idTipoDeRepositorio);

        IQueryable<TipoDeRepositorio> Listar();
    }
}
