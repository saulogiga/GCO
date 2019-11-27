using System;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeScriptDeBanco
    {
        public static ScriptDeBanco Criar(string nome, string comentario, string script, bool executado, int idUnidadeDesenvolvimento, int idTipoScript, int userId)
        {
            return new ScriptDeBanco { Nome = nome,Comentario = comentario, Script = script, Executado = executado, IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento, IdTipoScript = idTipoScript, UserId = userId, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
