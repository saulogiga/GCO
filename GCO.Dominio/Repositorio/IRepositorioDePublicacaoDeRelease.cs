using GCO.Dominio.Entidade;
using System;
using System.Linq;

namespace GCO.Dominio.Repositorio
{
    public interface IRepositorioDePublicacaoDeRelease
    {
        void Criar(PublicacaoDeRelease publicacao);

        void Atualizar(PublicacaoDeRelease publicacao);

        void Apagar(PublicacaoDeRelease publicacao);

        PublicacaoDeRelease Obter(Int64 idPublicacaoRealse);

        IQueryable<PublicacaoDeRelease> Listar();
    }
}
