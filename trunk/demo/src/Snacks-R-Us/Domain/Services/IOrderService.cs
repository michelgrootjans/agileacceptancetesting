using System.Collections.Generic;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Etensions;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.Domain.Services
{
    public interface IOrderService
    {
        void Order(CreateOrderDto order);
        IEnumerable<OrderDto> GetMyOrders();
    }

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Snack> snackRepository;

        public OrderService(IRepository<Order> repository, IRepository<Snack> snackRepository)
        {
            orderRepository = repository;
            this.snackRepository = snackRepository;
        }

        public void Order(CreateOrderDto orderDto)
        {
            var order = Map.This(orderDto).ToA<Order>();
            var snack = snackRepository.Get(orderDto.SnackId.ToLong());
            order.Snack = snack;
            orderRepository.Save(order);
        }

        public IEnumerable<OrderDto> GetMyOrders()
        {
            var orders = orderRepository.FindAll();
            return Map.These(orders).ToAListOf<OrderDto>();
        }
    }
}