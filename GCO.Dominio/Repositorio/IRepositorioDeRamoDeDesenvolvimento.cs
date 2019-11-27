using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeRamoDeDesenvolvimento
    {
        void Criar(RamoDeDesenvolvimento ramo);

        void Atualizar(RamoDeDesenvolvimento ramo);

        void Apagar(RamoDeDesenvolvimento ramo);

        RamoDeDesenvolvimento Obter(int idRamo);

        IQueryable<RamoDeDesenvolvimento> Listar();
    }
}
