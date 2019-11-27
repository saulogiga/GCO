using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("RamoProducao")]
    public class RamoDeProducao
    {
        [Column("IdRamoProducao"), Key]
        public int IdRamoProducao { get; set; }

        [Display(Name = "Caminho")]
        [Column("Caminho")]
        [StringLength(200)]
        public string Caminho { get; set; }

        [Display(Name = "Versão"), Required(ErrorMessage = "O campo Versão é obrigatório")]
        [Column("Versao")]
        [StringLength(75)]
        public string Versao { get; set; }

        [Display(Name = "Pacote de Valor")]
        [Column("Solicitacao")]
        [StringLength(50)]
        public string Solicitacao { get; set; }

        [Display(Name = "Comentário")]
        [Column("Comentario")]
        [StringLength(500)]
        public string Comentario { get; set; }

        [Display(Name = "Data de Publicação"), Required(ErrorMessage = "O campo Data de Publicação é obrigatório")]
        [Column("DataPublicacao")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime DataPublicacao { get; set; }

        [Display(Name = "Repositório"), Required(ErrorMessage = "O campo Repositório é obrigatório")]
        [Column("IdRepositorio"), ForeignKey("Repositorio")]
        public int IdRepositorio { get; set; }
        public virtual Repositorio Repositorio { get; set; }

        [Display(Name = "Projeto"), Required(ErrorMessage = "O campo Projeto é obrigatório")]
        [Column("IdProjeto"), ForeignKey("Projeto")]
        public int IdProjeto { get; set; }
        public virtual Projeto Projeto { get; set; }

        [Display(Name = "Módulo"), Required(ErrorMessage = "O campo Módulo é obrigatório")]
        [Column("IdModulo"), ForeignKey("Modulo")]
        public int IdModulo { get; set; }
        public virtual Modulo Modulo { get; set; }

        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }

        [Column("DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DataExclusao")]
        public DateTime? DataExclusao { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        public string Ramo
        {
            get
            {
                if (Modulo != null)
                {
                    return "Módulo: " + Modulo.Nome + " | Versão: " + Versao + " | Caminho: " + Caminho;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
