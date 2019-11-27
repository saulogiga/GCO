using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeServidorDeBanco
    {
        void Criar(ServidorDeBanco servidor);

        void Atualizar(ServidorDeBanco servidor);

        void Apagar(ServidorDeBanco servidor);

        ServidorDeBanco Obter(int idServidor);

        IQueryable<ServidorDeBanco> Listar();
    }
}
