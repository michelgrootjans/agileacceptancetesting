using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Mapping
{
    public class when_CreditToDtoMapper_is_told_to_map_a_credit : InstanceContextSpecification<IMapper<User, ViewCreditDto>>
    {
        private ViewCreditDto result;
        private User user;
        private double credit;

        protected override void Arrange()
        {
            credit = 45.7;
            user = Fixtures.Users.JoeDeveloper;
            user.AddCredits(credit);
        }

        protected override IMapper<User, ViewCreditDto> CreateSystemUnderTest()
        {
            return new UserToCreditDtoMapper();
        }

        protected override void Act()
        {
            result = sut.Map(user);
        }

        [Test]
        public void should_map_the_credit_amount()
        {
            result.Credit.ShouldBeEqualTo(credit.ToString());
        }

        [Test]
        public void should_map_the_user_id()
        {
            result.UserId.ShouldBeEqualTo(user.Id.ToString());
        }

        [Test]
        public void should_map_the_username()
        {
            result.UserName.ShouldBeEqualTo(user.Name);
        }
    }

}