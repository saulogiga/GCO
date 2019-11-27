using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("RamoDesenvolvimento")]
    public class RamoDeDesenvolvimento
    {
        [Column("IdRamoDesenvolvimento"), Key]
        public int IdRamoDesenvolvimento { get; set; }

        [Column("Caminho")]
        [StringLength(200)]
        public string Caminho { get; set; }

        [Column("Versao")]
        [StringLength(75)]
        public string Versao { get; set; }

        [Column("Comentario")]
        public string Comentario { get; set; }

        [Display(Name = "Ramo de Produção")]
        [Column("IdRamoProducao"), ForeignKey("RamoDeProducao")]
        public int? IdRamoProducao { get; set; }
        public virtual RamoDeProducao RamoDeProducao { get; set; }

        [Display(Name = "Repositório"), Required(ErrorMessage = "O campo Repositório é obrigatório")]
        [Column("IdRepositorio"), ForeignKey("Repositorio")]
        public int IdRepositorio { get; set; }
        public virtual Repositorio Repositorio { get; set; }

        [Column("IdUnidadeDesenvolvimento"), ForeignKey("UnidadeDeDesenvolvimento")]
        public int IdUnidadeDesenvolvimento { get; set; }
        public virtual UnidadeDeDesenvolvimento UnidadeDeDesenvolvimento { get; set; }

        [Display(Name = "Módulo"), Required(ErrorMessage = "O campo Módulo é obrigatório")]
        [Column("IdModulo"), ForeignKey("Modulo")]
        public int IdModulo { get; set; }
        public virtual Modulo Modulo { get; set; }

        [Column("Branch"), Required(ErrorMessage = "O campo Branch é obrigatório")]
        public bool Branch { get; set; }

        [Column("Fechado")]
        public bool Fechado { get; set; }

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
