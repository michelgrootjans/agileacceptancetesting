using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Etensions;

namespace Snacks_R_Us.Domain.Mapping
{
    internal class CreateOrderDtoMapper : IMapper<CreateOrderDto, Order>
    {
        public Order Map(CreateOrderDto createDto)
        {
            var order = new Order();

            order.Qty = createDto.Qty.ToLong();
            
            return order;
        }
    }
}