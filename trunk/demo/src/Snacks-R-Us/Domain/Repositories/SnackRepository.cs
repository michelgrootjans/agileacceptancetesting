using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Repositories
{
    public class SnackRepository : IRepository<Snack>
    {
        private List<Snack> snacks;

        public SnackRepository()
        {
            snacks = new List<Snack>
                         {
                             new Snack(1, "Pizza Hawaii", 5.5),
                             new Snack(2, "Club Sandwich", 2.9)
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