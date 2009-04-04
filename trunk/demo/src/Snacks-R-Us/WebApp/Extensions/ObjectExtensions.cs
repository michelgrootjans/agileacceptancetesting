using System.Web.Routing;

namespace Snacks_R_Us.WebApp.Extensions
{
    public static class ObjectExtensions
    {
        public static RouteValueDictionary ToIdRoute(this object id)
        {
            return new RouteValueDictionary {{"Id", id.ToString()}};
        }
    }
}