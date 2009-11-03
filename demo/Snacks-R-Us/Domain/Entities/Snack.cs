namespace Snacks_R_Us.Domain.Entities
{
    public class Snack : IEntity
    {
        private static long idCounter;

        public long Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }

        public Snack(string name, double price)
        {
            Id = ++idCounter;
            Name = name;
            Price = price;
        }
    }
}