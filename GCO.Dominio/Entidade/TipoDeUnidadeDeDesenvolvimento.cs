using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("TipoUnidadeDesenvolvimento")]
    public class TipoDeUnidadeDeDesenvolvimento
    {
        [Column("IdTipoUnidadeDesenvolvimento"), Key]
        public int IdTipoUnidadeDesenvolvimento { get; set; }

        [Column("Nome")]
        [Display(Name = "Tipo de Unidade de Desenvolvimento"), Required(ErrorMessage = "Descrição da unidade é obrigatória")]
        public string Nome { get; set; }

        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }

        [Column("DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DataExclusao")]
        public DateTime? DataExclusao { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

    public enum EnumDeTipoDeUnidadeDeDesenvolvimento
    {
        Desenvolvimento = 1,
        Bug = 2
    }
}
