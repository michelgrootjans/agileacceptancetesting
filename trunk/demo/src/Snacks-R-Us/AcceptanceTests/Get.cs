using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Extensions;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class Get
    {
        private readonly IUserService userService;
        private readonly ISnackService snackService;

        public Get()
        {
            Fitnesse.Init();

            snackService = Container.GetImplementationOf<ISnackService>();
            userService = Container.GetImplementationOf<IUserService>();
        }

        internal SnackDto Snack(string snackName)
        {
            return snackService.GetAllSnacks().Find(s => s.Name.Equals(snackName));
        }

        internal ViewUserDto User(string userName)
        {
            return userService.GetAllUsers().Find(u => u.Name.Equals(userName));
        }
    }
}