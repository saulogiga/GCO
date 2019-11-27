using GCO.Dominio.Entidade;
using GCO.Infra;
using System;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeUnidadeDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public UnidadeDeDesenvolvimento Criar(UnidadeDeDesenvolvimento unidade)
        {

            
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var repositorioDeUsuario = new RepositorioDeUsuario(_unitOfWork);
            var usuario = new List<Usuario>();

            if (unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado.GetHashCode() && unidade.DataPublicacao == null)
                throw new Exception("A data de publicação é obrigatória para finalizar a tarefa.");

            if (unidade.Desenvolvedores != null)
            {
                foreach (Usuario u in unidade.Desenvolvedores)
                {
                    usuario.Add(repositorioDeUsuario.Obter(u.UserId));
                }
                unidade.Desenvolvedores = usuario;
            }

            repositorioDeUnidadeDeDesenvolvimento.Criar(unidade);
            _unitOfWork.SaveChanges();
            //_unitOfWork.Dispose();

            return unidade;
        }

        public void Atualizar(UnidadeDeDesenvolvimento unidade)
        {
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var repositorioDeUsuario = new RepositorioDeUsuario(_unitOfWork);
            var usuario = new List<Usuario>();

            if (unidade.IdStatusUnidadeDesenvolvimento == EnumDeStatusDeUnidadeDeDesenvolvimento.Finalizado.GetHashCode() && unidade.DataPublicacao == null)
                throw new Exception("A data de publicação é obrigatória para finalizar a tarefa.");

            if (unidade.Desenvolvedores != null)
            {
                foreach (Usuario u in unidade.Desenvolvedores)
                {
                    usuario.Add(repositorioDeUsuario.Obter(u.UserId));
                }
                unidade.Desenvolvedores = usuario;
            }

            repositorioDeUnidadeDeDesenvolvimento.Atualizar(unidade);
            _unitOfWork.SaveChanges();
            //_unitOfWork.Dispose(); Deu merda na execução do script
        }

        public IEnumerable<UnidadeDeDesenvolvimento> Listar()
        {
            var repositorioUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            return repositorioUnidadeDeDesenvolvimento.Listar();
        }

        public UnidadeDeDesenvolvimento Obter(UnidadeDeDesenvolvimento unidade)
        {
            var repositorioUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            return repositorioUnidadeDeDesenvolvimento.Obter(unidade.IdUnidadeDesenvolvimento);
        }

        public void Deletar(UnidadeDeDesenvolvimento unidade)
        {
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            unidade = repositorioDeUnidadeDeDesenvolvimento.Obter(unidade.IdUnidadeDesenvolvimento);
            repositorioDeUnidadeDeDesenvolvimento.Apagar(unidade);
            _unitOfWork.SaveChanges();
            //_unitOfWork.Dispose();
        }
    }
}
