using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    interface IRepositorioDeArquivoDeUnidadeDeDesenvolvimento
    {
        void Criar(ArquivoDeUnidadeDeDesenvolvimento arquivoDeUnidadeDeDesenvolvimento);

        void Atualizar(ArquivoDeUnidadeDeDesenvolvimento arquivoDeUnidadeDeDesenvolvimento);

        void Apagar(ArquivoDeUnidadeDeDesenvolvimento arquivoDeUnidadeDeDesenvolvimento);

        ArquivoDeUnidadeDeDesenvolvimento Obter(int idArquivoDeUnidadeDeDesenvolvimento);

        IQueryable<ArquivoDeUnidadeDeDesenvolvimento> Listar();
    }
}
