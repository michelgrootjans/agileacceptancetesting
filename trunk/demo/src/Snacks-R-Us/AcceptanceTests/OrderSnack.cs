using System;
using fit;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class OrderSnack : ColumnFixture
    {
        private readonly IOrderService orderService;
        
        public double Qty { get; set; }
        public string Snack { get; set; }
        public string Result { get; set; }

        public OrderSnack()
        {
            Fitnesse.Init();

            orderService = Container.GetImplementationOf<IOrderService>();
        }

        public override void Execute()
        {
            try
            {
                var snack = Get.Snack(Snack);
                orderService.Order(new CreateOrderDto { Qty = Qty.ToString(), SnackId = snack.Id });
                Result = "Success";
            }
            catch (Exception e)
            {
                Result = e.Message;
            }
        }

    }
}