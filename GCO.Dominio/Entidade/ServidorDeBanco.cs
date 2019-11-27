using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("ServidorBanco")]
    public class ServidorDeBanco
    {
        [Column("IdServidorBanco"), Key]
        public int IdServidorBanco { get; set; }

        [Column("StringConexao")]
        public string StringConexao { get; set; }

        [Column("NomeServidor")]
        public string NomeServidor { get; set; }

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
