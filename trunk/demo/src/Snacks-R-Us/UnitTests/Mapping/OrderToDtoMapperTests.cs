using System.Collections.Generic;
using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Mapping
{

    public class when_OrderToDtoMapper_is_told_to_map_an_order : InstanceContextSpecification<IMapper<IEnumerable<Order>, ViewOrdersDto>>
    {
        private ViewOrdersDto result;
        private Order order;
        private Snack snack;
        private IEnumerable<Order> orders;

        protected override void Arrange()
        {
            snack = new Snack("Pizza", 2.9);
            order = new Order{Qty = 2, Snack = snack};
            orders = new List<Order> {order};
        }

        protected override IMapper<IEnumerable<Order>, ViewOrdersDto> CreateSystemUnderTest()
        {
            return new OrderToDtoMapper();
        }

        protected override void Act()
        {
            result = sut.Map(orders);
        }

        [Test]
        public void shold_map_its_quantity()
        {
            result.Orders[0].Qty.ShouldBeEqualTo(order.Qty);
        }

        [Test]
        public void should_map_its_snack_name()
        {
            result.Orders[0].SnackName.ShouldBeEqualTo(snack.Name);
        }

        [Test]
        public void should_map_its_unit_price()
        {
            result.Orders[0].UnitPrice.ShouldBeEqualTo(snack.Price);
        }

        [Test]
        public void should_map_its_total_price()
        {
            result.Orders[0].TotalPrice.ShouldBeEqualTo(order.Qty * snack.Price);
        }

        [Test]
        public void should_map_the_total_price_of_all_ordres()
        {
            var expected = order.Qty * snack.Price;
            result.Total.ShouldBeEqualTo(expected.ToString());
        }
    }
}