using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Utilities;
using Snacks_R_Us.WebApp.Controllers;
using Rhino.Mocks;

namespace Snacks_R_Us.UnitTests.Controllers
{
    public class when_UserController_is_told_to_list_all_users : InstanceContextSpecification<IUserController>
    {
        private IUserService service;
        private ActionResult result;
        private IEnumerable<ViewUserDto> users;

        protected override void Arrange()
        {
            users = new List<ViewUserDto>();
            service = RegisterDependencyInContainer<IUserService>();
            When(service).IsToldTo(s => s.GetAllUsers()).Return(users);
        }

        protected override IUserController CreateSystemUnderTest()
        {
            return new UserController();
        }

        protected override void Act()
        {
            result = sut.List();
        }

        [Test]
        public void should_tell_the_service_to_get_all_users()
        {
            service.AssertWasCalled(s => s.GetAllUsers());
        }

        [Test]
        public void should_put_the_result_into_the_view()
        {
            sut.ViewData.Model.ShouldBeEqualTo(users);
        }

        [Test]
        public void should_show_the_view()
        {
            result.ShouldShowView();
        }
    }


}