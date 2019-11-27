using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public static class FabricaDeStatusDeUnidadeDeDesenvolvimento
    {
        public static StatusDeUnidadeDeDesenvolvimento Criar(string nome)
        {
            return new StatusDeUnidadeDeDesenvolvimento { Nome = nome, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
