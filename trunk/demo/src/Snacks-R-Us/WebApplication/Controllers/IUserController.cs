using System.Web.Mvc;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.WebApp.Controllers
{
    public interface IUserController : IController
    {
        ActionResult List();
    }

    public class UserController : Controller, IUserController
    {
        private IUserService userService;

        public UserController()
        {
            userService = Container.GetImplementationOf<IUserService>();
        }

        public ActionResult List()
        {
            ViewData.Model = userService.GetAllUsers();
            return View();
        }
    }
}