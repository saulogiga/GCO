using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeTipoDeUnidadeDeDesenvolvimento
    {
        public static TipoDeUnidadeDeDesenvolvimento Criar(string nome)
        {
            return new TipoDeUnidadeDeDesenvolvimento { Nome = nome, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
