using System;

namespace Snacks_R_Us.WebApp.Repositories
{
    public class InMemoryDatabase : IRepository
    {
        public long Save<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}