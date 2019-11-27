using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("webpages_Roles")]
    public class Perfil
    {
        [Column("RoleId"), Key]
        public int RoleId { get; set; }

        [Column("RoleName")]
        [Display(Name = "Nome do Perfil")]
        public string RoleName { get; set; }
    }
}
