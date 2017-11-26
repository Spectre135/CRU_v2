using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using WCFAuthConsole.DTO;

namespace WcfAuth
{

    [ServiceContract]
    public interface IAuthService
    {

        [OperationContract]
        DAuth GetSession(string userNameOrRIFID);

        [OperationContract]
        DAplRoles GetApplicationRoles(string userNameOrRIFID, string upl);

        [OperationContract]
        DSessionValid IsSessionValid(string sessionToken);

    }

    [DataContract]
    public class DAuth
    {
        [DataMember]
        public string SessionAuthToken { get; set; }
        [DataMember]
        public Boolean IsUserValidInAD { get; set; }
    }

    [DataContract]
    public class DAplRoles
    {
        [DataMember]
        public List<DPravice> Pravice { get; set; }
    }

    [DataContract]
    public class DSessionValid
    {
        [DataMember]
        public bool SessionValid { get; set; }
    }
}
