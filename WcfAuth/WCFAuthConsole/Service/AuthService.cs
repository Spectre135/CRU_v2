﻿using WcfAuth.Auth;

namespace WcfAuth
{

    public class AuthService : IAuthService
    {
        public DAplRoles GetApplicationRoles(string userName, string apl)
        {
            return AuthWorker.GetApplicationRoles(userName, apl);
        }

        public DAuth GetSession(string userName)
        {
            return AuthWorker.CreateSession(userName);
        }

    }
}