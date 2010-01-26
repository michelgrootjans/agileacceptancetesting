using System;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class OrderingASnack
    {
        public string Qty { get; set; }
        public string Snack { get; set; }
        public string ForUser { get; set; }
        public string Result { get; set; }

        public void Execute()
        {
            Current.UserName = ForUser;
            var orderservice = Container.GetImplementationOf<IOrderService>();
            var orderDto = new CreateOrderDto {Qty = Qty, SnackName = Snack};
            try
            {
                orderservice.Order(orderDto);
            }
            catch (Exception exception)
            {
                Result = exception.Message;
            }
        }
    }
}