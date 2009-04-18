using System;
using System.Collections.Generic;
using fit;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class OrderSnack : ColumnFixture
    {
        public double Qty { get; set; }
        public long SnackId { get; set; }
        public double UnitPrice { get; set; }
        protected double TotalPrice { get; set; }
        public string Fault { get; set; }

        public override void Execute()
        {
            ApplicationStartup.Run();
            var orderService = Container.GetImplementationOf<IOrderService>();
            try
            {
                orderService.Order(new CreateOrderDto{Qty = Qty.ToString(), SnackId = SnackId.ToString()});
                var myOrders = orderService.GetMyOrders();

                //var order = GetItem(myOrders, 0);

                //UnitPrice = order.UnitPrice;
                //TotalPrice = order.TotalPrice;

                Fault = null;
            }
            catch (Exception e)
            {
                Fault = e.Message;
            }
        }

        private static T GetItem<T>(IEnumerable<T> list, int index)
        {
            var i = 0;
            foreach (var t in list)
            {
                if (i == index) return t;
                i++;
            }
            return default(T);
        }
    }
}