using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GCO.ServicoDePublish
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        DateTime ObterHora();


        [OperationContract]
        IEnumerable<string> ObterPastas(string caminho);


        [OperationContract]
        string Publicar(Int64 idPublicacaoRelease, string arquivo, string caminho, string chave);

        // TODO: Add your service operations here
    }


    
}
