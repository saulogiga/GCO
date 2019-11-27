using System.Collections.Generic;
using GCO.Dominio.Entidade;
using GCO.Infra;

namespace GCO.Processo
{
    public class ProcessoDeModulo
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public Modulo Criar(Modulo modulo)
        {
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);

            repositorioDeModulo.Criar(modulo);
            _unitOfWork.SaveChanges();
            return modulo;
        }

        public void Atualizar(Modulo modulo)
        {
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);

            repositorioDeModulo.Atualizar(modulo);
            _unitOfWork.SaveChanges();
        }
        
        public IEnumerable<Modulo> Listar()
        {
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);
            return repositorioDeModulo.Listar();
        }

        public Modulo Obter(Modulo modulo)
        {
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);
            return repositorioDeModulo.Obter(modulo.IdModulo);
        }
    }
}
