using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GCO.Infra
{
    public class RepositorioDeProjeto : IRepositorioDeProjeto
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeProjeto(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(Projeto projeto)
        {
            _unitOfWork.Projeto.Add(projeto);
        }

        public void Atualizar(Projeto projeto)
        {
            var atual = Obter(projeto.IdProjeto);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(projeto);
        }

        public void Apagar(Projeto projeto)
        {
            var atual = Obter(projeto.IdProjeto);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(projeto);
        }

        public Projeto Obter(int idProjeto)
        {
            return _unitOfWork.Projeto.Include(p => p.Cliente).SingleOrDefault(p => p.IdProjeto == idProjeto && p.IsDeleted == false);
        }

        public IQueryable<Projeto> Listar()
        {
            return _unitOfWork.Projeto.Where(p => p.IsDeleted == false).Include(p => p.Cliente);
        }
    }
}
