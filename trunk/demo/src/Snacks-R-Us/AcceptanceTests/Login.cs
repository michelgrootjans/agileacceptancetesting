using fit;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class LogInScript : Fixture
    {
        private readonly IMembershipService membershipService;
        private readonly IAuthenticationService authenticationService;

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }

        public LogInScript()
        {
            Fitnesse.Init();

            membershipService = Container.GetImplementationOf<IMembershipService>();
            authenticationService = Container.GetImplementationOf<IAuthenticationService>();
        }

        public void LogIn()
        {
            LoggedIn = membershipService.ValidateUser(UserName, Password);
            if(LoggedIn)
                authenticationService.SignIn(UserName, false);
        }

        public void LogOut()
        {
            if(LoggedIn)
                authenticationService.SignOut();
        }
    }
}