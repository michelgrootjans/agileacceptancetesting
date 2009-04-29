using fit;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class CreateUsers : ColumnFixture
    {
        private readonly IMembershipService membershipService;

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Result { get; set; }

        public CreateUsers()
        {
            Fitnesse.Init();

            membershipService = Container.GetImplementationOf<IMembershipService>();
        }

        public override void Execute()
        {
            Result = membershipService.CreateUser(UserName, Password, Email, Role).ToString();
        }
    }
}