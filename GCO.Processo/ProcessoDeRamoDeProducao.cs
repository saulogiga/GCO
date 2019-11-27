using GCO.Dominio.Entidade;
using GCO.Dominio.Fabrica;
using GCO.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GCO.Processo
{
    public class ProcessoDeRamoDeProducao
    {
        private UnitOfWork _unitOfWork;

        public ProcessoDeRamoDeProducao()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ProcessoDeRamoDeProducao(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public RamoDeProducao Criar(RamoDeProducao ramo)
        {
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);
            var repositorioDeProjeto = new RepositorioDeProjeto(_unitOfWork);
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);

            var repositorio = repositorioDeRepositorio.Obter(ramo.IdRepositorio);
            var projeto = repositorioDeProjeto.Obter(ramo.IdProjeto);
            var modulo = repositorioDeModulo.Obter(ramo.IdModulo);

            ramo.Caminho = GerarCaminho(repositorio,projeto, ramo.Versao);

            //if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.TFS)
            //{
            //    ramo.Caminho = "Team Project: " + projeto.Cliente.TeamProject + " Caminho: " + repositorio.Caminho + " /Main";
            //}
            //else if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.SVN)
            //{
            //    ramo.Caminho = repositorio.Caminho + "/Trunk";
            //}

            var ramoDeProducao = FabricaDeRamoDeProducao.Criar(ramo.Caminho, ramo.Comentario, ramo.Versao, ramo.Solicitacao, ramo.DataPublicacao, repositorio, projeto, modulo);

            repositorioDeRamoDeProducao.Criar(ramoDeProducao);
            _unitOfWork.SaveChanges();


            return ramoDeProducao;
        }

        public void Atualizar(RamoDeProducao ramo)
        {
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);
            var repositorioDeProjeto = new RepositorioDeProjeto(_unitOfWork);
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);

            var ramoDeProducao = repositorioDeRamoDeProducao.Obter(ramo.IdRamoProducao);
            var repositorio = repositorioDeRepositorio.Obter(ramo.IdRepositorio);
            var projeto = repositorioDeProjeto.Obter(ramo.IdProjeto);

            ramoDeProducao.Caminho = GerarCaminho(repositorio,projeto, ramo.Versao);

            //if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.TFS)
            //{
            //    ramo.Caminho = "Team Project: " + projeto.Cliente.TeamProject + " Caminho: " + repositorio.Caminho + " /Main";
            //}
            //else if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.SVN)
            //{
            //    ramo.Caminho = repositorio.Caminho + "/Trunk";
            //}

            ramoDeProducao.Comentario = ramo.Comentario;
            ramoDeProducao.Versao = ramo.Versao;
            ramoDeProducao.DataPublicacao = ramo.DataPublicacao;
            ramoDeProducao.Solicitacao = ramo.Solicitacao;

            ramoDeProducao.Projeto = repositorioDeProjeto.Obter(ramo.IdProjeto);
            ramoDeProducao.IdProjeto = ramoDeProducao.Projeto.IdProjeto;

            ramoDeProducao.Repositorio = repositorioDeRepositorio.Obter(ramo.IdRepositorio);
            ramoDeProducao.IdRepositorio = ramoDeProducao.Repositorio.IdRepositorio;

            ramoDeProducao.Modulo = repositorioDeModulo.Obter(ramo.IdModulo);
            ramoDeProducao.IdModulo = ramoDeProducao.Modulo.IdModulo;

            repositorioDeRamoDeProducao.Atualizar(ramoDeProducao);
            _unitOfWork.SaveChanges();

        }

        public void Apagar(RamoDeProducao ramo)
        {
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);

            var _ramo = repositorioDeRamoDeProducao.Obter(ramo.IdRamoProducao);

            repositorioDeRamoDeProducao.Apagar(_ramo);
            _unitOfWork.SaveChanges();
        }

        public RamoDeProducao Obter(int idRamo)
        {
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);
            return repositorioDeRamoDeProducao.Obter(idRamo);
        }

        public IEnumerable<RamoDeProducao> Listar()
        {
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);
            return repositorioDeRamoDeProducao.Listar();
        }

        public string GerarCaminho(Repositorio repositorio,Projeto projeto, string versao)
        {
            if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.TFS)
            {
                //return "Team Project: " + projeto.Cliente.TeamProject + " Caminho: " + repositorio.Caminho; // +" /Main";
                return repositorio.Caminho + "  Label: " + versao.Trim();
            }
            else if (repositorio.IdTipoRepositorio == (int)EnumDeTipoDeRepositorio.SVN)
            {
                return repositorio.Caminho + "/tags/" + versao.Trim();
            }

            return String.Empty;
        }

        public RamoDeProducao AtualizarVersao(RamoDeProducao ramo)
        {
            var repositorioDeRamoDeProducao = new RepositorioDeRamoDeProducao(_unitOfWork);
            var repositorioDeProjeto = new RepositorioDeProjeto(_unitOfWork);
            var repositorioDeRepositorio = new RepositorioDeRepositorio(_unitOfWork);
            var repositorioDeModulo = new RepositorioDeModulo(_unitOfWork);
            var repositorioDeRamoDeDesenvolvimento = new RepositorioDeRamoDeDesenvolvimento(_unitOfWork);

            var repositorio = repositorioDeRepositorio.Obter(ramo.IdRepositorio);
            var projeto = repositorioDeProjeto.Obter(ramo.IdProjeto);
            var modulo = repositorioDeModulo.Obter(ramo.IdModulo);

            ramo.Caminho = GerarCaminho(repositorio, projeto, ramo.Versao);

            var novoRamoDeProducao = FabricaDeRamoDeProducao.Criar(ramo.Caminho, ramo.Comentario, ramo.Versao, ramo.Solicitacao, ramo.DataPublicacao, repositorio, projeto, modulo);

            repositorioDeRamoDeProducao.Criar(novoRamoDeProducao);

            var ramoAnterior = repositorioDeRamoDeProducao.Obter(ramo.IdRamoProducao);

            repositorioDeRamoDeProducao.Apagar(ramoAnterior);


            var ramosDesenvolvimento = repositorioDeRamoDeDesenvolvimento.Listar().Where(r => r.IdRamoProducao == ramo.IdRamoProducao && r.Fechado == false).ToList();

            foreach (RamoDeDesenvolvimento rd in ramosDesenvolvimento)
            {
                rd.IdRamoProducao = novoRamoDeProducao.IdRamoProducao;
                rd.RamoDeProducao = novoRamoDeProducao;
                repositorioDeRamoDeDesenvolvimento.Atualizar(rd);
            }


            _unitOfWork.SaveChanges();


            return novoRamoDeProducao;
        }
    }
}
