using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Extensions;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public static class Get
    {
        private static readonly IUserService UserService;
        private static readonly ISnackService SnackService;

        static Get()
        {
            Fitnesse.Init();

            SnackService = Container.GetImplementationOf<ISnackService>();
            UserService = Container.GetImplementationOf<IUserService>();
        }

        internal static SnackDto Snack(string snackName)
        {
            return SnackService.GetAllSnacks().Find(s => s.Name.Equals(snackName));
        }

        internal static ViewUserDto User(string userName)
        {
            return UserService.GetAllUsers().Find(u => u.Name.Equals(userName));
        }
    }
}