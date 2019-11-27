using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeUnidadeDeDesenvolvimento
    {
        void Criar(UnidadeDeDesenvolvimento unidade);

        void Atualizar(UnidadeDeDesenvolvimento unidade);

        void Apagar(UnidadeDeDesenvolvimento unidade);

        UnidadeDeDesenvolvimento Obter(int idUnidade);

        IQueryable<UnidadeDeDesenvolvimento> Listar();
    }
}
