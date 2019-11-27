using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeRamoDeProducao
    {
        public static RamoDeProducao Criar(string caminho, string comentario, string versao, string solicitacao, DateTime dataPublicacao, Entidade.Repositorio repositorio, Projeto projeto, Modulo modulo)
        {
            return new RamoDeProducao { Caminho = caminho, Comentario = comentario, Repositorio = repositorio, IdRepositorio = repositorio.IdRepositorio, Versao = versao, DataPublicacao = dataPublicacao, Solicitacao = solicitacao, Projeto = projeto, IdProjeto = projeto.IdProjeto,Modulo = modulo, IdModulo = modulo.IdModulo, DataInclusao = DateTime.Now, DataAlteracao = DateTime.Now, IsDeleted = false };
        }
    }
}
