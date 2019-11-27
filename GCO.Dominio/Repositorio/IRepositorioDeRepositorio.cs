using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeRepositorio
    {
        void Criar(Entidade.Repositorio repositorio);

        void Atualizar(Entidade.Repositorio repositorio);

        void Apagar(Entidade.Repositorio repositorio);

        Entidade.Repositorio Obter(int idrepositorio);

        IQueryable<Entidade.Repositorio> Listar();
    }
}
