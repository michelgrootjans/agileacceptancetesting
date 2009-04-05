namespace Snacks_R_Us.Domain.Entities
{
    public class Snack
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }

        public Snack(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}