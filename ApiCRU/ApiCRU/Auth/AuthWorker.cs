using si.hit.WebCRU.Service;
using System;
using System.DirectoryServices.AccountManagement;
using WebCRU.Models;

namespace WebCRU.Auth
{
    public class AuthWorker
    {
        

        //validate user in Active Directory(AD)
        public static bool ValidateUser(string UserName)
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
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


            return valid;
        }


        //create new SessionToken
        public string GetNewSessionToken(string UserName)
        {
            string SessionAuthToken = Guid.NewGuid().ToString();

            Uporabniki Uporabnik = CRUDService.GetUporabnik(UserName);

            AuthSession _AuthSession = new AuthSession()
            {
                SessionToken = SessionAuthToken,
                //UporabnikKLJ = Uporabnik.UporabnikKLJ,
                 
            };


            return SessionAuthToken;
        }
    }
}