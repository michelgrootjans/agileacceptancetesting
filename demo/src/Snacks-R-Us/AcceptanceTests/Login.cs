using fit;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class LogInScript : Fixture
    {
        private readonly IMembershipService service;

        public LogInScript()
        {
            ApplicationStartup.Run();
            service = Container.GetImplementationOf<IMembershipService>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }

        public void LogIn()
        {
            LoggedIn = service.ValidateUser(UserName, Password);
        }
    }
}