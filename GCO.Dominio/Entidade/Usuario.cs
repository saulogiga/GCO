using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("UserProfile")]
    public class Usuario
    {
        [Column("UserId"), Key]
        public int UserId { get; set; }

        [Column("UserName")]
        [Display(Name = "Nome do Usuário")]
        public string UserName { get; set; }

        public virtual ICollection<UnidadeDeDesenvolvimento> UnidadesDeDesenvolvimentos { get; set; }

    }
}
