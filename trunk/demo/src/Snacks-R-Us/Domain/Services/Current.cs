using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using Snacks_R_Us.Domain.IoC;

namespace Snacks_R_Us.Domain.Services
{
    public static class Current
    {
        private static HttpSessionState Session { get { return HttpContext.Current.Session; } }

        public static IPrincipal User
        {
            get 
            { 
                var userService = Container.GetImplementationOf<IMembershipService>();
                return userService.GetPrincipal(UserName);
            }
        }

        internal static string UserName
        {
            get { return Session["username"] as string ?? ""; }
            set { Session["userName"] = value; }
        }

        public static string Credits
        {
            get 
            {
                var creditService = Container.GetImplementationOf<ICreditService>();
                var credits = creditService.GetCreditsForCurrentUser();
                return credits.Credit;
            }
        }
    }
}