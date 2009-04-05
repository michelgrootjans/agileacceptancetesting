using System.Web.Mvc;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.WebApp.Controllers
{
    public interface ISnackController
    {
        ActionResult Index();
        ViewDataDictionary ViewData { get; }
    }

    public class SnackController : Controller, ISnackController
    {
        public ActionResult Index()
        {
            var service = Container.GetImplementationOf<ISnackService>();
            ViewData.Model = service.GetAllSnacks();
            return View();
        }
    }
}