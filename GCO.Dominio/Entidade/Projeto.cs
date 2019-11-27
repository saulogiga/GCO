using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("Projeto")]
    public class Projeto
    {
        [Column("IdProjeto"), Key]
        public int IdProjeto { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome"), Required(ErrorMessage = "Descrição do projeto é obrigatória")]
        public string Nome { get; set; }

        [Column("IdCliente"), ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }

        [Column("DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DataExclusao")]
        public DateTime? DataExclusao { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("IdProjetoPai")]
        public int? IdProjetoPai { get; set; }

        [Column("Ambiente")]
        public string Ambiente { get; set; }

        [Column("Versao")]
        [Display(Name = "Versão")]
        public string Versao { get; set; }
    }
}
