using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDePerfil
    {
        Perfil Obter(int idPerfil);

        IQueryable<Perfil> Listar();
    }
}
