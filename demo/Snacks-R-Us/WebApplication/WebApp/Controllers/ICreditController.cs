using System;
using System.Web.Mvc;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.WebApp.Controllers
{
    public interface ICreditController : IController
    {
        ActionResult MyCredit();
        ActionResult Edit(string id);
        ActionResult Update(AddCreditDto addCreditDto);
    }

    public class CreditController : Controller, ICreditController
    {
        private readonly ICreditService creditService;
        
        public CreditController()
        {
            creditService = Container.GetImplementationOf<ICreditService>();
        }

        public ActionResult MyCredit()
        {
            ViewData.Model = creditService.GetCreditsForCurrentUser();
            return View();
        }

        public ActionResult Edit(string id)
        {
            ViewData.Model = creditService.GetCreditsForUser(id);
            return View();
        }

        public ActionResult Update(AddCreditDto addCreditDto)
        {
            creditService.AddCredit(addCreditDto);
            return RedirectToAction("List", "User");
        }
    }
}