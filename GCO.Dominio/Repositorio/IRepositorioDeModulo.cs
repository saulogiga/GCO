using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeModulo
    {
        void Criar(Modulo modulo);

        void Atualizar(Modulo modulo);

        void Apagar(Modulo modulo);

        Modulo Obter(int idModulo);

        IQueryable<Modulo> Listar();
    }
}
