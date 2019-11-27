using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System.Data.Entity;


namespace GCO.Infra
{
    public class RepositorioDePublicacaoDeRelease : IRepositorioDePublicacaoDeRelease
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDePublicacaoDeRelease(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(PublicacaoDeRelease publicacao)
        {
            _unitOfWork.PublicacaoRealse.Add(publicacao);
        }

        public void Atualizar(PublicacaoDeRelease publicacao)
        {
            var atual = Obter(publicacao.IdPublicacaoRelease);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(publicacao);
        }

        public void Apagar(PublicacaoDeRelease publicacao)
        {
            var atual = Obter(publicacao.IdPublicacaoRelease);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(publicacao);
        }

        public PublicacaoDeRelease Obter(Int64 idPublicacaoRealse)
        {
            _unitOfWork.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");

            return _unitOfWork.PublicacaoRealse
                              .Include(u => u.UnidadeDeDesenvolvimento)
                              .Include(u => u.Usuario)
                              .Include(u => u.PublicacaoStatus).SingleOrDefault(p => p.IdPublicacaoRelease == idPublicacaoRealse);
        }

        public IQueryable<PublicacaoDeRelease> Listar()
        {
            _unitOfWork.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");

            return _unitOfWork.PublicacaoRealse.Where(p => p.IsDeleted == false)
                              .Include(u => u.UnidadeDeDesenvolvimento)
                              .Include(u => u.Usuario)
                              .Include(u => u.PublicacaoStatus);
        }
    }
}
