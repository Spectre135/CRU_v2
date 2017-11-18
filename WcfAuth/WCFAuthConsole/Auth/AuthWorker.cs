using System;
using System.DirectoryServices.AccountManagement;
using WcfAuth.DAO;
using WcfAuth.Models;

namespace WcfAuth.Auth
{
    public class AuthWorker
    {

        //validate user in Active Directory(AD)
        private static bool ValidateUser(string userName)
        {
            bool valid = false;
            string domain = "novakbm.nkbm.si";

            try
            {
                using (var domainContext = new PrincipalContext(ContextType.Domain, domain))
                {
                    using (UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName))
                    {
                        valid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            return valid;
        }

        //create new SessionToken
        private static string GetNewSessionToken(string userName)
        {
            string SessionAuthToken = Guid.NewGuid().ToString();

            Uporabniki Uporabnik = DAOService.GetUporabnik(userName);

            AuthSession authSession = new AuthSession()
            {
                SessionToken = SessionAuthToken,
                UporabnikKLJ = Uporabnik.UporabnikKLJ,
                SessionTimeOut = 1800,
                Issued = DateTime.Now,
                Expired = DateTime.Now.AddSeconds(1800)
            };

            DAOService.SaveNewSession(authSession);

            return SessionAuthToken;
        }

        //check SessionToken is valid
        public static bool IsTokenValid(string sessionToken)
        {
            bool IsValid = false;

            AuthSession _AuthSession = DAOService.GetSession(sessionToken);

            if (_AuthSession.Expired < DateTime.Now)
            {
                IsValid = true;
            }


            return IsValid;
        }

        //Create new session and return session token
        public static DAuth CreateSession(string userName)
        {

            DAuth auth = new DAuth()
            {
                SessionAuthToken = "invalid username in domain",
                IsUserValidInAD = false
            };

            if (ValidateUser(userName))
            {
                auth.SessionAuthToken = GetNewSessionToken(userName);
                auth.IsUserValidInAD = true;
            };

            return auth;
        }

        //Get roles for application
        public static DAplRoles GetApplicationRoles(string userName,string apl)
        {
            DAplRoles AplRoles = new DAplRoles()
            {
                Pravice = DAOService.GetPravice(userName, apl)
            };

            return AplRoles;
        }

    }
}