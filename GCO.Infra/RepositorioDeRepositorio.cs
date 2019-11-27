using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GCO.Infra
{
    public class RepositorioDeRepositorio : IRepositorioDeRepositorio
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeRepositorio(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(Dominio.Entidade.Repositorio repositorio)
        {
            _unitOfWork.Repositorio.Add(repositorio);
        }

        public void Atualizar(Dominio.Entidade.Repositorio repositorio)
        {
            var atual = Obter(repositorio.IdRepositorio);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(repositorio);
        }

        public void Apagar(Dominio.Entidade.Repositorio repositorio)
        {
            var atual = Obter(repositorio.IdRepositorio);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(repositorio);
        }

        public Dominio.Entidade.Repositorio Obter(int idrepositorio)
        {
            return _unitOfWork.Repositorio.Include(r => r.TipoRepositorio).SingleOrDefault(r => r.IdRepositorio == idrepositorio && r.IsDeleted == false);
        }

        public IQueryable<Dominio.Entidade.Repositorio> Listar()
        {
            return _unitOfWork.Repositorio.Where(r => r.IsDeleted == false).Include(r => r.TipoRepositorio);
        }
    }
}
