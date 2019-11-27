using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeTipoDeScriptDeBanco
    {
        void Criar(TipoDeScriptDeBanco tipo);

        void Atualizar(TipoDeScriptDeBanco tipo);

        void Apagar(TipoDeScriptDeBanco tipo);

        TipoDeScriptDeBanco Obter(int idTipo);

        IQueryable<TipoDeScriptDeBanco> Listar();
    }
}
