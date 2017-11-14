using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebCRU.Auth;

namespace WcfAuth
{

    public class AuthService : IAuthService
    {

        public DAuth GetSession(string username)
        {
            return AuthWorker.CreateSession(username);
        }
        
    }
}
