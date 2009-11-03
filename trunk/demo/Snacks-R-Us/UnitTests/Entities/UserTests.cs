using System;
using System.Linq;
using NUnit.Framework;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Entities
{
    public class when_an_order_is_added_to_a_user_with_enough_credits : InstanceContextSpecification<User>
    {
        private Order order;

        protected override void Arrange()
        {
            var snack = new Snack("Pizza", 2.3);
            order = new Order{Qty = 10, Snack = snack};
        }

        protected override User CreateSystemUnderTest()
        {
            var user = new User("name", "password", "email", "role");
            user.AddCredits(23);

            return user;
        }

        protected override void Act()
        {
            sut.AddOrder(order);
        }

        [Test]
        public void should_add_the_order_in_the_users_orders()
        {
            sut.Orders.ShouldContain(order);
        }

        [Test]
        public void should_deduct_the_ordered_amount_from_the_users_credits()
        {
            sut.Credit.Amount.ShouldBeEqualTo(0);
        }
    }

    public class when_an_order_is_added_to_a_user_with_insufficient_credits : InstanceContextSpecification<User>
    {
        private Order order;
        private Action addOrder;

        protected override void Arrange()
        {
            var snack = new Snack("Pizza", 2.3);
            order = new Order {Qty = 1, Snack = snack};
        }

        protected override User CreateSystemUnderTest()
        {
            return new User("name", "password", "email", "role");
        }

        protected override void Act()
        {
            addOrder = () => sut.AddOrder(order);
        }

        [Test]
        public void should_throw_an_insufficient_credits_exception()
        {
            addOrder.ShouldThrow<InsufficientCreditsException>();
        }

    }

    public class when_an_order_is_added_twice_to_a_user : InstanceContextSpecification<User>
    {
        private Order pizza1;
        private Order pizza2;
        private Order result;
        private Snack snack;

        protected override void Arrange()
        {
            snack = new Snack("Pizza", 3.2);
            pizza1 = new Order(snack);
            pizza2 = new Order(snack);
        }

        protected override User CreateSystemUnderTest()
        {
            var user = new User("name", "password", "email", "role");
            user.AddCredits(10);
            return user;
        }

        protected override void Act()
        {
            sut.AddOrder(pizza1);
            sut.AddOrder(pizza2);
            result = sut.Orders.GetItem(0);
        }

        [Test]
        public void should_contain_the_order_only_once()
        {
            sut.Orders.Count().ShouldBeEqualTo(1);
        }

        [Test]
        public void should_contain_two_pizzas()
        {
            sut.Orders.GetItem(0).Qty.ShouldBeEqualTo(2);
            result.Snack.ShouldBeSameAs(snack);
        }

    }

}