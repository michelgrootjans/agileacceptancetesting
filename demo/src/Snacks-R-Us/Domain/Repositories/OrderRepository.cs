using System;
using System.Collections.Generic;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly List<Order> orders;
        
        public OrderRepository()
        {
            orders = new List<Order>();
        }

        public void Save(Order order)
        {
            orders.Add(order);
        }

        public Order Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> FindAll()
        {
            return orders;
        }
    }
}