using System;
using System.Web.Mvc;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.WebApp.Controllers
{
    public interface IOrderController : IController
    {
        ActionResult Order(CreateOrderDto order);
        ActionResult MyOrders();
        ActionResult Today();
    }

    [HandleError]
    public class OrderController : Controller, IOrderController
    {
        private readonly IOrderService orderService;

        public OrderController()
        {
            orderService = Container.GetImplementationOf<IOrderService>();
        }

        public ActionResult MyOrders()
        {
            ViewData.Model = PrepareMyOrders();
            return View("MyOrders");
        }

        public ActionResult Order(CreateOrderDto order)
        {
            try
            {
                orderService.Order(order);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("_FORM", e.Message);
            }            
            return MyOrders();
        }

        private MyOrdersDto PrepareMyOrders()
        {
            var model = new MyOrdersDto();

            var snackService = Container.GetImplementationOf<ISnackService>();
            model.Orders = orderService.GetMyOrders();
            model.Snacks = snackService.GetAllSnacks().ToSelectList(s => s.Id, s => s.ScreenName);

            return model;
        }

        public ActionResult Today()
        {
            ViewData.Model = orderService.GetTodaysOrders();
            return View();
        }
    }
}