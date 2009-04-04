using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Snacks_R_Us.UnitTests.Utilities;
using Snacks_R_Us.WebApp.Controllers;
using Snacks_R_Us.WebApp.Models;
using Snacks_R_Us.WebApp.Services;

namespace Snacks_R_Us.UnitTests.Controllers
{
    public class when_OrderController_is_told_to_prepare_an_order :  InstanceContextSpecification<IOrderController>
    {
        private ActionResult result;
        protected override void Arrange()
        {
            RegisterDependencyInContainer<IOrderService>();
        }

        protected override IOrderController CreateSystemUnderTest()
        {
            return new OrderController();
        }

        protected override void Act()
        {
            result = sut.New();
        }

        [Test]
        public void should_show_New_view()
        {
            result.ShouldShowView();
        }
    }

    public class when_OrderController_is_told_to_order_one_pizza_for_10_euro : InstanceContextSpecification<IOrderController>
    {
        private long orderId;
        private CreateOrderDto order;
        private IOrderService service;
        private ActionResult result;

        protected override void Arrange()
        {
            orderId = 8765;
            order = new CreateOrderDto{Qty = 1, SnackName = "Pizza Hawaii"};
            service = RegisterDependencyInContainer<IOrderService>();
            When(service).IsToldTo(s => s.Order(order)).Return(orderId);
        }

        protected override IOrderController CreateSystemUnderTest()
        {
            return new OrderController();
        }

        protected override void Act()
        {
            result = sut.Order(order);
        }

        [Test]
        public void should_tell_the_service_to_place_the_order()
        {
            service.AssertWasCalled(s => s.Order(order));
        }

        [Test]
        public void should_redirect_to_ViewOrder_view()
        {
            result.ShouldRedirectToAction("Detail")
                .ShouldHaveIdInRoute(orderId.ToString());
        }
    }

    public class when_OrderController_is_told_to_show_an_Order_detail : InstanceContextSpecification<IOrderController>
    {
        private IOrderService service;
        private long orderId;
        private ActionResult result;
        private ViewOrderDto orderDto;

        protected override void Arrange()
        {
            orderId = 548;
            orderDto = new ViewOrderDto();
            service = RegisterDependencyInContainer<IOrderService>();

            When(service).IsToldTo(s => s.GetOrder(orderId)).Return(orderDto);
        }

        protected override IOrderController CreateSystemUnderTest()
        {
            return new OrderController();
        }

        protected override void Act()
        {
            result = sut.Detail(orderId);
        }

        [Test]
        public void should_tell_the_service_to_get_the_order()
        {
            service.AssertWasCalled(s => s.GetOrder(orderId));
        }

        [Test]
        public void should_put_orderdto_ino_the_view()
        {
            sut.Model.ShouldBeSameAs(orderDto);
        }

        [Test]
        public void should_show_the_view()
        {
            result.ShouldShowView();
        }
    }

}