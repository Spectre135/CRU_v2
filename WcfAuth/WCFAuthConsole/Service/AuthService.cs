using WcfAuth.Auth;

namespace WcfAuth
{

    public class AuthService : IAuthService
    {
        public DAplRoles GetApplicationRoles(string userNameOrRIFID, string apl)
        {
            return AuthWorker.GetApplicationRoles(userNameOrRIFID, apl);
        }

        public DAuth GetSession(string userNameOrRIFID)
        {
            return AuthWorker.CreateSession(userNameOrRIFID);
        }

        public DSessionValid IsSessionValid(string sessionToken)
        {
            DSessionValid sessionValid = new DSessionValid
            {
                SessionValid = AuthWorker.IsTokenValid(sessionToken)
            };

            return sessionValid;
        }
    }
}
