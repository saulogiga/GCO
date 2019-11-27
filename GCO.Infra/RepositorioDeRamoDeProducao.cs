using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GCO.Infra
{
    public class RepositorioDeRamoDeProducao : IRepositorioDeRamoDeProducao
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeRamoDeProducao(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(Dominio.Entidade.RamoDeProducao ramo)
        {
            _unitOfWork.RamoDeProducao.Add(ramo);
        }

        public void Atualizar(Dominio.Entidade.RamoDeProducao ramo)
        {
            var atual = Obter(ramo.IdRamoProducao);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(ramo);
        }

        public void Apagar(Dominio.Entidade.RamoDeProducao ramo)
        {
            var atual = Obter(ramo.IdRamoProducao);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(ramo);
        }

        public Dominio.Entidade.RamoDeProducao Obter(int idRamo)
        {
            return _unitOfWork.RamoDeProducao
                .Include(r => r.Projeto)
                .Include(r => r.Repositorio)
                .Include(r => r.Modulo)
                .SingleOrDefault(r => r.IdRamoProducao == idRamo && r.IsDeleted == false);
        }

        public IQueryable<Dominio.Entidade.RamoDeProducao> Listar()
        {
            return _unitOfWork.RamoDeProducao.Where(r => r.IsDeleted == false)
                .Include(r => r.Projeto)
                .Include(r => r.Modulo)
                .Include(r => r.Repositorio);
        }
    }
}
