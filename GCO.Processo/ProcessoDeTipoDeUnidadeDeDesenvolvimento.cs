using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeTipoDeUnidadeDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public TipoDeUnidadeDeDesenvolvimento Criar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento)
        {
            var repositorioDeTipoDeUnidadeDeDesenvolvimento = new RepositorioDeTipoDeUnidadeDeDesenvolvimento(_unitOfWork);

            repositorioDeTipoDeUnidadeDeDesenvolvimento.Criar(tipoDeUnidadeDeDesenvolvimento);
            _unitOfWork.SaveChanges();
            return tipoDeUnidadeDeDesenvolvimento;
        }

        public void Atualizar(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento)
        {
            var repositorioDeTipoDeUnidadeDeDesenvolvimento = new RepositorioDeTipoDeUnidadeDeDesenvolvimento(_unitOfWork);

            repositorioDeTipoDeUnidadeDeDesenvolvimento.Atualizar(tipoDeUnidadeDeDesenvolvimento);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<TipoDeUnidadeDeDesenvolvimento> Listar()
        {
            var repositorioDeTipoDeUnidadeDeDesenvolvimento = new RepositorioDeTipoDeUnidadeDeDesenvolvimento(_unitOfWork);
            return repositorioDeTipoDeUnidadeDeDesenvolvimento.Listar();
        }

        public TipoDeUnidadeDeDesenvolvimento Obter(TipoDeUnidadeDeDesenvolvimento tipoDeUnidadeDeDesenvolvimento)
        {
            var repositorioDeTipoDeUnidadeDeDesenvolvimento = new RepositorioDeTipoDeUnidadeDeDesenvolvimento(_unitOfWork);
            return repositorioDeTipoDeUnidadeDeDesenvolvimento.Obter(tipoDeUnidadeDeDesenvolvimento.IdTipoUnidadeDesenvolvimento);
        }
    }
}
