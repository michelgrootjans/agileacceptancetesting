using System;
using fit;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;
using System.Linq;

namespace Snacks_R_Us.AcceptanceTests
{
    public class GetTodaysOrders : RowFixture
    {
        private readonly IOrderService orderService;

        public GetTodaysOrders()
        {
            Fitnesse.Init();

            orderService = Container.GetImplementationOf<IOrderService>();
        }

        public override Type GetTargetClass()
        {
            return typeof (ViewOrderDto);
        }

        public override object[] Query()
        {
            return orderService.GetTodaysOrders().ToArray();
        }
    }
}