using fit;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class CreateUsers : ColumnFixture
    {
        private readonly IMembershipService service;

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Fault { get; set; }

        public CreateUsers()
        {
            ApplicationStartup.Run();
            service = Container.GetImplementationOf<IMembershipService>();
        }

        public override void Execute()
        {
            Fault = service.CreateUser(UserName, Password, Email, Role).ToString();
        }
    }
}