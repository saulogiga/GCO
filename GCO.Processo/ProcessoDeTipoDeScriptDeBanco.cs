using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeTipoDeScriptDeBanco
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public IEnumerable<TipoDeScriptDeBanco > Listar()
        {
            var repositorioDeTipoDeScriptDeBanco = new RepositorioDeTipoDeScriptDeBanco(_unitOfWork);
            return repositorioDeTipoDeScriptDeBanco.Listar();
        }

        public TipoDeScriptDeBanco Obter(TipoDeScriptDeBanco tipo)
        {
            var repositorioDeTipoDeScriptDeBanco = new RepositorioDeTipoDeScriptDeBanco(_unitOfWork);
            return repositorioDeTipoDeScriptDeBanco.Obter(tipo.IdTipoScript);
        }
    }
}
