using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Mapping
{
    internal class OrderToDtoMapper : IMapper<Order, OrderDto>
    {
        public OrderDto Map(Order order)
        {
            var dto = new OrderDto();

            dto.Qty = order.Qty;
            dto.SnackName = order.SnackName;
            dto.UnitPrice = order.UnitPrice;
            dto.TotalPrice = order.TotalPrice;

            return dto;
        }
    }
}