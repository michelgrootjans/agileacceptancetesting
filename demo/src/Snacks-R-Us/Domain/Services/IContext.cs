using System.Collections.Generic;
using System.Web;

namespace Snacks_R_Us.Domain.Services
{
    public interface IContext
    {
        object this[string key] { get; set; }
    }

    public class WebContext : IContext
    {
        public object this[string key]
        {
            get { return HttpContext.Current.Session[key]; }
            set { HttpContext.Current.Session[key] = value; }
        }
    }

    public class StaticContext : IContext
    {
        private IDictionary<string, object> values;

        public StaticContext(IDictionary<string, object> values)
        {
            this.values = values;
        }

        public object this[string key]
        {
            get { return values[key]; }
            set { values[key] = value; }
        }
    }
}