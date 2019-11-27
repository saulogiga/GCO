using System;
using System.Collections.Generic;
using GCO.Dominio.Entidade;
using GCO.Infra;

namespace GCO.Processo
{
    public class ProcessoDeServidorDeBanco
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public bool ValidarBanco(int idServidorBanco, string nomeBanco)
        {
            try
            {
                var repositorioDeServidorDeBanco = new RepositorioDeServidorDeBanco(_unitOfWork);
                var servidorBanco = repositorioDeServidorDeBanco.Obter(idServidorBanco);

                var unitOfWorkOfScript = new UnitOfWorkOfScript(servidorBanco.StringConexao, nomeBanco);

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public IEnumerable<ServidorDeBanco> Listar()
        {
            var repositorioDeServidorDeBanco = new RepositorioDeServidorDeBanco(_unitOfWork);
            return repositorioDeServidorDeBanco.Listar();
        }

        public ServidorDeBanco Obter(ServidorDeBanco servidor)
        {
            var repositorioDeServidorDeBanco = new RepositorioDeServidorDeBanco(_unitOfWork);
            return repositorioDeServidorDeBanco.Obter(servidor.IdServidorBanco);
        }
    }
}
