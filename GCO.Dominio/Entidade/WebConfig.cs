using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("WebConfig")]
    public class WebConfig
    {
        [Column("IdWebConfig"), Key]
        public int IdWebConfig { get; set; }

        [Column("Valor")]
        [Display(Name = "Valor"), Required(ErrorMessage = "O campo Valor é obrigatório")]
        public string Valor { get; set; }

        [Column("IdTipoWebConfig"), ForeignKey("TipoDeWebConfig")]
        [Display(Name = "Tipo de Tag"), Required(ErrorMessage = "O campo Tipo de Tag é obrigatório")]
        public int IdTipoWebConfig { get; set; }
        public virtual TipoDeWebConfig TipoDeWebConfig { get; set; }

        [Display(Name = "Unidade de Desenvolvimento")]
        [Column("IdUnidadeDesenvolvimento"), ForeignKey("UnidadeDesenvolvimento")]
        public int IdUnidadeDesenvolvimento { get; set; }
        public virtual UnidadeDeDesenvolvimento UnidadeDesenvolvimento { get; set; }

        [Display(Name = "Usuário")]
        [Column("UserId"), ForeignKey("Usuario")]
        public int UserId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Módulo"), Required(ErrorMessage = "O campo Módulo é obrigatório")]
        [Column("IdModulo"), ForeignKey("Modulo")]
        public int? IdModulo { get; set; }
        public virtual Modulo Modulo { get; set; }

        [Column("Acao"), Required(ErrorMessage = "O campo Acao é obrigatório")]
        public int? Acao { get; set; }

        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }

        [Column("DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DataExclusao")]
        public DateTime? DataExclusao { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
