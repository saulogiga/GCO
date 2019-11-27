using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeProjeto
    {
        public static Projeto Criar(string nome, int idCliente)
        {
            return new Projeto { Nome = nome, IdCliente = idCliente, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
