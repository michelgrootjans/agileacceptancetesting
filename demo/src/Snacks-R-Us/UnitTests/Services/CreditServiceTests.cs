using System;
using NUnit.Framework;
using Rhino.Mocks;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Services
{
    public class when_CreditService_is_told_to_get_credits_for_user : InstanceContextSpecification<ICreditService>
    {
        private IRepository repository;
        private IMapper<User, ViewCreditDto> mapper;
        private Credit credit;
        private ViewCreditDto result;
        private ViewCreditDto viewCreditDto;
        private User user;

        protected override void Arrange()
        {
            credit = new Credit(0);
            user = Fixtures.Users.JoeDeveloper;
            user.Credit = credit;
            viewCreditDto = new ViewCreditDto();
            repository = Dependency<IRepository>();
            mapper = RegisterMapper<User, ViewCreditDto>();

            When(repository).IsToldTo(r => r.Find(Arg<Predicate<User>>.Is.Anything)).Return(user);
            When(mapper).IsToldTo(m => m.Map(user)).Return(viewCreditDto);
        }

        protected override ICreditService CreateSystemUnderTest()
        {
            return new CreditService(repository);
        }

        protected override void Act()
        {
            result = sut.GetCreditsForCurrentUser();
        }

        [Test]
        public void should_get_the_credits_from_the_repository()
        {
            repository.AssertWasCalled(r => r.Find(Arg<Predicate<User>>.Is.Anything));
        }

        [Test]
        public void should_map_the_credits_to_a_dto()
        {
            mapper.AssertWasCalled(m => m.Map(user));
        }

        [Test]
        public void should_return_the_dto()
        {
            result.ShouldBeEqualTo(viewCreditDto);
        }
    }

    public class when_CreditService_is_told_to_get_credits_for_a_user : InstanceContextSpecification<ICreditService>
    {
        private long userId;
        private ViewCreditDto result;
        private ViewCreditDto creditDto;
        private IRepository repository;
        private IMapper<User, ViewCreditDto> mapper;
        private Credit credit;
        private User user;

        protected override void Arrange()
        {
            userId = 875;
            credit = new Credit(5);
            creditDto = new ViewCreditDto();
            repository = Dependency<IRepository>();
            mapper = RegisterMapper<User, ViewCreditDto>();

            user = Fixtures.Users.JoeDeveloper;
            user.Credit = credit;
            When(repository).IsToldTo(r => r.Get<User>(userId)).Return(user);
            When(mapper).IsToldTo(m => m.Map(user)).Return(creditDto);
        }

        protected override ICreditService CreateSystemUnderTest()
        {
            return new CreditService(repository);
        }

        protected override void Act()
        {
            result = sut.GetCreditsForUser(userId.ToString());
        }

        [Test]
        public void should_get_the_user_from_the_repository()
        {
            repository.AssertWasCalled(r => r.Get<User>(userId));   
        }

        [Test]
        public void should_map_the_users_credit_to_a_dto()
        {
            mapper.AssertWasCalled(m => m.Map(user));
        }

        [Test]
        public void should_return_the_dto()
        {
            result.ShouldBeSameAs(creditDto);
        }
    }

    public class when_CreditService_is_told_to_add_credtis_to_a_user : InstanceContextSpecification<ICreditService>
    {
        private IRepository repository;
        private long userId;
        private AddCreditDto addCreditDto;
        private double amount;
        private User user;

        protected override void Arrange()
        {
            userId = 654;
            amount = 20.6;
            addCreditDto = new AddCreditDto{UserId = userId.ToString(), Amount = amount.ToString()};
            repository = Dependency<IRepository>();
            user = Fixtures.Users.JoeDeveloper;

            When(repository).IsToldTo(r => r.Get<User>(userId)).Return(user);
        }

        protected override ICreditService CreateSystemUnderTest()
        {
            return new CreditService(repository);
        }

        protected override void Act()
        {
            sut.AddCredit(addCreditDto);
        }

        [Test]
        public void should_get_the_user_from_the_repository()
        {
            repository.AssertWasCalled(r => r.Get<User>(userId));
        }

        [Test]
        public void should_add_the_amount_to_the_users_credit()
        {
            user.Credit.Amount.ShouldBeEqualTo(amount);
        }
    }

}