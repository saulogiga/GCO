using System;
using GCO.Dominio.Entidade;

namespace GCO.Dominio.Fabrica
{
    public class FabricaDeWebConfig
    {
        public static WebConfig Criar(string valor, int idTipoWebConfig,int idModulo, int acao ,int idUnidadeDesenvolvimento, int userId)
        {
            return new WebConfig { Valor = valor, IdTipoWebConfig = idTipoWebConfig,IdModulo = idModulo, Acao = acao, IdUnidadeDesenvolvimento = idUnidadeDesenvolvimento, UserId = userId, DataInclusao = DateTime.Now, IsDeleted = false };
        }
    }
}
