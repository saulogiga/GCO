using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("ScriptBanco")]
    public class ScriptDeBanco
    {
        [Column("IdScript"), Key]
        public Int64 IdScript { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome do Script"), Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("Comentario")]
        [Display(Name = "Comentário do Script")]
        public string Comentario { get; set; }

        [Column("Script")]
        [Display(Name = "Script de Banco"), Required(ErrorMessage = "O Script de banco é obrigatório")]
        public string Script { get; set; }

        [Display(Name = "Executado")]
        [Column("Executado")]
        public bool Executado { get; set; }

        [Display(Name = "Unidade de Desenvolvimento")]
        [Column("IdUnidadeDesenvolvimento"), ForeignKey("UnidadeDesenvolvimento")]
        public int IdUnidadeDesenvolvimento { get; set; }
        public virtual UnidadeDeDesenvolvimento UnidadeDesenvolvimento { get; set; }

        [Display(Name = "Tipo de Script")]
        [Column("IdTipoScript"), ForeignKey("TipoDeScriptDeBanco")]
        public int IdTipoScript { get; set; }
        public virtual TipoDeScriptDeBanco TipoDeScriptDeBanco { get; set; }

        [Display(Name = "Usuário")]
        [Column("UserId"), ForeignKey("Usuario")]
        public int UserId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Ordem")]
        [Column("Ordem")]
        public int Ordem { get; set; }

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
