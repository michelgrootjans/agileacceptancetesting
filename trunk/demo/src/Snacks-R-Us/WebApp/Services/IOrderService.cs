using System;
using Snacks_R_Us.WebApp.Entities;
using Snacks_R_Us.WebApp.Models;
using Snacks_R_Us.WebApp.Repositories;

namespace Snacks_R_Us.WebApp.Services
{
    public interface IOrderService
    {
        long Order(CreateOrderDto order);
        ViewOrderDto GetOrder(long orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public long Order(CreateOrderDto orderDto)
        {
            var order = Map.This(orderDto).ToA<Order>();
            repository.Save(order);
            return order.Id;
        }

        public ViewOrderDto GetOrder(long orderId)
        {
            throw new NotImplementedException();
        }
    }
}