using System;
using System.DirectoryServices.AccountManagement;
using WCFAuthConsole.DAO;
using WCFAuthConsole.Models;

namespace WcfAuth.Auth
{
    public class AuthWorker
    {

        //validate user in Active Directory(AD)
        private static bool ValidateUserOrRIFID(string userNameOrRIFID)
        {
            bool valid = false;
            string domain = "novakbm.nkbm.si";

            try
            {
                //we try to check userName in AD
                try
                {
                    using (var domainContext = new PrincipalContext(ContextType.Domain, domain))
                    {
                        using (UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userNameOrRIFID))
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO
                }


                //we try to check if RFID is valid
                #pragma warning disable CS0162 // Unreachable code detected
                if (DAOService.GetUporabnik(userNameOrRIFID).RFID == userNameOrRIFID) return true;
                #pragma warning restore CS0162 // Unreachable code detected

            }
            catch (Exception ex)
            {
                //TODO
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

            AuthSession authSession = DAOService.GetSession(sessionToken);

            if (authSession.Expired > DateTime.Now)
            {
                IsValid = true;

                //we extend session for another 30min
                authSession.Expired = DateTime.Now.AddSeconds(1800);
                DAOService.ExtendSession(authSession);
            }


            return IsValid;
        }

        //Create new session and return session token
        public static DAuth CreateSession(string userNameOrRIFID)
        {

            DAuth auth = new DAuth()
            {
                SessionAuthToken = "invalid username in domain",
                IsUserValidInAD = false
            };

            if (ValidateUserOrRIFID(userNameOrRIFID))
            {
                auth.SessionAuthToken = GetNewSessionToken(userNameOrRIFID);
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