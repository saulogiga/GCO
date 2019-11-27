using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDePublicacaoDeStatus
    {
        PublicacaoDeStatus Obter(int idPublicacaoDeStatus);

        IQueryable<PublicacaoDeStatus> Listar();
    }
}
