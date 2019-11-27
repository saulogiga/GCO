using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GCO.Infra
{
    public class RepositorioDeRamoDeDesenvolvimento : IRepositorioDeRamoDeDesenvolvimento
    {

        private UnitOfWork _unitOfWork;
        public RepositorioDeRamoDeDesenvolvimento(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(Dominio.Entidade.RamoDeDesenvolvimento ramo)
        {
            _unitOfWork.RamoDeDesenvolvimento.Add(ramo);
        }

        public void Atualizar(Dominio.Entidade.RamoDeDesenvolvimento ramo)
        {
            var atual = Obter(ramo.IdRamoDesenvolvimento);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(ramo);
        }

        public void Apagar(Dominio.Entidade.RamoDeDesenvolvimento ramo)
        {
            var atual = Obter(ramo.IdRamoDesenvolvimento);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(ramo);
        }

        public Dominio.Entidade.RamoDeDesenvolvimento Obter(int idRamo)
        {
            return _unitOfWork.RamoDeDesenvolvimento
                .Include(r => r.RamoDeProducao)
                .Include(r => r.Repositorio)
                .Include(r => r.UnidadeDeDesenvolvimento)
                .Include(r => r.UnidadeDeDesenvolvimento.Projeto)
                .Include(r => r.Modulo)
                .SingleOrDefault(r => r.IdRamoDesenvolvimento == idRamo && r.IsDeleted == false);
        }

        public IQueryable<Dominio.Entidade.RamoDeDesenvolvimento> Listar()
        {
            return _unitOfWork.RamoDeDesenvolvimento.Where(r => r.IsDeleted == false)
                .Include(r => r.RamoDeProducao)
                .Include(r => r.Repositorio)
                .Include(r => r.Modulo)
                .Include(r => r.UnidadeDeDesenvolvimento)
                .Include(r => r.UnidadeDeDesenvolvimento.Projeto);
        }
    }
}
