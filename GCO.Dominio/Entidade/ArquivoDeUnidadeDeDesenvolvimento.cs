using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("ArquivoUnidadeDesenvolvimento")]
    public class ArquivoDeUnidadeDeDesenvolvimento
    {
        [Column("IdArquivoUnidadeDesenvolvimento", Order = 0), Key]
        public int IdArquivoUnidadeDesenvolvimento { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome"), Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("Caminho")]
        [Display(Name = "Caminho"), Required(ErrorMessage = "O campo Caminho é obrigatório")]
        public string Caminho { get; set; }

        [Display(Name = "Tipo de Arquivo")]
        [Column("IdTipoArquivo", Order = 1), ForeignKey("TipoDeArquivo"), Key]
        public int IdTipoArquivo { get; set; }
        public virtual TipoDeArquivo TipoDeArquivo { get; set; }

        [Column("UserId", Order = 2), ForeignKey("Usuario"), Key]
        public int UserId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Column("IdUnidadeDesenvolvimento", Order = 3), ForeignKey("UnidadeDeDesenvolvimento"), Key]
        public int IdUnidadeDesenvolvimento { get; set; }
        public virtual UnidadeDeDesenvolvimento UnidadeDeDesenvolvimento { get; set; }

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
