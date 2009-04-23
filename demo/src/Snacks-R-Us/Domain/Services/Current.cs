using System.Security.Principal;
using Snacks_R_Us.Domain.IoC;

namespace Snacks_R_Us.Domain.Services
{
    public static class Current
    {
        public static IContext Context { get; set; }

        public static IPrincipal User
        {
            get
            {
                var membershipService = Container.GetImplementationOf<IMembershipService>();
                return membershipService.GetPrincipal(UserName);
            }
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

        internal static string UserName
        {
            get { return (Context["userName"] ?? "") as string; }
            set { Context["userName"] = value; }
        }
    }
}