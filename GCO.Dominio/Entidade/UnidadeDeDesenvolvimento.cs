using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("UnidadeDesenvolvimento")]
    public class UnidadeDeDesenvolvimento
    {
        [Column("IdUnidadeDesenvolvimento"), Key]
        public int IdUnidadeDesenvolvimento { get; set; }

        [Column("NumeroTicket")]
        [Display(Name = "Pacote de Valor"), Required(ErrorMessage = "O campo Pacote de Valor é obrigatória")]
        public string NumeroTicket { get; set; }

        [Column("NumeroSolicitacao")]
        [Display(Name = "Entregáveis")]
        [StringLength(50)]
        public string NumeroSolicitacao { get; set; }

        [Column("DescricaoModulos")]
        public string DescricaoModulos { get; set; }
        

        [Column("ExecutantoScript")]
        public bool ExecutantoScript { get; set; }

        [Column("DataPublicacao")]
        [Display(Name = "Data de Publicação")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? DataPublicacao { get; set; }

        [Display(Name = "Solicitante")]
        [Column("IdSolicitante"), ForeignKey("Solicitante")]
        public int IdSolicitante { get; set; }
        public virtual Usuario Solicitante { get; set; }

        [Display(Name = "Status da Unidade de Desenvolvimento")]
        [Column("IdStatusUnidadeDesenvolvimento"), ForeignKey("StatusUnidadeDesenvolvimento")]

        public int IdStatusUnidadeDesenvolvimento { get; set; }
        public virtual StatusDeUnidadeDeDesenvolvimento StatusUnidadeDesenvolvimento { get; set; }

        [Display(Name = "Tipo da Unidade de Desenvolvimento")]
        [Column("IdTipoUnidadeDesenvolvimento"), ForeignKey("TipoDeUnidadeDeDesenvolvimento")]
        public int IdTipoUnidadeDesenvolvimento { get; set; }
        public virtual TipoDeUnidadeDeDesenvolvimento TipoDeUnidadeDeDesenvolvimento { get; set; }

        [Display(Name = "Projeto")]
        [Column("IdProjeto"), ForeignKey("Projeto")]
        public int IdProjeto { get; set; }
        public virtual Projeto Projeto { get; set; }

        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }

        [Column("DataAlteracao")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DataExclusao")]
        public DateTime? DataExclusao { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Usuario> Desenvolvedores { get; set; }

        public virtual ICollection<ScriptDeBanco> Scripts { get; set; }

        public virtual ICollection<UnidadeDeDesenvolvimentoDeBanco> UnidadeDeDesenvolvimentoDeBanco { get; set; }

        public virtual ICollection<WebConfig> WebConfig { get; set; }

        public virtual ICollection<ArquivoDeUnidadeDeDesenvolvimento> ArquivoDeUnidadeDeDesenvolvimento { get; set; }

        public virtual ICollection<RamoDeDesenvolvimento> RamoDeDesenvolvimento { get; set; }

        [Column("CaminhoBuild")]
        [Display(Name = "Caminho do Build")]
        [StringLength(500)]
        public string CaminhoBuild { get; set; }

        [Column("NomeBuild")]
        [Display(Name = "Nome do Build")]
        [StringLength(15)]
        public string NomeBuild { get; set; }

        [Column("CaminhoPublish")]
        [Display(Name = "Caminho do Publish")]
        [StringLength(500)]
        public string CaminhoPublish { get; set; }

        [Column("TeamProject")]
        [StringLength(50)]
        [Display(Name = "Team Project"), Required(ErrorMessage = "O campo Team Project é obrigatório")]
        public string TeamProject { get; set; }

        
    }
}
