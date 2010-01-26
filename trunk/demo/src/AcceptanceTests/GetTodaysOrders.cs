using System;
using System.Collections.Generic;
using System.Linq;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class GetTodaysOrders
    {
        public Type GetTargetClass()
        {
            return typeof(ViewOrderDto);
        }
		
		public List<Object> Query()
        {
            var orderService = Container.GetImplementationOf<IOrderService>();
			List<ViewOrderDto> viewOrderDtos = orderService.GetTodaysOrders().ToList();
			
			//todo: get this cleaner
			var results = new List<object>();
			foreach (ViewOrderDto viewOrderDto in viewOrderDtos)
			{
				results.Add(ReflectionHelper.AddObjectToQueryResult(viewOrderDto));
			}
			return results;
        }
    }
}