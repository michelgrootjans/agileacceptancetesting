using fitlibrary;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class ManageUsersCredit : DoFixture
    {
        private readonly ICreditService creditService;

        public ManageUsersCredit()
        {
            Fitnesse.Init();

            creditService = Container.GetImplementationOf<ICreditService>();
        }

        public string CreditsForUserIs(string userName)
        {
            var user = new Get().User(userName);
            return creditService.GetCreditsForUser(user.Id).CreditAmount;
        }

        public void AddCreditsFor(string credit, string userName)
        {
            var user = new Get().User(userName);
            creditService.AddCredit(new AddCreditDto {Amount = credit, UserId = user.Id});
        }
    }
}