using NUnit.Framework;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Mapping
{
    public class when_CreateOrderDtoMapper_is_told_to_map_to_an_order : InstanceContextSpecification<IMapper<CreateOrderDto, Order>>
    {
        private CreateOrderDto createOrderDto;
        private Order result;
        private long requestedQuantity;

        protected override void Arrange()
        {
            ApplicationStartup.InitializeMappers();

            requestedQuantity = 5;
            createOrderDto = new CreateOrderDto {Qty = requestedQuantity.ToString(), SnackId = "886"};
        }

        protected override IMapper<CreateOrderDto, Order> CreateSystemUnderTest()
        {
            return new GenericAutoMapper<CreateOrderDto, Order>();
        }

        protected override void Act()
        {
            result = sut.Map(createOrderDto);
        }

        [Test]
        public void should_map_its_qty()
        {
            result.Qty.ShouldBeEqualTo(requestedQuantity);
        }
    }

}