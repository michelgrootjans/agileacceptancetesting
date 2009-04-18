using System;

namespace Snacks_R_Us.Domain.Entities
{
    public class Order
    {
        private static long idCounter;
        
        public long Id { get; private set; }
        public double Qty { get; set; }
        public DateTime Date { get; private set; }
        public Snack Snack { get; internal set; }

        internal Order()
        {
            Id = ++idCounter;
            Date = DateTime.Now;
            Qty = 1;
        }

        public Order(Snack snack) : this()
        {
            Snack = snack;
        }

        public string SnackName { get { return Snack.Name; } }
        public double UnitPrice { get { return Snack.Price; } }
        public double TotalPrice { get { return Snack.Price * Qty; } }
    }
}