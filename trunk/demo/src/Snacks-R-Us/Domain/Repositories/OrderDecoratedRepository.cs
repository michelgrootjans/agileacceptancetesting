using System;
using System.Collections.Generic;
using System.Linq;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Repositories
{
    public class OrderDecoratedRepository : IRepository
    {
        private readonly IRepository repository;

        public OrderDecoratedRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public void Save<T>(T t)
        {
            repository.Save(t);
        }

        public T Get<T>(long id) where T : IEntity
        {
            return repository.Get<T>(id);
        }

        public IEnumerable<T> FindAll<T>()
        {
            return repository.FindAll<T>();
        }

        public T Find<T>(Predicate<T> predicate)
        {
            return repository.Find(predicate);
        }

        public IEnumerable<T> FindAll<T>(Predicate<T> predicate)
        {
            if (typeof(T).IsAssignableFrom(typeof(Order)))
                return FindOrdersMatching(predicate);
            return repository.FindAll(predicate);
        }

        private IEnumerable<T> FindOrdersMatching<T>(Predicate<T> predicate)
        {
            var orderPredicate = predicate as Predicate<Order>;

            var orders = from user in FindAll<User>()
                         from order in user.Orders
                         where orderPredicate(order)
                         select order;

            return (IEnumerable<T>) orders;
        }
    }
}