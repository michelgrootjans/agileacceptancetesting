using System.Security.Principal;
using Snacks_R_Us.Domain.IoC;

namespace Snacks_R_Us.Domain.Services
{
    public static class Current
    {
        public static IContext Context { get; set; }
        private static readonly IMembershipService UserService;
        private static readonly ICreditService CreditService;

        static Current()
        {
            UserService = Container.GetImplementationOf<IMembershipService>();
            CreditService = Container.GetImplementationOf<ICreditService>();
        }

        public static IPrincipal User
        {
            get { return UserService.GetPrincipal(UserName); }
        }

        public static string Credits
        {
            get 
            {
                var credits = CreditService.GetCreditsForCurrentUser();
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