using System;
using System.Collections.Generic;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeUnidadeDeDesenvolvimento
    {
        public static UnidadeDeDesenvolvimento Criar(string numeroTicket, string numeroSolicitacao, int idTipoUnidadeDesenvolvimento, int idProjeto, int idSolicitante, int idStatusUnidadeDesenvolvimento, ICollection<Usuario> usuarios, string teamProject, string caminhoBuild, string caminhoPublish, string nomeBuild)
        {
            return new UnidadeDeDesenvolvimento { NumeroTicket = numeroTicket, NumeroSolicitacao = numeroSolicitacao, IdTipoUnidadeDesenvolvimento = idTipoUnidadeDesenvolvimento, IdProjeto = idProjeto, IdSolicitante = idSolicitante, IdStatusUnidadeDesenvolvimento = idStatusUnidadeDesenvolvimento , DataInclusao = DateTime.Now, IsDeleted = false, Desenvolvedores = usuarios, ExecutantoScript = false, TeamProject = teamProject, CaminhoBuild = caminhoBuild, CaminhoPublish = caminhoPublish, NomeBuild = nomeBuild };
        }

        public static UnidadeDeDesenvolvimento Criar(string numeroTicket, string numeroSolicitacao, int idTipoUnidadeDesenvolvimento, int idProjeto, int idSolicitante, int idStatusUnidadeDesenvolvimento, ICollection<Usuario> usuarios, ICollection<UnidadeDeDesenvolvimentoDeBanco> bancos, string teamProject, string caminhoBuild, string caminhoPublish, string nomeBuild)
        {
            return new UnidadeDeDesenvolvimento { NumeroTicket = numeroTicket, NumeroSolicitacao = numeroSolicitacao, IdTipoUnidadeDesenvolvimento = idTipoUnidadeDesenvolvimento, IdProjeto = idProjeto, IdSolicitante = idSolicitante, IdStatusUnidadeDesenvolvimento = idStatusUnidadeDesenvolvimento, DataInclusao = DateTime.Now, IsDeleted = false, UnidadeDeDesenvolvimentoDeBanco = bancos, Desenvolvedores = usuarios, ExecutantoScript = false, TeamProject = teamProject, CaminhoBuild = caminhoBuild, CaminhoPublish = caminhoPublish, NomeBuild = nomeBuild };
        }

        public static UnidadeDeDesenvolvimento Criar(string numeroTicket, string numeroSolicitacao, int idTipoUnidadeDesenvolvimento, int idProjeto, int idSolicitante, int idStatusUnidadeDesenvolvimento, ICollection<Usuario> usuarios, string teamProject, string caminhoBuild, string caminhoPublish, string nomeBuild, string descricaoModulos)
        {
            return new UnidadeDeDesenvolvimento { NumeroTicket = numeroTicket, NumeroSolicitacao = numeroSolicitacao, IdTipoUnidadeDesenvolvimento = idTipoUnidadeDesenvolvimento, IdProjeto = idProjeto, IdSolicitante = idSolicitante, IdStatusUnidadeDesenvolvimento = idStatusUnidadeDesenvolvimento, DescricaoModulos = descricaoModulos, DataInclusao = DateTime.Now, IsDeleted = false, Desenvolvedores = usuarios, ExecutantoScript = false, TeamProject = teamProject, CaminhoBuild = caminhoBuild, CaminhoPublish = caminhoPublish, NomeBuild = nomeBuild };
        }
    }
}
