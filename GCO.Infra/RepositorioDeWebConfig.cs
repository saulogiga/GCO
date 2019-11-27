using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Dominio.Entidade;
using System.Data.Entity;
using GCO.Dominio.Repositorio;

namespace GCO.Infra
{
    public class RepositorioDeWebConfig : IRepositorioDeWebConfig
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeWebConfig(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }


        public void Criar(WebConfig webConfig)
        {
            _unitOfWork.WebConfig.Add(webConfig);
        }

        public void Atualizar(WebConfig webConfig)
        {
            var atual = Obter(webConfig.IdWebConfig);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(webConfig);
        }

        public void Apagar(WebConfig webConfig)
        {
            var atual = Obter(webConfig.IdWebConfig);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(webConfig);
        }

        public WebConfig Obter(int idWebConfig)
        {
            return _unitOfWork.WebConfig.Include(s => s.UnidadeDesenvolvimento).Include(t => t.TipoDeWebConfig).Include(s => s.Usuario).Include(m => m.Modulo).SingleOrDefault(w => w.IdWebConfig == idWebConfig && w.IsDeleted == false);
        }

        public IQueryable<WebConfig> Listar()
        {
            return _unitOfWork.WebConfig.Include(s => s.UnidadeDesenvolvimento).Include(t => t.TipoDeWebConfig).Include(s => s.Usuario).Include(m => m.Modulo).Where(w => w.IsDeleted == false);
        }
    }
}
