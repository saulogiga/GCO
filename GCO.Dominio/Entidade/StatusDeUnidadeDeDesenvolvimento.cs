using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("StatusUnidadeDesenvolvimento")]
    public class StatusDeUnidadeDeDesenvolvimento
    {
        [Column("IdStatusUnidadeDesenvolvimento"), Key]
        public int IdStatusUnidadeDesenvolvimento { get; set; }

        [Column("Nome")]
        [Display(Name = "Status da Unidade de Desenvolvimento"), Required(ErrorMessage = "Nome do Status da Unidade de Desenvolvimento é obrigatória")]
        public string Nome { get; set; }

        [Column("DataInclusao")]
        public DateTime DataInclusao { get; set; }

        [Column("DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DataExclusao")]
        public DateTime? DataExclusao { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }


    public enum EnumDeStatusDeUnidadeDeDesenvolvimento
    {
        Pendente	=   1,
        EmDesenvolvimento	= 2,
        Homologação	= 3,
        AguardandoHomologacaoDeProdutos	= 4,
        AguardandoPublicacaoEmProducao	= 5,
        Finalizado	= 6
    }
}
