using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeRamoDeProducao
    {
        void Criar(RamoDeProducao ramo);

        void Atualizar(RamoDeProducao ramo);

        void Apagar(RamoDeProducao ramo);

        RamoDeProducao Obter(int idRamo);

        IQueryable<RamoDeProducao> Listar();
    }
}
