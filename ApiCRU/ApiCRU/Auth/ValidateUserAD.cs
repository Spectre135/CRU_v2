using System;
using System.DirectoryServices.AccountManagement;

namespace WebCRU.Auth
{
    public class ValidateUserAD
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
    }
}