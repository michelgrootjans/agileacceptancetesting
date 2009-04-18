using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Mapping
{
    public class when_UserToDtoMapper_is_told_to_map_a_user : InstanceContextSpecification<IMapper<User, ViewUserDto>>
    {
        private User user;
        private ViewUserDto result;

        protected override void Arrange()
        {
            user = Fixtures.Users.JoeDeveloper;
            user.AddCredits(5.5);
        }

        protected override IMapper<User, ViewUserDto> CreateSystemUnderTest()
        {
            return new UserToDtoMapper();
        }

        protected override void Act()
        {
            result = sut.Map(user);
        }

        [Test]
        public void should_map_its_id()
        {
            result.Id.ShouldBeEqualTo(user.Id.ToString());
        }

        [Test]
        public void should_map_its_name()
        {
         result.Name.ShouldBeEqualTo(user.Name);   
        }

        [Test]
        public void should_map_its_email()
        {
            result.Email.ShouldBeEqualTo(user.Email);
        }

        [Test]
        public void should_map_its_credit()
        {
            result.Credit.ShouldBeEqualTo(user.Credit.Amount.ToString());
        }
    }

}