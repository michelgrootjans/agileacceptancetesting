using System;
using System.Collections.Generic;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Etensions;
using Snacks_R_Us.Domain.Extensions;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.Domain.Services
{
    public interface IOrderService
    {
        void Order(CreateOrderDto order);
        ViewOrdersDto GetMyOrders();
        ViewOrdersDto GetTodaysOrders();
    }

    internal class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public void Order(CreateOrderDto orderDto)
        {
            var snack = repository.Get<Snack>(orderDto.SnackId.ToLong());
            if(snack.IsNull())
                throw new ArgumentException("Unkown snack.");

            var user = repository.Find<User>(u => u.Name.Equals(Current.UserName));
            if(user.IsNull())
                throw new ArgumentException("Unkown user. Please register.");

            var order = Map.This(orderDto).ToA<Order>();
            order.Snack = snack;
            user.AddOrder(order);
        }

        public ViewOrdersDto GetMyOrders()
        {
            var user = repository.Find<User>(u => u.Name.Equals(Current.UserName));
            return Map.This(user.Orders).ToA<ViewOrdersDto>();
        }

        public ViewOrdersDto GetTodaysOrders()
        {
            var orders = repository.FindAll<Order>(o => o.Date.Date.Equals(DateTime.Now.Date));
            var groupedOrders = Group(orders);
            return Map.This(groupedOrders).ToA<ViewOrdersDto>();
        }

        private IEnumerable<Order> Group(IEnumerable<Order> orders)
        {
            var groupedOrders = new List<Order>();
            foreach (var order in orders)
            {
                var existingOrder = groupedOrders.Find(o => o.Snack.Name.Equals(order.Snack.Name));
                if (existingOrder.IsNotNull())
                    existingOrder.Qty += order.Qty;
                else
                    groupedOrders.Add(new Order(order.Snack){Qty = order.Qty});
            }
            return groupedOrders;
        }
    }
}