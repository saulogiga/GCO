using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeTipoDeWebConfig
    {
        TipoDeWebConfig Obter(int idTipoWebConfig);

        IQueryable<TipoDeWebConfig> Listar();
    }
}
