using System;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.UnitTests.Fixtures
{
    public class Orders
    {
        public static Order OnePizza
        {
            get { return new Order(Snacks.Pizza){Qty = 1}; }
        }
    }
}