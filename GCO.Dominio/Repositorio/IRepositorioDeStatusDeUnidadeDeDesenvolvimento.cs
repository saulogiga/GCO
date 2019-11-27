using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeStatusDeUnidadeDeDesenvolvimento
    {
        void Criar(StatusDeUnidadeDeDesenvolvimento status);

        void Atualizar(StatusDeUnidadeDeDesenvolvimento status);

        void Apagar(StatusDeUnidadeDeDesenvolvimento status);

        StatusDeUnidadeDeDesenvolvimento Obter(int idStatus);

        IQueryable<StatusDeUnidadeDeDesenvolvimento> Listar();
    }
}
