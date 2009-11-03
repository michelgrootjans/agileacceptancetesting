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
            if(Amount + amount < 0)
                throw new InsufficientCreditsException(Amount);
            Amount += amount;
        }

        public void Clear()
        {
            Amount = 0;
        }
    }
}