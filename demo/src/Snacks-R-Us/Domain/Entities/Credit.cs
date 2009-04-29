namespace Snacks_R_Us.Domain.Entities
{
    public class Credit
    {
        public double Amount { get; private set; }

        public Credit(double originalValue)
        {
            Amount = originalValue;
        }

        public void AddAmount(double amt)
        {
            Amount += amt;
        }

        public void Clear()
        {
            Amount = 0;
        }
    }
}