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
            Init.FitNesseTests();

            creditService = Container.GetImplementationOf<ICreditService>();
        }

        public string CreditsForUserIs(string userName)
        {
            var user = Get.User(userName);
            return creditService.GetCreditsForUser(user.Id).Credit;
        }

        public void AddCreditsFor(string credit, string userName)
        {
            var user = Get.User(userName);
            creditService.AddCredit(new AddCreditDto{Amount = credit, UserId = user.Id});
        }

    }
}