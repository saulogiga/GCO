﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GCO.Apresentacao.ServicoDePublicacao {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicoDePublicacao.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ObterHora", ReplyAction="http://tempuri.org/IService/ObterHoraResponse")]
        System.DateTime ObterHora();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ObterPastas", ReplyAction="http://tempuri.org/IService/ObterPastasResponse")]
        string[] ObterPastas(string caminho);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Publicar", ReplyAction="http://tempuri.org/IService/PublicarResponse")]
        string Publicar(long idPublicacaoRelease, string arquivo, string caminho, string chave);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : GCO.Apresentacao.ServicoDePublicacao.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<GCO.Apresentacao.ServicoDePublicacao.IService>, GCO.Apresentacao.ServicoDePublicacao.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.DateTime ObterHora() {
            return base.Channel.ObterHora();
        }
        
        public string[] ObterPastas(string caminho) {
            return base.Channel.ObterPastas(caminho);
        }
        
        public string Publicar(long idPublicacaoRelease, string arquivo, string caminho, string chave) {
            return base.Channel.Publicar(idPublicacaoRelease, arquivo, caminho, chave);
        }
    }
}