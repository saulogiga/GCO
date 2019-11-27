using GCO.Dominio.Entidade;
using GCO.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GCO.Infra
{
    public class RepositorioDeUnidadeDeDesenvolvimento : IRepositorioDeUnidadeDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork;
        public RepositorioDeUnidadeDeDesenvolvimento(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Criar(UnidadeDeDesenvolvimento unidade)
        {
            _unitOfWork.UnidadeDeDesenvolvimento.Add(unidade);
        }

        public void Atualizar(UnidadeDeDesenvolvimento unidade)
        {
            var atual = Obter(unidade.IdUnidadeDesenvolvimento);
            atual.DataAlteracao = DateTime.Now;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(unidade);
        }

        public void Apagar(UnidadeDeDesenvolvimento unidade)
        {
            var atual = Obter(unidade.IdUnidadeDesenvolvimento);
            atual.DataExclusao = DateTime.Now;
            atual.IsDeleted = true;
            _unitOfWork.Entry(atual).CurrentValues.SetValues(unidade);
        }

        public UnidadeDeDesenvolvimento Obter(int idUnidade)
        {
            _unitOfWork.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");

            return _unitOfWork.UnidadeDeDesenvolvimento.Include(u => u.Projeto)
                .Include(u => u.Projeto)
                .Include(u => u.Projeto.Cliente)
                .Include(u => u.Solicitante)
                .Include(u => u.StatusUnidadeDesenvolvimento)
                .Include(u => u.TipoDeUnidadeDeDesenvolvimento)
                .Include(u => u.Scripts.Select(s => s.TipoDeScriptDeBanco))
                .Include(u => u.Scripts.Select(s => s.Usuario))
                .Include(u => u.Desenvolvedores)
                .Include(u => u.UnidadeDeDesenvolvimentoDeBanco)
                .Include(u => u.UnidadeDeDesenvolvimentoDeBanco.Select(b => b.ServidorDeBanco))
                .Include(u => u.UnidadeDeDesenvolvimentoDeBanco.Select(b => b.TipoDeServidor))
                .Include(u => u.RamoDeDesenvolvimento.Select(r => r.Repositorio))
                .Include(u => u.RamoDeDesenvolvimento.Select(r => r.Repositorio.TipoRepositorio))
                .Include(u => u.RamoDeDesenvolvimento.Select(r => r.RamoDeProducao))
                .Include(u => u.RamoDeDesenvolvimento.Select(r => r.Modulo))
                .Include(u => u.WebConfig)
                .Include(u => u.WebConfig.Select(w => w.TipoDeWebConfig))
                .Include(u => u.WebConfig.Select(w => w.Modulo))
                .Include(u => u.ArquivoDeUnidadeDeDesenvolvimento)
                .SingleOrDefault(m => m.IdUnidadeDesenvolvimento == idUnidade && m.IsDeleted == false);
        }

        public IQueryable<UnidadeDeDesenvolvimento> Listar()
        {
            _unitOfWork.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");

            return _unitOfWork.UnidadeDeDesenvolvimento.Where(m => m.IsDeleted == false)
                .Include(u => u.Projeto)
                .Include(u => u.Projeto.Cliente)
                .Include(u => u.Solicitante)
                .Include(u => u.StatusUnidadeDesenvolvimento)
                .Include(u => u.TipoDeUnidadeDeDesenvolvimento)
                .Include(u => u.Scripts.Select(s => s.TipoDeScriptDeBanco))
                .Include(u => u.Scripts.Select(s => s.Usuario))
                .Include(u => u.Desenvolvedores)
                .Include(u => u.UnidadeDeDesenvolvimentoDeBanco)
                .Include(u => u.UnidadeDeDesenvolvimentoDeBanco.Select(b => b.ServidorDeBanco))
                .Include(u => u.UnidadeDeDesenvolvimentoDeBanco.Select(b => b.TipoDeServidor))
                .Include(u => u.RamoDeDesenvolvimento.Select(r => r.Repositorio))
                .Include(u => u.RamoDeDesenvolvimento.Select(r => r.RamoDeProducao))
                .Include(u => u.RamoDeDesenvolvimento.Select(r => r.Modulo))
                .Include(u => u.WebConfig)
                .Include(u => u.WebConfig.Select(w => w.TipoDeWebConfig))
                .Include(u => u.WebConfig.Select(w => w.Modulo))
                .Include(u => u.ArquivoDeUnidadeDeDesenvolvimento);
        }
    }
}
