using System;
using System.Collections.Generic;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Repositories
{
    public interface IRepository
    {
        void Save<T>(T t);
        T Get<T>(long id) where T : IEntity;
        IEnumerable<T> FindAll<T>();
        T Find<T>(Predicate<T> predicate);
        IEnumerable<T> FindAll<T>(Predicate<T> predicate);
    }

    public class InMemoryRepository : IRepository
    {
        private readonly Dictionary<Type, List<object>> entities;

        public InMemoryRepository()
        {
            entities = new Dictionary<Type, List<object>>();
        }

        public void Save<T>(T t)
        {
            if(! TableOf<T>().Contains(t))
                TableOf<T>().Add(t);
        }

        public T Get<T>(long id) where T : IEntity
        {
            return ListOf<T>().Find(t => t.Id.Equals(id));
        }

        public IEnumerable<T> FindAll<T>()
        {
            return ListOf<T>();
        }

        public T Find<T>(Predicate<T> predicate)
        {
            return ListOf<T>().Find(predicate);
        }

        public IEnumerable<T> FindAll<T>(Predicate<T> predicate)
        {
            return ListOf<T>().FindAll(predicate);
        }

        private List<T> ListOf<T>()
        {
            return TableOf<T>().ConvertAll(t => (T)t);
        }

        private List<Object> TableOf<T>()
        {
            if(! entities.ContainsKey(typeof(T)))
                entities.Add(typeof(T), new List<object>());
            return entities[typeof(T)];
        }
    }
}