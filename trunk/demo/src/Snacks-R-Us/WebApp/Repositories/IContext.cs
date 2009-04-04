using System.Collections;

namespace Snacks_R_Us.WebApp.Repositories
{
    public interface IContext
    {
        IDictionary Items { get; }
    }
}