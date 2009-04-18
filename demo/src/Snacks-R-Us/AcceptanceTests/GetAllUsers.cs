using System;
using System.Linq;
using fit;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class GetAllUsers : RowFixture
    {
        public override Type GetTargetClass()
        {
            return typeof (ViewUserDto);
        }

        public override object[] Query()
        {
            var service = Container.GetImplementationOf<IUserService>();
            return service.GetAllUsers().ToArray();
        }
    }
}