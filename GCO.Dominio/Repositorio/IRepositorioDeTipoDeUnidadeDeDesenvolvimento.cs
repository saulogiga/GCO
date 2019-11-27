using GCO.Dominio.Entidade;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeTipoDeUnidadeDeDesenvolvimento
    {
        void Criar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento);

        void Atualizar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento);

        void Apagar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento);

        TipoDeUnidadeDeDesenvolvimento Obter(int idTipoDeUnidadeDeDesenvolvimento);

        IQueryable<TipoDeUnidadeDeDesenvolvimento> Listar();
    }
}
