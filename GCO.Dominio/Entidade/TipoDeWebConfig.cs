using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("TipoWebConfig")]
    public class TipoDeWebConfig
    {
        [Column("IdTipoWebConfig"), Key]
        public int IdTipoWebConfig { get; set; }

        [Column("Nome")]
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
