using System.Collections.Generic;

namespace Snacks_R_Us.Domain.Repositories
{
    public interface IRepository<T>
    {
        void Save(T t);
        T Get(long id);
        IEnumerable<T> FindAll();
    }
}