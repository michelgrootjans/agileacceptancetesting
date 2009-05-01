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
        private readonly ISnackService snackService;

        public double Qty { get; set; }
        public string Snack { get; set; }

        public string ForUser
        {
            set { Current.UserName = value; }
        }

        public string Result { get; set; }

        public OrderSnack()
        {
            Fitnesse.Init();
            orderService = Container.GetImplementationOf<IOrderService>();
            snackService = Container.GetImplementationOf<ISnackService>();
        }

        public override void Execute()
        {
            try
            {
                var snack = snackService.GetSnack(Snack);
                orderService.Order(new CreateOrderDto {Qty = Qty.ToString(), SnackId = snack.Id});
                Result = "Success";
            }
            catch (Exception e)
            {
                Result = e.Message;
            }
        }
    }
}