using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("Cliente")]
    public class Cliente
    {
        [Column("IdCliente"), Key]
        public int IdCliente { get; set; }

        [Column("Nome")]
        [Display(Name = "Cliente"), Required(ErrorMessage = "Descrição do cliente é obrigatória")]
        public string Nome { get; set; }

        [Column("TeamProject")]
        public string TeamProject { get; set; }

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
