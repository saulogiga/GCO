using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeWebConfig
    {
        void Criar(WebConfig webConfig);

        void Atualizar(WebConfig webConfig);

        void Apagar(WebConfig webConfig);

        WebConfig Obter(int idWebConfig);

        IQueryable<WebConfig> Listar();
    }
}
