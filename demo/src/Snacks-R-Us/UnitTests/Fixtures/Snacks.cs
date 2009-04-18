using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.UnitTests.Fixtures
{
    public class Snacks
    {
        public static Snack Pizza
        {
            get { return new Snack("Pizza", 5.5); }
        }
    }
}