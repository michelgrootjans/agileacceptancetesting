using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Fixtures;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Services
{
    public class OrderServiceTest : InstanceContextSpecification<IOrderService>
    {
        protected long snackId;
        protected Order order;
        protected Snack snack;
        protected CreateOrderDto createOrderDto;
        protected IRepository repository;
        protected IMapper<CreateOrderDto, Order> orderMapper;
        protected User user;

        protected override void Arrange()
        {
            snackId = 6785;
            order = new Order();
            snack = new Snack("Pizza", 2.9);
            user = Users.JoeDeveloper;
            user.AddCredits(3);

            createOrderDto = new CreateOrderDto {SnackId = snackId.ToString()};

            orderMapper = RegisterMapper<CreateOrderDto, Order>();
            repository = Dependency<IRepository>();
        }

        protected override IOrderService CreateSystemUnderTest()
        {
            return new OrderService(repository);
        }
    }

    public class when_OrderService_is_told_to_place_an_order : OrderServiceTest
    {
        protected override void Arrange()
        {
            base.Arrange();
            When(orderMapper).IsToldTo(m => m.Map(createOrderDto)).Return(order);
            When(repository).IsToldTo(r => r.Get<Snack>(snackId)).Return(snack);
            When(repository).IsToldTo(r => r.Find(Arg<Predicate<User>>.Is.Anything)).Return(user);
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
            repository.AssertWasCalled(r => r.Get<Snack>(snackId));
        }

        [Test]
        public void should_save_a_new_order_to_the_repository()
        {
            repository.AssertWasCalled(r => r.Find(Arg<Predicate<User>>.Is.Anything));
        }

        [Test]
        public void should_put_the_snack_in_the_order()
        {
            order.Snack.ShouldBeSameAs(snack);
        }

        [Test]
        public void should_add_the_order_to_the_user()
        {
            user.Orders.ShouldContain(order);
        }
    }

    public class when_OrderService_is_told_to_place_an_order_with_an_unknown_user : OrderServiceTest
    {
        private Action orderSnack;

		protected override void Arrange()
		{
			base.Arrange();
			When(repository).IsToldTo(r => r.Get<Snack>(snackId)).Return(snack);
		}

        protected override void Act()
        {
            orderSnack = () => sut.Order(createOrderDto);
        }

        [Test]
        public void should_throw_an_UnknownUserException()
        {
            orderSnack.ShouldThrow<ArgumentException>()
                .Message.ShouldBeEqualTo("Unkown user. Pleaser register.");
        }
    }


    public class when_OrderService_is_told_to_get_my_order : InstanceContextSpecification<IOrderService>
    {
        private IRepository repository;
        private ViewOrdersDto result;
        private IMapper<IEnumerable<Order>, ViewOrdersDto> orderMapper;
        private Order order1;
        private Order order2;
        private ViewOrderDto orderDto1;
        private ViewOrderDto orderDto2;
        private ViewOrdersDto ordersDto;
        private User user;

        protected override void Arrange()
        {
            repository = Dependency<IRepository>();
            orderMapper = RegisterMapper<IEnumerable<Order>, ViewOrdersDto>();

            user = Users.JoeDeveloper;
            user.AddCredits(3);

            order1 = new Order(new Snack("Pizza", 2));
            order2 = new Order(new Snack("Sandwich", 1));
            orderDto1 = new ViewOrderDto();
            orderDto2 = new ViewOrderDto();
            ordersDto = new ViewOrdersDto();
            ordersDto.Orders.Add(orderDto1);
            ordersDto.Orders.Add(orderDto2);

            user.AddOrder(order1);
            user.AddOrder(order2);

            When(repository).IsToldTo(r => r.Find(Arg<Predicate<User>>.Is.Anything)).Return(user);
            When(orderMapper).IsToldTo(m => m.Map(user.Orders)).Return(ordersDto);
        }

        protected override IOrderService CreateSystemUnderTest()
        {
            return new OrderService(repository);
        }

        protected override void Act()
        {
            result = sut.GetMyOrders();
        }

        [Test]
        public void should_map_all_the_orders_to_Dtos()
        {
            orderMapper.AssertWasCalled(m => m.Map(user.Orders));
        }

        [Test]
        public void should_return_the_mapped_orders()
        {
            result.Orders.ShouldContain(orderDto1);
            result.Orders.ShouldContain(orderDto2);
        }
    }

    public class when_OrderService_is_told_to_get_todays_orders : InstanceContextSpecification<IOrderService>
    {
        private IRepository repository;
        private ViewOrdersDto result;
        private IMapper<IEnumerable<Order>, ViewOrdersDto> mapper;
        private Order order1;
        private Order order2;
        private ViewOrderDto order1Dto;
        private ViewOrderDto order2Dto;
        private ViewOrdersDto ordersDto;
        private List<Order> orders;

        protected override void Arrange()
        {
            order1 = Orders.OnePizza;
            order2 = Orders.OnePizza;
            orders = new List<Order> {order1, order2};
            order1Dto = new ViewOrderDto();
            order2Dto = new ViewOrderDto();
            ordersDto = new ViewOrdersDto();
            ordersDto.Orders.Add(order1Dto);
            ordersDto.Orders.Add(order2Dto);

            mapper = RegisterMapper<IEnumerable<Order>, ViewOrdersDto>();
            repository = Dependency<IRepository>();

            When(repository)
                .IsToldTo(r => r.FindAll(Arg<Predicate<Order>>.Is.Anything))
                .Return(orders);

            When(mapper).IsToldTo(m => m.Map(Arg<IEnumerable<Order>>.Is.Anything)).Return(ordersDto);
        }

        protected override IOrderService CreateSystemUnderTest()
        {
            return new OrderService(repository);
        }

        protected override void Act()
        {
            result = sut.GetTodaysOrders();
        }

        [Test]
        public void should_get_all_orders_from_today_from_the_repostitory()
        {
            repository.AssertWasCalled(r => r.FindAll(Arg<Predicate<Order>>.Is.Anything));
        }

        [Test]
        public void should_map_the_resulting_orders()
        {
            mapper.AssertWasCalled(m => m.Map(Arg<IEnumerable<Order>>.Is.Anything));
        }


        [Test]
        public void should_return_a_list_containing_the_two_mapped_results()
        {
            result.ShouldContain(order1Dto);
            result.ShouldContain(order2Dto);
        }
    }
}