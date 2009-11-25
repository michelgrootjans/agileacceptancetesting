using fitlibrary;
using Snacks_R_Us.Domain;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Given : DoFixture
    {
        public Given(object theSystemUnderTest) : base(theSystemUnderTest)
        {
            Fitnesse.Init();
        }

        public Given()
        {
            Fitnesse.Init();
        }

        public void AUserWithCredits(string userName, double credit)
        {
            DemoData.AddDeveloper(userName, null, null, credit);
        }

        public void ASnackWithPrice(string snackName, double price)
        {
            DemoData.AddSnack(snackName, price);
        }

        public void ASnackThatCostsCredits(string snackName, double price)
        {
            DemoData.AddSnack(snackName, price);
        }
    }
}