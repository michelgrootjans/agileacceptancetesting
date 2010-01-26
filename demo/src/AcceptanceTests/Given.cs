using Snacks_R_Us.Domain;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Given     {
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