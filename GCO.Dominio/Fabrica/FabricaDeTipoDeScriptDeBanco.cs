using System;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeTipoDeScriptDeBanco
    {
        public static TipoDeScriptDeBanco Criar(string nome)
        {
            return new TipoDeScriptDeBanco { Nome = nome, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
