using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.WebApp.Extensions;
using Container=Snacks_R_Us.Domain.IoC.Container;

namespace Snacks_R_Us.WebApp.Controllers
{
    public interface IOrderController
    {
        object Model { get; }
        ActionResult Order(CreateOrderDto order);
        ActionResult MyOrders();
    }

    public class OrderController : Controller, IOrderController
    {
        private readonly IOrderService orderService;

        public OrderController()
        {
            orderService = Container.GetImplementationOf<IOrderService>();
        }

        public ActionResult Order(CreateOrderDto order)
        {
            orderService.Order(order);
            return RedirectToAction("MyOrders");
        }

        public ActionResult MyOrders()
        {
            var model = new MyOrdersDto();
            ViewData.Model = model;

            var snackService = Container.GetImplementationOf<ISnackService>();
            model.Orders = orderService.GetMyOrders();
            model.Snacks = snackService.GetAllSnacks().ToSelectList();

            return View();
        }

        public object Model
        {
            get { return ViewData.Model; }
        }
    }
}