using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeTipoDeRepositorio
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public IEnumerable<TipoDeRepositorio> Listar()
        {
            var repositorioDeTipoDeRepositorio = new RepositorioDeTipoDeRepositorio(_unitOfWork);
            return repositorioDeTipoDeRepositorio.Listar();
        }

        public TipoDeRepositorio Obter(int idTipoDeRepositorio)
        {
            var repositorioDeTipoDeRepositorio = new RepositorioDeTipoDeRepositorio(_unitOfWork);
            return repositorioDeTipoDeRepositorio.Obter(idTipoDeRepositorio);
        }
    }
}
