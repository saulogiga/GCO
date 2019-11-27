using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.Composition;
using GCO.Dominio.Entidade;
using GCO.Dominio;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Migrations;

namespace GCO.Infra
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {

        public UnitOfWork()
            : base("name=UnitOfWork")
        {
            Configuration.LazyLoadingEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UnidadeDeDesenvolvimento>()
                .HasMany(u => u.Desenvolvedores)
                .WithMany(ud => ud.UnidadesDeDesenvolvimentos)
                .Map(m => m.ToTable("UnidadeDesenvolvimentoDesenvolvedor")
                    .MapLeftKey("IdUnidadeDesenvolvimento")
                    .MapRightKey("UserId"));

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Modulo> Modulo { get; set; }
        public IDbSet<TipoDeServidor> TipoDeServidor { get; set; }
        public IDbSet<TipoDeUnidadeDeDesenvolvimento> TipoDeUnidadeDeDesenvolvimento { get; set; }
        public IDbSet<StatusDeUnidadeDeDesenvolvimento> StatusDeUnidadeDeDesenvolvimento { get; set; }
        public IDbSet<Cliente> Cliente { get; set; }
        public IDbSet<Projeto> Projeto { get; set; }
        public IDbSet<Perfil> Perfil { get; set; }
        public IDbSet<Usuario> Usuario { get; set; }
        public IDbSet<UnidadeDeDesenvolvimento> UnidadeDeDesenvolvimento { get; set; }
        public IDbSet<TipoDeScriptDeBanco> TipoDeScriptDeBanco { get; set; }
        public IDbSet<ScriptDeBanco> ScriptDeBanco { get; set; }
        public IDbSet<ServidorDeBanco> ServidorDeBanco { get; set; }
        public IDbSet<TipoDeWebConfig> TipoDeWebConfig { get; set; }
        public IDbSet<TipoDeArquivo> TipoDeArquivo { get; set; }
        public IDbSet<WebConfig> WebConfig { get; set; }
        public IDbSet<ArquivoDeUnidadeDeDesenvolvimento> ArquivoDeUnidadeDeDesenvolvimento { get; set; }
        public IDbSet<TipoDeRepositorio> TipoDeRepositorio { get; set; }
        public IDbSet<Repositorio> Repositorio { get; set; }
        public IDbSet<RamoDeDesenvolvimento> RamoDeDesenvolvimento { get; set; }
        public IDbSet<RamoDeProducao> RamoDeProducao { get; set; }
        public IDbSet<PublicacaoDeStatus> PublicacaoStatus { get; set; }
        public IDbSet<PublicacaoDeRelease> PublicacaoRealse { get; set; }

    }

}
