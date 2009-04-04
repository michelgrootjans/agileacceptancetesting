using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Snacks_R_Us.WebApp.Repositories
{
    public class StaticContext : IContext
    {
        private readonly IDictionary items;

        public StaticContext()
        {
            items = new Dictionary<object, object>();
        }

        public IDictionary Items
        {
            get { return items; }
        }
    }

    public class SessionContext : IContext
    {
        private readonly HttpContext httpContext;

        public SessionContext(HttpContext current)
        {
            httpContext = current;
        }

        public IDictionary Items
        {
            get { return httpContext.Items; }
        }
    }
}