using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("TipoServidor")]
    public class TipoDeServidor
    {
        [Column("IdTipoServidor"), Key]
        public int IdTipoServidor { get; set; }

        [Column("Nome")]
        [Display(Name = "Tipo Servidor")]
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
    public enum EnumDeTipoDeServidor
    {
        HomologacaoDeBanco = 1,
        HomologacaoDeAplicacao = 2,
        ProducaoDeBanco = 3,
        ProducaoDeAplicacao = 4
    }

}
