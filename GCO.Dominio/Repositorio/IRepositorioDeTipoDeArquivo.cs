using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeTipoDeArquivo
    {
        TipoDeArquivo Obter(int idTipoArquivo);

        IQueryable<TipoDeArquivo> Listar();
    }
}
