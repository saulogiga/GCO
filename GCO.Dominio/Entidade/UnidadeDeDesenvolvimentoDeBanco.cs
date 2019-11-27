using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCO.Dominio.Entidade
{
    [Table("UnidadeDesenvolvimentoBanco")]
    public class UnidadeDeDesenvolvimentoDeBanco
    {
        [Column("IdUnidadeDesenvolvimento", Order = 0), ForeignKey("UnidadeDeDesenvolvimento"),Key]
        public int IdUnidadeDesenvolvimento { get; set; }
        public virtual UnidadeDeDesenvolvimento UnidadeDeDesenvolvimento { get; set; }

        [Column("IdServidorBanco", Order = 1), ForeignKey("ServidorDeBanco"),Key]
        public int IdServidorBanco { get; set; }
        public virtual ServidorDeBanco ServidorDeBanco { get; set; }

        [Column("IdTipoServidor", Order = 2), ForeignKey("TipoDeServidor"),Key]
        public int IdTipoServidor { get; set; }
        public virtual TipoDeServidor TipoDeServidor { get; set; }

        [Column("NomeBanco",Order = 3), Key]
        public string NomeBanco { get; set; }
    }
}
