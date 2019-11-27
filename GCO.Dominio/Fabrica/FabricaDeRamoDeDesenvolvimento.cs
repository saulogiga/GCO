using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeRamoDeDesenvolvimento
    {
        public static RamoDeDesenvolvimento Criar(string caminho, string comentario,string versao, bool branch, Entidade.Repositorio repositorio,UnidadeDeDesenvolvimento unidadeDesenvolvimento,RamoDeProducao ramoDeProducao, Modulo modulo )
        {
            return new RamoDeDesenvolvimento { Caminho = caminho, Comentario = comentario, Branch = branch, Fechado = false, RamoDeProducao = ramoDeProducao, IdRamoProducao = ramoDeProducao.IdRamoProducao, Repositorio = repositorio, IdRepositorio = repositorio.IdRepositorio, UnidadeDeDesenvolvimento = unidadeDesenvolvimento, IdUnidadeDesenvolvimento = unidadeDesenvolvimento.IdUnidadeDesenvolvimento, Versao = versao, Modulo = modulo, IdModulo = modulo.IdModulo, DataInclusao = DateTime.Now, DataAlteracao = DateTime.Now, IsDeleted = false };
        }

        public static RamoDeDesenvolvimento Criar(string caminho, string comentario, string versao, bool branch, Entidade.Repositorio repositorio, UnidadeDeDesenvolvimento unidadeDesenvolvimento, Modulo modulo)
        {
            return new RamoDeDesenvolvimento { Caminho = caminho, Comentario = comentario, Branch = branch, Fechado = false, Repositorio = repositorio, IdRepositorio = repositorio.IdRepositorio, UnidadeDeDesenvolvimento = unidadeDesenvolvimento, IdUnidadeDesenvolvimento = unidadeDesenvolvimento.IdUnidadeDesenvolvimento, Versao = versao, Modulo = modulo, IdModulo = modulo.IdModulo, DataInclusao = DateTime.Now, DataAlteracao = DateTime.Now, IsDeleted = false };
        }
    }
}
