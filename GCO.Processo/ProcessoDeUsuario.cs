using GCO.Dominio.Entidade;
using GCO.Infra;
using System.Collections.Generic;

namespace GCO.Processo
{
    public class ProcessoDeUsuario
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public IEnumerable<Usuario> Listar()
        {
            var repositorioDeUsuario = new RepositorioDeUsuario(_unitOfWork);
            return repositorioDeUsuario.Listar();
        }

        public Usuario Obter(Usuario usuario)
        {
            var repositorioDeUsuario = new RepositorioDeUsuario(_unitOfWork);
            return repositorioDeUsuario.Obter(usuario.UserId);
        }
    }
}
