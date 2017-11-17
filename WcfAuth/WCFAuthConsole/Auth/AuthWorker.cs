using System;
using System.DirectoryServices.AccountManagement;
using WcfAuth.Models;
using WcfAuth;
using WcfAuth.DAO;

namespace WcfAuth.Auth
{
    public class AuthWorker
    {

        //validate user in Active Directory(AD)
        private static bool ValidateUser(string UserName)
        {
            bool valid = false;
            string Domain = "novakbm.nkbm.si";

            try
            {
                using (var domainContext = new PrincipalContext(ContextType.Domain, Domain))
                {
                    using (UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, UserName))
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
        private static string GetNewSessionToken(string UserName)
        {
            string SessionAuthToken = Guid.NewGuid().ToString();

            Uporabniki Uporabnik = DAOService.GetUporabnik(UserName);

            AuthSession _AuthSession = new AuthSession()
            {
                SessionToken = SessionAuthToken,
                UporabnikKLJ = Uporabnik.UporabnikKLJ,
                SessionTimeOut = 1800,
                Issued = DateTime.Now,
                Expired = DateTime.Now.AddSeconds(1800)
            };

            DAOService.SaveNewSession(_AuthSession);

            return SessionAuthToken;
        }

        //check SessionToken is valid
        public static bool IsTokenValid(string SessionToken)
        {
            bool IsValid = false;

            AuthSession _AuthSession = DAOService.GetSession(SessionToken);

            if (_AuthSession.Expired < DateTime.Now)
            {
                IsValid = true;
            }


            return IsValid;
        }

        //Create new session and return session token
        public static DAuth CreateSession(string UserName)
        {

            DAuth auth = new DAuth()
            {
                IsUserValidInAD = false
            };

            if (ValidateUser(UserName))
            {
                auth.SessionAuthToken = GetNewSessionToken(UserName);
                auth.IsUserValidInAD = true;
            };

            return auth;
        }

    }
}