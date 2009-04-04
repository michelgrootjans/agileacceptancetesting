using System;
using System.Web.Mvc;
using Snacks_R_Us.WebApp.Extensions;
using Snacks_R_Us.WebApp.IoC;
using Snacks_R_Us.WebApp.Models;
using Snacks_R_Us.WebApp.Services;

namespace Snacks_R_Us.WebApp.Controllers
{
    public interface IOrderController
    {
        ActionResult New();
        ActionResult Order(CreateOrderDto order);
        ActionResult Detail(long orderId);
        object Model { get; }
    }

    public class OrderController : Controller, IOrderController
    {
        private readonly IOrderService orderService;

        public OrderController()
        {
            orderService = Container.GetImplementationOf<IOrderService>();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Order(CreateOrderDto order)
        {
            var orderId = orderService.Order(order);

            return RedirectToAction("Detail", orderId.ToIdRoute());
        }

        public ActionResult Detail(long orderId)
        {
            ViewData.Model = orderService.GetOrder(orderId);

            return View();
        }

        public object Model
        {
            get { return ViewData.Model; }
        }
    }
}