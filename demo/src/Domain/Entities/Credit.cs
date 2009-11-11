namespace Snacks_R_Us.Domain.Entities
{
    public class Credit
    {
        public double Amount { get; private set; }

        public Credit(double originalValue)
        {
            Amount = originalValue;
        }

        public void AddAmount(double amount)
        {
            if (Amount + amount < 0)
                throw new InsufficientCreditsException(Amount, amount);
            Amount += amount;
        }

        internal void Pay(Order order)
        {
            if (order.TotalPrice > Amount)
                throw new InsufficientCreditsException(Amount, order.TotalPrice);
            AddAmount(-order.TotalPrice);
        }
    }
}