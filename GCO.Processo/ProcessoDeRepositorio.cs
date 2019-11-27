using GCO.Dominio.Fabrica;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeRepositorio
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void Criar(Dominio.Entidade.Repositorio repositorio)
        {
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            var repositorioDeTipoDeRepositorio = new RepositorioDeTipoDeRepositorio(_unitOfWork);

            var tipo = repositorioDeTipoDeRepositorio.Obter(repositorio.IdTipoRepositorio);

            var _repositorio = FabricaDeRepositorio.Criar(repositorio.Nome, repositorio.Caminho, tipo);


            repositorioDeRepositorio.Criar(_repositorio);
            _unitOfWork.SaveChanges();
            _unitOfWork.Dispose();
            
        }

        public void Atualizar(Dominio.Entidade.Repositorio repositorio)
        {
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            var repositorioDeTipoDeRepositorio = new RepositorioDeTipoDeRepositorio(_unitOfWork);

            var tipo = repositorioDeTipoDeRepositorio.Obter(repositorio.IdTipoRepositorio);
            var _repositorio = repositorioDeRepositorio.Obter(repositorio.IdRepositorio);

            _repositorio.Nome = repositorio.Nome;
            _repositorio.Caminho = repositorio.Caminho;
            _repositorio.TipoRepositorio = tipo;
            _repositorio.IdTipoRepositorio = tipo.IdTipoRepositorio;

            repositorioDeRepositorio.Atualizar(_repositorio);
            _unitOfWork.SaveChanges();
            _unitOfWork.Dispose();
        }

        public void Apagar(Dominio.Entidade.Repositorio repositorio)
        {
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);

            var _repositorio = repositorioDeRepositorio.Obter(repositorio.IdRepositorio);

            repositorioDeRepositorio.Apagar(_repositorio);
            _unitOfWork.SaveChanges();
            _unitOfWork.Dispose();
        }

        public Dominio.Entidade.Repositorio Obter(int idRepositorio)
        {
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            return repositorioDeRepositorio.Obter(idRepositorio);
        }

        public IEnumerable<Dominio.Entidade.Repositorio> Listar()
        {
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            return repositorioDeRepositorio.Listar();
        }
    }
}
