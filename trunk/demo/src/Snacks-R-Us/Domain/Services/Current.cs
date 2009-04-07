using System.Security.Principal;
using System.Web;
using Snacks_R_Us.Domain.IoC;

namespace Snacks_R_Us.Domain.Services
{
    public static class Current
    {
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
            private get
            {
                if (HttpContext.Current.Session["username"] == null) return "";
                return HttpContext.Current.Session["username"] as string;
            }
            set { HttpContext.Current.Session["username"] = value; }
        }
    }
}