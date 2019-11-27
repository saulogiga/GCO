using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeProjeto
    {
        void Criar(Projeto projeto);

        void Atualizar(Projeto projeto);

        void Apagar(Projeto projeto);

        Projeto Obter(int idProjeto);

        IQueryable<Projeto> Listar();
    }
}
