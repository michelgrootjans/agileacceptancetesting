using System;

namespace Snacks_R_Us.Domain
{
    public class InsufficientCreditsException : Exception
    {
        private readonly double creditsLeft;
        private readonly double creditsNecessary;

        public InsufficientCreditsException(double creditsLeft, double creditsNecessary)
        {
            this.creditsLeft = creditsLeft;
            this.creditsNecessary = creditsNecessary;
        }

        public override string Message
        {
            get { return string.Format("You don't have enough credit. You need €{0} extra.", creditsNecessary - creditsLeft); }
        }
    }
}