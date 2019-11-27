using System.Collections.Generic;
using GCO.Dominio.Entidade;
using GCO.Infra;

namespace GCO.Processo
{
    public class ProcessoDeTipoDeWebConfig
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public IEnumerable<TipoDeWebConfig> Listar()
        {
            var repositorioDeTipoDeWebConfig = new RepositorioDeTipoDeWebConfig(_unitOfWork);
            return repositorioDeTipoDeWebConfig.Listar();
        }

        public TipoDeWebConfig Obter(TipoDeWebConfig tipoDeWebConfig)
        {
            var repositorioDeTipoDeWebConfig = new RepositorioDeTipoDeWebConfig(_unitOfWork);
            return repositorioDeTipoDeWebConfig.Obter(tipoDeWebConfig.IdTipoWebConfig);
        }
    }
}
