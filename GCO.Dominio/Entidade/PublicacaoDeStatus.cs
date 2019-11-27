using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("PublicacaoStatus")]
    public class PublicacaoDeStatus
    {
        [Column("IdPublicacaoStatus"), Key]
        public int IdPublicacaoStatus { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }

        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }

        [Column("DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DataExclusao")]
        public DateTime? DataExclusao { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

    public enum EnumDeStatusDePublicacao
    {
        AGUARDANDOPUBLICAÇÃO = 1,
        PUBLICANDORELEASE = 2,
        ERROPUBLICAÇÃO = 3,
        PUBLICAÇÃOREALIZADACOMSUCESSO = 4
    }
}
