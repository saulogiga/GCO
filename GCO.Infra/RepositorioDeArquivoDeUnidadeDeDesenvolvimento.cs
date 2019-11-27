using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Entidade;

namespace GCO.Infra
{
    public class RepositorioDeArquivoDeUnidadeDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeArquivoDeUnidadeDeDesenvolvimento(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }


        public void Criar(ArquivoDeUnidadeDeDesenvolvimento arquivoDeUnidadeDeDesenvolvimento)
        {
            _unitOfWork.ArquivoDeUnidadeDeDesenvolvimento.Add(arquivoDeUnidadeDeDesenvolvimento);
        }

        public void Atualizar(ArquivoDeUnidadeDeDesenvolvimento arquivoDeUnidadeDeDesenvolvimento)
        {
            var atual = Obter(arquivoDeUnidadeDeDesenvolvimento.IdArquivoUnidadeDesenvolvimento);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(arquivoDeUnidadeDeDesenvolvimento);
        }

        public void Apagar(ArquivoDeUnidadeDeDesenvolvimento arquivoDeUnidadeDeDesenvolvimento)
        {
            var atual = Obter(arquivoDeUnidadeDeDesenvolvimento.IdArquivoUnidadeDesenvolvimento);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(arquivoDeUnidadeDeDesenvolvimento);
        }

        public ArquivoDeUnidadeDeDesenvolvimento Obter(int idArquivoUnidadeDesenvolvimento)
        {
            return _unitOfWork.ArquivoDeUnidadeDeDesenvolvimento.SingleOrDefault(a => a.IdArquivoUnidadeDesenvolvimento == idArquivoUnidadeDesenvolvimento && a.IsDeleted == false);
        }

        public IQueryable<ArquivoDeUnidadeDeDesenvolvimento> Listar()
        {
            return _unitOfWork.ArquivoDeUnidadeDeDesenvolvimento.Where(a => a.IsDeleted == false);
        }
    }
}
