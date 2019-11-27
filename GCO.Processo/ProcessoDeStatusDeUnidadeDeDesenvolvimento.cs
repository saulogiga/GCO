using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeStatusDeUnidadeDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public StatusDeUnidadeDeDesenvolvimento Criar(StatusDeUnidadeDeDesenvolvimento status)
        {
            var repositorioDeStatusDeUnidadeDeDesenvolvimento = new RepositorioDeStatusDeUnidadeDeDesenvolvimento(_unitOfWork);

            repositorioDeStatusDeUnidadeDeDesenvolvimento.Criar(status);
            _unitOfWork.SaveChanges();
            return status;
        }

        public void Atualizar(StatusDeUnidadeDeDesenvolvimento status)
        {
            var repositorioDeStatusDeUnidadeDeDesenvolvimento = new RepositorioDeStatusDeUnidadeDeDesenvolvimento(_unitOfWork);

            repositorioDeStatusDeUnidadeDeDesenvolvimento.Atualizar(status);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<StatusDeUnidadeDeDesenvolvimento> Listar()
        {
            var repositorioDeStatusDeUnidadeDeDesenvolvimento = new RepositorioDeStatusDeUnidadeDeDesenvolvimento(_unitOfWork);
            return repositorioDeStatusDeUnidadeDeDesenvolvimento.Listar();
        }

        public StatusDeUnidadeDeDesenvolvimento Obter(StatusDeUnidadeDeDesenvolvimento status)
        {
            var repositorioDeStatusDeUnidadeDeDesenvolvimento = new RepositorioDeStatusDeUnidadeDeDesenvolvimento(_unitOfWork);
            return repositorioDeStatusDeUnidadeDeDesenvolvimento.Obter(status.IdStatusUnidadeDesenvolvimento);
        }
    }
}
