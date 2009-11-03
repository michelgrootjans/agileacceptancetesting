using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Utilities;
using Snacks_R_Us.WebApp.Controllers;

namespace Snacks_R_Us.UnitTests.Controllers
{
    public class when_OrderController_is_told_to_order_one_pizza : InstanceContextSpecification<IOrderController>
    {
        private CreateOrderDto createOrderDto;
        private IOrderService service;
        private ActionResult result;

        protected override void Arrange()
        {
            createOrderDto = new CreateOrderDto{Qty = "1", SnackId = "2"};

            service = RegisterDependencyInContainer<IOrderService>();
            RegisterDependencyInContainer<ISnackService>();

        }

        protected override IOrderController CreateSystemUnderTest()
        {
            return new OrderController();
        }

        protected override void Act()
        {
            result = sut.Order(createOrderDto);
        }

        [Test]
        public void should_tell_the_service_to_place_the_order()
        {
            service.AssertWasCalled(s => s.Order(createOrderDto));
        }

        [Test]
        public void should_redirect_to_ViewOrder_view()
        {
            result.ShouldShowView("MyOrders");
        }
    }

    public class when_OrderController_is_told_to_show_MyOrders : InstanceContextSpecification<IOrderController>
    {
        private IOrderService orderService;
        private ISnackService snackService;
        private ActionResult result;
        private ViewOrdersDto orders;
        private IEnumerable<SnackDto> snacks;

        protected override void Arrange()
        {
            orders = new ViewOrdersDto();
            snacks = new List<SnackDto>();

            orderService = RegisterDependencyInContainer<IOrderService>();
            snackService = RegisterDependencyInContainer<ISnackService>();

            When(orderService).IsToldTo(s => s.GetMyOrders()).Return(orders);
            When(snackService).IsToldTo(s => s.GetAllSnacks()).Return(snacks);
        }

        protected override IOrderController CreateSystemUnderTest()
        {
            return new OrderController();
        }

        protected override void Act()
        {
            result = sut.MyOrders();
        }

        [Test]
        public void should_tell_the_service_to_get_my_orders()
        {
            orderService.AssertWasCalled(s => s.GetMyOrders());
        }

        [Test]
        public void should_get_all_the_snacks_from_the_repository()
        {
            snackService.AssertWasCalled(s => s.GetAllSnacks());
        }

        [Test]
        public void should_show_my_orders()
        {
            result.ShouldShowView("MyOrders");
        }

        [Test]
        public void should_put_my_orders_in_the_model()
        {
            sut.ViewData.Model.ShouldBeOfType<MyOrdersDto>()
                .Orders.ShouldBeSameAs(orders);
        }
    }

    public class when_OrderController_is_told_to_show_orders_placed_today : InstanceContextSpecification<IOrderController>
    {
        private ActionResult result;
        private IOrderService service;
        private ViewOrdersDto orders;

        protected override void Arrange()
        {
            orders = new ViewOrdersDto();
            service = RegisterDependencyInContainer<IOrderService>();
            When(service).IsToldTo(s => s.GetTodaysOrders()).Return(orders);
        }

        protected override IOrderController CreateSystemUnderTest()
        {
            return new OrderController();
        }

        protected override void Act()
        {
            result = sut.Today();
        }

        [Test]
        public void should_ask_the_service_for_todays_orders()
        {
            service.AssertWasCalled(s => s.GetTodaysOrders());
        }

        [Test]
        public void should_put_the_result_in_the_view()
        {
            sut.ViewData.Model.ShouldBeSameAs(orders);
        }

        [Test]
        public void should_show_todays_view()
        {
            result.ShouldShowView();
        }
    }

}