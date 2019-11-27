using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeCliente
    {
        public static Cliente Criar(string nome)
        {
            return new Cliente { Nome = nome, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
