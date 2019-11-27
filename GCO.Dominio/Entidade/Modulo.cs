using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("Modulo")]
    public class Modulo
    {
        [Column("IdModulo"), Key]
        public int IdModulo { get; set; }

        [Column("Nome")]
        [Display(Name="Módulo"), Required(ErrorMessage="Descrição do módulo é obrigatória")]
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
}
