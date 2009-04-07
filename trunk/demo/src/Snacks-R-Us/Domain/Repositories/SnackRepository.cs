using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Repositories
{
    internal class SnackRepository : IRepository<Snack>
    {
        private readonly List<Snack> snacks;

        public SnackRepository()
        {
            snacks = new List<Snack>
                         {
                             new Snack(1, "Pizza Hawaii", 5.5),
                             new Snack(2, "Club Sandwich", 2.9),
                             new Snack(3, "Cesar's Salad", 5.2),
                             new Snack(4, "Tiramisu", 4.3),
                             new Snack(5, "Big Mac", 6.6)
                         };

        }

        public void Save(Snack t)
        {
            throw new NotImplementedException();
        }

        public Snack Get(long id)
        {
            return snacks.Find(s => s.Id == id);
        }

        public IEnumerable<Snack> FindAll()
        {
            return new ReadOnlyCollection<Snack>(snacks);
        }
    }
}