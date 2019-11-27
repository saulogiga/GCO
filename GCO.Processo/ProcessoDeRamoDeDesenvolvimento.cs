using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using GCO.Infra;
using System.Collections.Generic;
using System.Linq;

namespace GCO.Processo
{
    public class ProcessoDeRamoDeDesenvolvimento
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public void Criar(RamoDeDesenvolvimento ramo)
        {
            var repositorioDeRamoDeDesenvolvimento = new RepositorioDeRamoDeDesenvolvimento(_unitOfWork);
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);

            var ramoDeDesenvolvimento = new RamoDeDesenvolvimento();

            var repositorio = repositorioDeRepositorio.Obter(ramo.IdRepositorio);
            var unidadeDeDesenvolvimento = repositorioDeUnidadeDeDesenvolvimento.Obter(ramo.IdUnidadeDesenvolvimento);
            var modulo = repositorioDeModulo.Obter(ramo.IdModulo);

            //Gerar Caminho

            if (ramo.Branch) { ramo.Versao = string.Empty; } else { ramo.Caminho = string.Empty; }
            if (!ramo.Branch) { ramo.Caminho = GerarCaminho(ramo.IdRepositorio, unidadeDeDesenvolvimento.IdProjeto, ramo.Branch) + ramo.Caminho; }

            if (ramo.IdRamoProducao != null)
            {
                var ramoDeProducao = repositorioDeRamoDeProducao.Obter(ramo.IdRamoProducao.Value);
                ramoDeDesenvolvimento = FabricaDeRamoDeDesenvolvimento.Criar(ramo.Caminho, ramo.Comentario, ramo.Versao, ramo.Branch, repositorio, unidadeDeDesenvolvimento, ramoDeProducao, modulo);
            }
            else {
                ramoDeDesenvolvimento = FabricaDeRamoDeDesenvolvimento.Criar(ramo.Caminho, ramo.Comentario, ramo.Versao, ramo.Branch, repositorio, unidadeDeDesenvolvimento, modulo);
            }

            repositorioDeRamoDeDesenvolvimento.Criar(ramoDeDesenvolvimento);
            _unitOfWork.SaveChanges();
        }

        public void Atualizar(RamoDeDesenvolvimento ramo)
        {
            var repositorioDeRamoDeDesenvolvimento = new RepositorioDeRamoDeDesenvolvimento(_unitOfWork);
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            var repositorioDeUnidadeDeDesenvolvimento = new RepositorioDeUnidadeDeDesenvolvimento(_unitOfWork);
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);
            var ramoDeDesenvolvimento = repositorioDeRamoDeDesenvolvimento.Obter(ramo.IdRamoDesenvolvimento);

            
            ramoDeDesenvolvimento.Comentario = ramo.Comentario;
            ramoDeDesenvolvimento.Versao = ramo.Versao;
            ramoDeDesenvolvimento.Branch = ramo.Branch;

            if (ramoDeDesenvolvimento.IdModulo != ramo.IdModulo)
            {
                ramoDeDesenvolvimento.Modulo = repositorioDeModulo.Obter(ramo.IdModulo);
                ramoDeDesenvolvimento.IdModulo = ramoDeDesenvolvimento.Modulo.IdModulo;
            }

            if (ramo.IdRamoProducao != null && ramoDeDesenvolvimento.IdRamoProducao != ramo.IdRamoProducao)
            {
                ramoDeDesenvolvimento.RamoDeProducao = repositorioDeRamoDeProducao.Obter(ramo.IdRamoProducao.Value);
                ramoDeDesenvolvimento.IdRamoProducao = ramoDeDesenvolvimento.RamoDeProducao.IdRamoProducao;
            }

            if (ramoDeDesenvolvimento.IdRepositorio != ramo.IdRepositorio)
            {
                ramoDeDesenvolvimento.Repositorio = repositorioDeRepositorio.Obter(ramo.IdRepositorio);
                ramoDeDesenvolvimento.IdRepositorio = ramoDeDesenvolvimento.Repositorio.IdRepositorio;
            }

            if (ramoDeDesenvolvimento.IdUnidadeDesenvolvimento != ramo.IdUnidadeDesenvolvimento)
            {
                ramoDeDesenvolvimento.UnidadeDeDesenvolvimento = repositorioDeUnidadeDeDesenvolvimento.Obter(ramo.IdUnidadeDesenvolvimento);
                ramoDeDesenvolvimento.IdUnidadeDesenvolvimento = ramoDeDesenvolvimento.UnidadeDeDesenvolvimento.IdUnidadeDesenvolvimento;
            }

            if (ramo.Branch) { ramo.Versao = string.Empty; } else { ramo.Caminho = string.Empty; }

            if (!ramo.Branch) { ramoDeDesenvolvimento.Caminho = GerarCaminho(ramo.IdRepositorio, ramoDeDesenvolvimento.UnidadeDeDesenvolvimento.IdProjeto, ramo.Branch) + ramo.Caminho; }
            else
            {
                ramoDeDesenvolvimento.Caminho = ramo.Caminho;
            }


            repositorioDeRamoDeDesenvolvimento.Atualizar(ramoDeDesenvolvimento);
            _unitOfWork.SaveChanges();
        }

        public void Apagar(RamoDeDesenvolvimento ramo)
        {
            var repositorioDeRamoDeDesenvolvimento = new RepositorioDeRamoDeDesenvolvimento(_unitOfWork);
            repositorioDeRamoDeDesenvolvimento.Apagar(ramo);
            _unitOfWork.SaveChanges();
        }

        public RamoDeDesenvolvimento Obter(int idRamo)
        {
            var repositorioDeRamoDeDesenvolvimento = new RepositorioDeRamoDeDesenvolvimento(_unitOfWork);
            return repositorioDeRamoDeDesenvolvimento.Obter(idRamo);
        }

        public IEnumerable<RamoDeDesenvolvimento> Listar()
        {
            var repositorioDeRamoDeDesenvolvimento = new RepositorioDeRamoDeDesenvolvimento(_unitOfWork);
            return repositorioDeRamoDeDesenvolvimento.Listar();
        }

        public void FecharRamoDeDesenvolvimento(int idRamoDesenvolvimento, string versaoDeProducao)
        {
            var repositorioDeRamoDeDesenvolvimento = new RepositorioDeRamoDeDesenvolvimento(_unitOfWork);
            var processoDeRamoDeProducao = new ProcessoDeRamoDeProducao(_unitOfWork);
            var ProcessoDeUnidadeDeDesenvolvimento = new ProcessoDeUnidadeDeDesenvolvimento();


            var ramoDeDesenvolvimento = repositorioDeRamoDeDesenvolvimento.Obter(idRamoDesenvolvimento);
            var unidadeDeDesenvolvimento = ProcessoDeUnidadeDeDesenvolvimento.Obter(ramoDeDesenvolvimento.UnidadeDeDesenvolvimento);

            var novoRamoDeProducao = processoDeRamoDeProducao.Criar(new RamoDeProducao
            {
                IdModulo = ramoDeDesenvolvimento.IdModulo,
                IdRepositorio = ramoDeDesenvolvimento.IdRepositorio,
                IdProjeto = unidadeDeDesenvolvimento.IdProjeto,
                Solicitacao = unidadeDeDesenvolvimento.NumeroTicket,
                Versao = versaoDeProducao,
                Comentario = "Entregáveis do Pacote: " + unidadeDeDesenvolvimento.NumeroSolicitacao,
                DataPublicacao = unidadeDeDesenvolvimento.DataPublicacao.Value
            });


            if (ramoDeDesenvolvimento.IdRamoProducao != null)
            {
                var ramoDeProducao = processoDeRamoDeProducao.Obter(ramoDeDesenvolvimento.IdRamoProducao.Value);

                var ramos = repositorioDeRamoDeDesenvolvimento.Listar().Where(r => r.IdRamoProducao == ramoDeDesenvolvimento.IdRamoProducao && r.IdRamoDesenvolvimento != idRamoDesenvolvimento && r.Fechado == false).ToList();

                foreach (RamoDeDesenvolvimento rd in ramos)
                {
                    rd.IdRamoProducao = novoRamoDeProducao.IdRamoProducao;
                    rd.RamoDeProducao = novoRamoDeProducao;
                    repositorioDeRamoDeDesenvolvimento.Atualizar(rd);
                }

                if (ramoDeProducao != null)
                    processoDeRamoDeProducao.Apagar(ramoDeProducao);
            }

            ramoDeDesenvolvimento.Fechado = true;
            repositorioDeRamoDeDesenvolvimento.Atualizar(ramoDeDesenvolvimento);

            _unitOfWork.SaveChanges();
        }

        public string GerarCaminho(int idRepositorio, int idProjeto, bool? branch)
        {
            var processoDeRepositorio = new ProcessoDeRepositorio();
            var processoDeProjeto = new ProcessoDeProjeto();

            var repositorio = processoDeRepositorio.Obter(idRepositorio);
            var projeto = processoDeProjeto.Obter(new Projeto { IdProjeto = idProjeto });

            if (branch != null && branch == false)
            {
                if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.TFS)
                {
                    return repositorio.Caminho.Trim();// +"/Main";
                }
                else if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.SVN)
                {
                    return repositorio.Caminho.Trim()  +"/tags/";
                }
            }
            else
            {
                if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.TFS)
                {
                    return repositorio.Caminho.Trim();// +"/Branches/";
                }
                else if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.SVN)
                {
                    return repositorio.Caminho.Trim();// +"/Branches/";
                }
            }
            return string.Empty;
        }
    }
}
