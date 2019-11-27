using System.Collections.Generic;
using GCO.Dominio.Entidade;
using GCO.Infra;

namespace GCO.Processo
{
    public class ProcessoDeTipoDeArquivo
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public IEnumerable<TipoDeArquivo> Listar()
        {
            var repositorioDeTipoDeArquivo = new RepositorioDeTipoDeArquivo(_unitOfWork);
            return repositorioDeTipoDeArquivo.Listar();
        }

        public TipoDeArquivo Obter(TipoDeArquivo tipoDeArquivo)
        {
            var repositorioDeTipoDeArquivo = new RepositorioDeTipoDeArquivo(_unitOfWork);
            return repositorioDeTipoDeArquivo.Obter(tipoDeArquivo.IdTipoArquivo);
        }
    }
}
