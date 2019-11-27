
using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeRepositorio
    {
        public static GCO.Dominio.Entidade.Repositorio Criar(string nome, string caminho, TipoDeRepositorio tipoRepositorio)
        {
            return new GCO.Dominio.Entidade.Repositorio { Nome = nome, Caminho = caminho, TipoRepositorio = tipoRepositorio, IdTipoRepositorio = tipoRepositorio.IdTipoRepositorio, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
