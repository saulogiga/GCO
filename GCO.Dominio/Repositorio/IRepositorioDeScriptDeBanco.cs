using System;
using System.Linq;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDeScriptDeBanco
    {
        void Criar(ScriptDeBanco script);

        void Atualizar(ScriptDeBanco script);

        void Apagar(ScriptDeBanco script);

        ScriptDeBanco Obter(Int64 idScript);

        IQueryable<ScriptDeBanco> Listar();
    }
}
