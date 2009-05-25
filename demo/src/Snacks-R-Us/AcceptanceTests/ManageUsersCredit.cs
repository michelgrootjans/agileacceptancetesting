using fitlibrary;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.Domain.Etensions;

namespace Snacks_R_Us.AcceptanceTests
{
    public class ManageUsersCredit : DoFixture
    {
        private readonly ICreditService creditService;
        private IUserService userService;

        public ManageUsersCredit()
        {
            Fitnesse.Init();

            creditService = Container.GetImplementationOf<ICreditService>();
            userService = Container.GetImplementationOf<IUserService>();
        }

        public string CreditsForUserIs(string userName)
        {
            var user = userService.GetUser(userName);
            return creditService.GetCreditsForUser(user.Id).CreditAmount;
        }

        public void AddCreditsFor(string credit, string userName)
        {
            var user = userService.GetUser(userName);
            creditService.AddCredit(new AddCreditDto { Amount = credit, UserId = user.Id });
        }

		public void DeductCreditsFor(string credit, string userName)
		{
			var user = userService.GetUser(userName);
			creditService.AddCredit(new AddCreditDto { Amount = (credit.ToDouble() * -1).ToString(), UserId = user.Id });
		}
    }
}