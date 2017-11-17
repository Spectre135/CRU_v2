using WcfAuth.Auth;

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
