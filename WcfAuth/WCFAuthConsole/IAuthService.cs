using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfAuth
{

    [ServiceContract]
    public interface IAuthService
    {

        [OperationContract]
        DAuth GetSession(string username);

    }

    [DataContract]
    public class DAuth
    {
        [DataMember]
        public string SessionAuthToken { get; set; }
        [DataMember]
        public Boolean IsUserValidInAD { get; set; }
    }
}
