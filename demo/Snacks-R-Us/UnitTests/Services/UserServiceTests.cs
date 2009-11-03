using System.Collections.Generic;
using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Utilities;
using Rhino.Mocks;
using System.Linq;

namespace Snacks_R_Us.UnitTests.Services
{
    public class when_UserService_is_told_to_get_all_users : InstanceContextSpecification<IUserService>
    {
        private IRepository repository;
        private IMapper<User, ViewUserDto> mapper;
        private IEnumerable<ViewUserDto> result;
        private User user1;
        private User user2;
        private ViewUserDto user1Dto;
        private ViewUserDto user2Dto;

        protected override void Arrange()
        {
            user1 = Fixtures.Users.JoeDeveloper;
            user2 = Fixtures.Users.JoeDeveloper;
            user1Dto = new ViewUserDto();
            user2Dto = new ViewUserDto();
            
            repository = Dependency<IRepository>();
            mapper = RegisterMapper<User, ViewUserDto>();

            When(repository).IsToldTo(r => r.FindAll<User>()).Return(new List<User> {user1, user2});
            When(mapper).IsToldTo(m => m.Map(user1)).Return(user1Dto);
            When(mapper).IsToldTo(m => m.Map(user2)).Return(user2Dto);
        }

        protected override IUserService CreateSystemUnderTest()
        {
            return new UserService(repository);
        }

        protected override void Act()
        {
            result = sut.GetAllUsers().ToList();
        }

        [Test]
        public void should_tell_the_repository_to_get_all_users()
        {
            repository.AssertWasCalled(r => r.FindAll<User>());
        }

        [Test]
        public void should_map_all_the_users_to_dtos()
        {
            mapper.AssertWasCalled(m => m.Map(user1));
            mapper.AssertWasCalled(m => m.Map(user2));
        }

        [Test]
        public void should_return_both_mapped_users()
        {
            result.ShouldContain(user1Dto);
            result.ShouldContain(user2Dto);
        }
    }

}