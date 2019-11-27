using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using GCO.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GCO.Processo
{
    public class ProcessoDePublicacaoDeRelease
    {
        public void Criar(PublicacaoDeRelease publicacao)
        {
            var _unitOfWork = new UnitOfWork();

            var repositorioDePublicacaoDeRelease = new RepositorioDePublicacaoDeRelease(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var repositorioDePublicacaoDeStatus = new RepositorioDePublicacaoDeStatus(_unitOfWork);
            var repositorioDeUsuario = new RepositorioDeUsuario(_unitOfWork);

            var publicacaoDeRealse = FabricaDePublicacaoDeRelease.Criar(publicacao.CaminhoOrigem,
                                                                       publicacao.PastaOrigem,
                                                                       publicacao.CaminhoDestino,
                                                                       publicacao.ArqNaoSelecionados,
                                                                       publicacao.DirNaoSelecionados,
                                                                       publicacao.Release,
                                                                       repositorioDeUnidadeDeDesenvolvimento.Obter(publicacao.IdUnidadeDesenvolvimento),
                                                                       repositorioDePublicacaoDeStatus.Obter(publicacao.IdPublicacaoStatus),
                                                                       repositorioDeUsuario.Obter(publicacao.UserId));

            repositorioDePublicacaoDeRelease.Criar(publicacaoDeRealse);
            _unitOfWork.SaveChanges();
            _unitOfWork.Dispose();

        }

        public void Atualizar(PublicacaoDeRelease publicacao)
        {
            var _unitOfWork = new UnitOfWork();

            var repositorioDePublicacaoDeRelease = new RepositorioDePublicacaoDeRelease(_unitOfWork);
            var repositorioDePublicacaoDeStatus = new RepositorioDePublicacaoDeStatus(_unitOfWork);

            var publicacaoDeRealse = repositorioDePublicacaoDeRelease.Obter(publicacao.IdPublicacaoRelease);

            //Apenas os campos que pode alterar...
            publicacaoDeRealse.DataAlteracao = DateTime.Now;
            publicacaoDeRealse.DataFimPublicacao = publicacao.DataFimPublicacao;
            publicacaoDeRealse.DataInicioPublicacao = publicacao.DataInicioPublicacao;
            publicacaoDeRealse.Detalhes = publicacao.Detalhes;

            if (publicacao.IdPublicacaoStatus != publicacaoDeRealse.IdPublicacaoStatus)
            {
                publicacaoDeRealse.IdPublicacaoStatus = publicacao.IdPublicacaoStatus;
                publicacaoDeRealse.PublicacaoStatus = repositorioDePublicacaoDeStatus.Obter(publicacao.IdPublicacaoStatus);
            }


            repositorioDePublicacaoDeRelease.Atualizar(publicacaoDeRealse);
            _unitOfWork.SaveChanges();
            _unitOfWork.Dispose();
        }

        public void Apagar(PublicacaoDeRelease publicacao)
        {
            throw new NotImplementedException();
        }

        public PublicacaoDeRelease Obter(Int64 idPublicacaoRealse)
        {
            var _unitOfWork = new UnitOfWork();
            var repositorioDePublicacaoDeRelease = new RepositorioDePublicacaoDeRelease(_unitOfWork);
            var retorno = repositorioDePublicacaoDeRelease.Obter(idPublicacaoRealse);
            _unitOfWork.Dispose();
            return retorno;
        }

        public IEnumerable<PublicacaoDeRelease> Listar()
        {
            var _unitOfWork = new UnitOfWork();
            var repositorioDePublicacaoDeRelease = new RepositorioDePublicacaoDeRelease(_unitOfWork);
            var retorno = repositorioDePublicacaoDeRelease.Listar().ToList();
            _unitOfWork.Dispose();
            return retorno;
        }

        public IEnumerable<PublicacaoDeRelease> ListarPendentes()
        {
            var _unitOfWork = new UnitOfWork();
            var repositorioDePublicacaoDeRelease = new RepositorioDePublicacaoDeRelease(_unitOfWork);
            var retorno = repositorioDePublicacaoDeRelease.Listar().Where(p => p.IdPublicacaoStatus == (int)EnumDeStatusDePublicacao.AGUARDANDOPUBLICAÇÃO).ToList();
            _unitOfWork.Dispose();
            return retorno;
        }

        public IEnumerable<PublicacaoDeRelease> ListarPorUnidadeDesenvolvimento(int idUnidadeDesenvolvimento)
        {
            var _unitOfWork = new UnitOfWork();
            var repositorioDePublicacaoDeRelease = new RepositorioDePublicacaoDeRelease(_unitOfWork);
            var retorno = repositorioDePublicacaoDeRelease.Listar().Where(p => p.IdUnidadeDesenvolvimento == idUnidadeDesenvolvimento).OrderByDescending(p => p.DataInclusao).ToList();
            _unitOfWork.Dispose();
            return retorno;
        }
    }
}
