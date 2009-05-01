using System.Web.Security;

namespace Snacks_R_Us.Domain.Services
{
    public interface IAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    internal class FormsAuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationService authenticationService;

        public FormsAuthenticationService(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public void SignIn(string userName, bool createPersistentCookie)
        {
            authenticationService.SignIn(userName, createPersistentCookie);
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            authenticationService.SignOut();
            FormsAuthentication.SignOut();
        }
    }

    internal class SimpleAuthenticationService : IAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            Current.UserName = userName;
        }

        public void SignOut()
        {
            Current.UserName = null;
        }
    }
}