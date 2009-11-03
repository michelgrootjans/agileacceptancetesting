using System;
using fitlibrary;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class ManageUsersCredit : DoFixture
    {
        public string CreditsForUserIs(string userName)
        {
            var userService = Container.GetImplementationOf<IUserService>();
            return userService.GetUser(userName).CreditAmount;
        }

        public string AddCreditsFor(string amount, string userName)
        {
            var creditService = Container.GetImplementationOf<ICreditService>();
            var addCreditDto = new AddCreditDto{Amount = amount, UserName = userName};
            try
            {
                creditService.AddCredit(addCreditDto);
                return "Ok";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string DeductCreditsFor(string amount, string userName)
        {
            var creditService = Container.GetImplementationOf<ICreditService>();
            var addCreditDto = new AddCreditDto { Amount = "-" + amount, UserName = userName };
            try
            {
                creditService.AddCredit(addCreditDto);
                return "Ok";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}