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

        public InsufficientCreditsException(double creditsLeft)
        {
            this.creditsLeft = creditsLeft;
        }

        public override string Message
        {
            get
            {
                if (creditsNecessary != 0)
                    return string.Format(
                        "You don't have enough credit. You need €{0} extra.",
                        creditsNecessary - creditsLeft);

                return string.Format("You only have €{0} credits.", creditsLeft);
            }
        }
    }
}