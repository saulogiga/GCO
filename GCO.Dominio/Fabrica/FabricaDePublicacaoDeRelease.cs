using GCO.Dominio.Entidade;
using System;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDePublicacaoDeRelease
    {
        public static PublicacaoDeRelease Criar(string caminhoOrigem, string pastaOrigem, string caminhoDestino, string arqNaoSelecionados, string dirNaoSelecionados, string release, UnidadeDeDesenvolvimento unidadeDeDesenvolvimento, PublicacaoDeStatus publicacaoDeStatus, Usuario usuario)
        {
            return new PublicacaoDeRelease {CaminhoOrigem = caminhoOrigem, PastaOrigem = pastaOrigem, CaminhoDestino = caminhoDestino, ArqNaoSelecionados = arqNaoSelecionados, DirNaoSelecionados = dirNaoSelecionados, Release = release, IdUnidadeDesenvolvimento = unidadeDeDesenvolvimento.IdUnidadeDesenvolvimento, UnidadeDeDesenvolvimento = unidadeDeDesenvolvimento, IdPublicacaoStatus = publicacaoDeStatus.IdPublicacaoStatus, PublicacaoStatus = publicacaoDeStatus, UserId = usuario.UserId, Usuario = usuario, DataInclusao = DateTime.Now,  IsDeleted = false, 
                                            Detalhes = @"PUBLISH RELEASE: " + release +
                                                              "\nUSUÁRIO: " + usuario.UserName +
                                                                 "\nDATA: " + DateTime.Now +
                                                   "\nDESTINO DO PUBLISH: " + unidadeDeDesenvolvimento.CaminhoPublish + caminhoDestino.Replace("//","/")
            };
        }
    }
}
