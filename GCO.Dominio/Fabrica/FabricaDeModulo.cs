using System;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Fabrica
{
    public static class FabricaDeModulo
    {
        public static Modulo Criar(string nome)
        {
            return new Modulo { Nome = nome, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
