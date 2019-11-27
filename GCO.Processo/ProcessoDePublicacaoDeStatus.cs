using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Linq;

namespace GCO.Processo
{
    public class ProcessoDePublicacaoDeStatus
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public PublicacaoDeStatus Obter(int idPublicacaoDeStatus)
        {
            var repositorioDePublicacaoDeStatus = new RepositorioDePublicacaoDeStatus(_unitOfWork);
            return repositorioDePublicacaoDeStatus.Obter(idPublicacaoDeStatus);
        }

        public IQueryable<PublicacaoDeStatus> Listar()
        {
            var repositorioDePublicacaoDeStatus = new RepositorioDePublicacaoDeStatus(_unitOfWork);
            return repositorioDePublicacaoDeStatus.Listar();
        }
    }
}
