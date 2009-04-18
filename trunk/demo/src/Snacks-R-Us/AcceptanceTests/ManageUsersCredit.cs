using fitlibrary;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.Domain.Extensions;

namespace Snacks_R_Us.AcceptanceTests
{
    public class ManageUsersCredit : DoFixture
    {
        private readonly IUserService userService;
        private readonly ICreditService creditService;

        public ManageUsersCredit()
        {
            ApplicationStartup.Run();

            userService = Container.GetImplementationOf<IUserService>();
            creditService = Container.GetImplementationOf<ICreditService>();
        }

        public string CreditsForUserIs(string userName)
        {
            var user = GetUser(userName);
            return creditService.GetCreditsForUser(user.Id).Credit;
        }

        public void AddCreditsFor(string credit, string userName)
        {
            var user = GetUser(userName);
            creditService.AddCredit(new AddCreditDto{Amount = credit, UserId = user.Id});
        }

        private ViewUserDto GetUser(string userName)
        {
            return userService.GetAllUsers().Find(u => u.Name.Equals(userName));
        }
    }
}