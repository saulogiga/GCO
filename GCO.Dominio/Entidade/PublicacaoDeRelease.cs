using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("PublicacaoRelease")]
    public class PublicacaoDeRelease
    {
        [Column("IdPublicacaoRelease"), Key]
        public Int64 IdPublicacaoRelease { get; set; }

        [Column("IdUnidadeDesenvolvimento"), ForeignKey("UnidadeDeDesenvolvimento")]
        public int IdUnidadeDesenvolvimento { get; set; }
        public virtual UnidadeDeDesenvolvimento UnidadeDeDesenvolvimento { get; set; }

        [Column("IdPublicacaoStatus"), ForeignKey("PublicacaoStatus")]
        public int IdPublicacaoStatus { get; set; }
        public virtual PublicacaoDeStatus PublicacaoStatus { get; set; }

        [Column("UserId"), ForeignKey("Usuario")]
        public int UserId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Column("DataInicioPublicacao")]
        public DateTime? DataInicioPublicacao { get; set; }

        [Column("DataFimPublicacao")]
        public DateTime? DataFimPublicacao { get; set; }

        [Column("CaminhoOrigem")]
        public string CaminhoOrigem { get; set; }

        [Column("PastaOrigem")]
        public string PastaOrigem { get; set; }

        [Column("CaminhoDestino")]
        public string CaminhoDestino { get; set; }

        [Column("ArqNaoSelecionados")]
        public string ArqNaoSelecionados { get; set; }

        [Column("DirNaoSelecionados")]
        public string DirNaoSelecionados { get; set; }

        [Column("Release")]
        public string Release { get; set; }

        [Column("Detalhes")]
        public string Detalhes { get; set; }


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
