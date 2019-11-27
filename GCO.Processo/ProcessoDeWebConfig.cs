using System;
using System.Collections.Generic;
using GCO.Dominio.Entidade;
using GCO.Infra;

namespace GCO.Processo
{
    public class ProcessoDeWebConfig
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void Criar(WebConfig webConfig)
        {
            var mensagemErro = string.Empty;
            var repositorioDeWebConfig = new RepositorioDeWebConfig(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);

            var unidade = repositorioDeUnidadeDeDesenvolvimento.Obter(webConfig.IdUnidadeDesenvolvimento);

            if (unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Pendente.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.AguardandoPublicacaoEmProducao.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado.GetHashCode())
                throw new Exception("Não é permitido incluir itens de Web.Config com a unidade de desenvolvimento Finalizada ou Pendente.");

            repositorioDeWebConfig.Criar(webConfig);
            _unitOfWork.SaveChanges();
        }

        public void Atualizar(WebConfig webConfig)
        {
            var mensagemErro = string.Empty;
            var repositorioDeWebConfig = new RepositorioDeWebConfig(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var unidade = repositorioDeUnidadeDeDesenvolvimento.Obter(webConfig.IdUnidadeDesenvolvimento);

            if (unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Pendente.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.AguardandoPublicacaoEmProducao.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado.GetHashCode())
                throw new Exception("Não é permitido alterar itens de Web.Config com a unidade de desenvolvimento Finalizada ou Pendente.");

            repositorioDeWebConfig.Atualizar(webConfig);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<WebConfig> Listar()
        {
            var repositorioDeWebConfig = new RepositorioDeWebConfig(_unitOfWork);
            return repositorioDeWebConfig.Listar();
        }

        public WebConfig Obter(WebConfig webConfig)
        {
            var repositorioDeWebConfig = new RepositorioDeWebConfig(_unitOfWork);
            return repositorioDeWebConfig.Obter(webConfig.IdWebConfig);
        }

        public void Apagar(WebConfig webConfig)
        {
            var mensagemErro = string.Empty;
            var repositorioDeWebConfig = new RepositorioDeWebConfig(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var unidade = repositorioDeUnidadeDeDesenvolvimento.Obter(webConfig.IdUnidadeDesenvolvimento);

            if (unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Pendente.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.AguardandoPublicacaoEmProducao.GetHashCode() ||
                unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado.GetHashCode())
                throw new Exception("Não é permitido alterar itens de Web.Config com a unidade de desenvolvimento Finalizada ou Pendente.");

            repositorioDeWebConfig.Apagar(webConfig);
            _unitOfWork.SaveChanges();
        }
    }
}
