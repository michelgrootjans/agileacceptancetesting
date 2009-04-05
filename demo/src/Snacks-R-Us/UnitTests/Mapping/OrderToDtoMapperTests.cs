using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Mapping
{
        
    public class when_OrderToDtoMapper_is_told_to_map_an_order : InstanceContextSpecification<IMapper<Order, OrderDto>>
    {
        private OrderDto result;
        private Order order;
        private Snack snack;

        protected override void Arrange()
        {
            snack = new Snack(1, "Pizza", 2.9);
            order = new Order{Id = 1, Qty = 2, Snack = snack};
        }

        protected override IMapper<Order, OrderDto> CreateSystemUnderTest()
        {
            return new OrderToDtoMapper();
        }

        protected override void Act()
        {
            result = sut.Map(order);
        }

        [Test]
        public void shold_map_its_quantity()
        {
            result.Qty.ShouldBeEqualTo(order.Qty);
        }

        [Test]
        public void should_map_its_snack_name()
        {
            result.SnackName.ShouldBeEqualTo(snack.Name);
        }

        [Test]
        public void should_map_its_unit_price()
        {
            result.UnitPrice.ShouldBeEqualTo(snack.Price);
        }

        [Test]
        public void should_map_its_total_price()
        {
            result.TotalPrice.ShouldBeEqualTo(order.Qty * snack.Price);
        }
    }
}