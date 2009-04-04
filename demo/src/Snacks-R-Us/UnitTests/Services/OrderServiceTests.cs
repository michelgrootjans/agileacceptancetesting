using NUnit.Framework;
using Snacks_R_Us.UnitTests.Utilities;
using Snacks_R_Us.WebApp;
using Snacks_R_Us.WebApp.Entities;
using Snacks_R_Us.WebApp.Models;
using Snacks_R_Us.WebApp.Repositories;
using Snacks_R_Us.WebApp.Services;
using Rhino.Mocks;

namespace Snacks_R_Us.UnitTests.Services
{
    public class when_OrderService_is_told_to_place_an_order : InstanceContextSpecification<IOrderService>
    {
        private IRepository repository;
        private Order order;
        private IMapper<CreateOrderDto, Order> mapper;
        private CreateOrderDto dto;
        private long id;
        private long result;

        protected override void Arrange()
        {
            id = 654;
            order = new Order{Id = id};
            dto = new CreateOrderDto();
            mapper = RegisterDependencyInContainer<IMapper<CreateOrderDto, Order>>();
            repository = Dependency<IRepository>();

            When(mapper).IsToldTo(m => m.Map(dto)).Return(order);
        }

        protected override IOrderService CreateSystemUnderTest()
        {
        return new OrderService(repository);}

        protected override void Act()
        {
            result = sut.Order(dto);
        }

        [Test]
        public void should_map_the_dto_to_an_order()
        {
            mapper.AssertWasCalled(m => m.Map(dto));
        }

        [Test]
        public void should_save_a_new_order_to_the_database()
        {
            repository.AssertWasCalled(r => r.Save(order));
        }

        [Test]
        public void should_return_the_id_of_the_entity()
        {
            result.ShouldBeEqualTo(id);
        }
    }

}