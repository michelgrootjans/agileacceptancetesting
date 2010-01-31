using System;
using System.Collections.Generic;
using System.Linq;
using SlimUtilities;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class GetTodaysOrders
    {
        public Type GetTargetClass()
        {
            return typeof (ViewOrderDto);
        }

        public List<Object> Query()
        {
            var orderService = Container.GetImplementationOf<IOrderService>();
            var viewOrderDtos = orderService.GetTodaysOrders().ToList();
            return viewOrderDtos.ToRowFixture().ToList();
        }
    }
}