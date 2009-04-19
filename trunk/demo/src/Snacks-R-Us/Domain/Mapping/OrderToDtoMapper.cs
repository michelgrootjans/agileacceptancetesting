using System.Collections.Generic;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Mapping
{
    internal class OrderToDtoMapper : 
        IMapper<Order, ViewOrderDto>,
        IMapper<IEnumerable<Order>, ViewOrdersDto>
    {
        public ViewOrderDto Map(Order order)
        {
            var dto = new ViewOrderDto();

            dto.Qty = order.Qty;
            dto.Snack = order.SnackName;
            dto.UnitPrice = order.UnitPrice;
            dto.TotalPrice = order.TotalPrice;

            return dto;
        }

        public ViewOrdersDto Map(IEnumerable<Order> from)
        {
            double total = 0;
            var orders = new ViewOrdersDto();
            foreach (var order in from)
            {
                orders.Orders.Add(Map(order));
                total += order.TotalPrice;
            }
            orders.Total = total.ToString();
            return orders;
        }
    }
}