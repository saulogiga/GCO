using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeUsuario
    {
        Usuario Obter(int idUsuario);

        IQueryable<Usuario> Listar();
    }
}
