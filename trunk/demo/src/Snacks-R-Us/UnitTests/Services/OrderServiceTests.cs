using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Services
{
    public class when_OrderService_is_told_to_place_an_order : InstanceContextSpecification<IOrderService>
    {
        private long snackId;
        private long orderId;
        private Order order;
        private Snack snack;
        private CreateOrderDto createOrderDto;
        private IRepository<Order> orderRepository;
        private IRepository<Snack> snackRepository;
        private IMapper<CreateOrderDto, Order> orderMapper;

        protected override void Arrange()
        {
            orderId = 654;
            snackId = 6785;
            order = new Order{Id = orderId};
            snack = new Snack(1, "Pizza", 2.9);
            createOrderDto = new CreateOrderDto{SnackId = snackId.ToString()};

            orderMapper = RegisterDependencyInContainer<IMapper<CreateOrderDto, Order>>();
            orderRepository = Dependency<IRepository<Order>>();
            snackRepository = Dependency<IRepository<Snack>>();

            When(orderMapper).IsToldTo(m => m.Map(createOrderDto)).Return(order);
            When(snackRepository).IsToldTo(r => r.Get(snackId)).Return(snack);
        }

        protected override IOrderService CreateSystemUnderTest()
        {
            return new OrderService(orderRepository, snackRepository);
        }

        protected override void Act()
        {
            sut.Order(createOrderDto);
        }

        [Test]
        public void should_map_the_dto_to_an_order()
        {
            orderMapper.AssertWasCalled(m => m.Map(createOrderDto));
        }

        [Test]
        public void should_get_the_snack_from_the_repository()
        {
            snackRepository.AssertWasCalled(r => r.Get(snackId));
        }

        [Test]
        public void should_save_a_new_order_to_the_repository()
        {
            orderRepository.AssertWasCalled(r => r.Save(order));
        }

        [Test]
        public void should_put_the_snack_in_the_order()
        {
            order.Snack.ShouldBeSameAs(snack);
        }
    }

    public class when_OrderService_is_told_to_get_my_order : InstanceContextSpecification<IOrderService>
    {
        private IRepository<Order> orderRepository;
        private IEnumerable<OrderDto> result;
        private IMapper<Order, OrderDto> orderMapper;
        private Order order1;
        private Order order2;
        private OrderDto orderDto1;
        private OrderDto orderDto2;

        protected override void Arrange()
        {
            orderRepository = Dependency<IRepository<Order>>();
            orderMapper = RegisterDependencyInContainer<IMapper<Order, OrderDto>>();

            order1 = new Order();
            order2 = new Order();
            orderDto1 = new OrderDto();
            orderDto2 = new OrderDto();

            When(orderRepository).IsToldTo(r => r.FindAll()).Return(new List<Order> {order1, order2});
            When(orderMapper).IsToldTo(m => m.Map(order1)).Return(orderDto1);
            When(orderMapper).IsToldTo(m => m.Map(order2)).Return(orderDto2);
        }

        protected override IOrderService CreateSystemUnderTest()
        {
            return new OrderService(orderRepository, Dependency<IRepository<Snack>>());
        }

        protected override void Act()
        {
            result = sut.GetMyOrders().ToList();
        }

        [Test]
        public void should_get_all_orders_from_the_repository()
        {
            orderRepository.AssertWasCalled(r => r.FindAll());
        }

        [Test]
        public void should_map_all_the_orders_to_Dtos()
        {
            orderMapper.AssertWasCalled(m => m.Map(order1));
            orderMapper.AssertWasCalled(m => m.Map(order2));
        }

        [Test]
        public void should_return_the_mapped_orders()
        {
            result.ShouldContain(orderDto1);
            result.ShouldContain(orderDto2);
        }
    }

}