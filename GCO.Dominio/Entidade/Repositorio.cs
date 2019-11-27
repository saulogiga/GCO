using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("Repositorio")]
    public class Repositorio
    {
        [Column("IdRepositorio"), Key]
        public int IdRepositorio { get; set; }

        [Display(Name = "Nome"), Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(70)]
        [Column("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Caminho"), Required(ErrorMessage = "O campo Caminho é obrigatório")]
        [StringLength(200)]
        [Column("Caminho")]
        public string Caminho { get; set; }

        [Display(Name = "Tipo de Repositório"), Required(ErrorMessage = "O campo Tipo de Repositório é obrigatório")]
        [Column("IdTipoRepositorio"), ForeignKey("TipoRepositorio")]
        public int IdTipoRepositorio { get; set; }
        public virtual TipoDeRepositorio TipoRepositorio { get; set; }

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
