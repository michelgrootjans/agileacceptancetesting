using System.Web.Mvc;

namespace Snacks_R_Us.WebApp.Controllers
{
    public interface IController
    {
        ViewDataDictionary ViewData { get; }
    }
}