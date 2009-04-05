namespace Snacks_R_Us.Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public Snack Snack { get; set; }

        public double Qty { get; set; }

        public string SnackName { get { return Snack.Name; } }
        public double UnitPrice { get { return Snack.Price; } }
        public double TotalPrice { get { return Snack.Price * Qty; } }
    }
}