using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeProjeto
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public Projeto Criar(Projeto projeto)
        {
            var repositorioDeProjeto = new RepositorioDeProjeto(_unitOfWork);

            repositorioDeProjeto.Criar(projeto);
            _unitOfWork.SaveChanges();
            return projeto;
        }

        public void Atualizar(Projeto projeto)
        {
            var repositorioDeProjeto = new RepositorioDeProjeto(_unitOfWork);

            repositorioDeProjeto.Atualizar(projeto);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Projeto> Listar()
        {
            var repositorioDeProjeto = new RepositorioDeProjeto(_unitOfWork);
            return repositorioDeProjeto.Listar();
        }

        public Projeto Obter(Projeto projeto)
        {
            var repositorioDeProjeto = new RepositorioDeProjeto(_unitOfWork);
            return repositorioDeProjeto.Obter(projeto.IdProjeto);
        }
    }
}
