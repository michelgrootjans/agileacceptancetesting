using System;
using fit;
using Snacks_R_Us.WebApp;
using Snacks_R_Us.WebApp.IoC;
using Snacks_R_Us.WebApp.Models;
using Snacks_R_Us.WebApp.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class OrderSnack : ColumnFixture
    {
        public double Qty { get; set; }
        public string SnackName { get; set; }
        public double UnitPrice { get; set; }
        protected double TotalPrice { get; set; }
        public string Fault { get; set; }

        public override void Execute()
        {
            ApplicationStartup.Run();
            var orderService = Container.GetImplementationOf<IOrderService>();
            try
            {
                var orderId = orderService.Order(new CreateOrderDto{Qty = Qty, SnackName = SnackName});
                var order = orderService.GetOrder(orderId);

                UnitPrice = order.UnitPrice;
                TotalPrice = order.TotalPrice;

                Fault = null;
            }
            catch (Exception e)
            {
                Fault = e.Message;
            }
        }
    }
}