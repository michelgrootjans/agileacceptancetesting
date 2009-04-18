using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Utilities;
using Snacks_R_Us.WebApp.Controllers;

namespace Snacks_R_Us.UnitTests.Controllers
{
    public class when_CreditController_is_told_to_show_my_credits : InstanceContextSpecification<ICreditController>
    {
        private ActionResult result;
        private ViewCreditDto creditDto;
        private ICreditService service;

        protected override void Arrange()
        {
            creditDto = new ViewCreditDto();
            
            service = RegisterDependencyInContainer<ICreditService>();

            When(service).IsToldTo(s => s.GetCreditsForCurrentUser()).Return(creditDto);
        }

        protected override ICreditController CreateSystemUnderTest()
        {
            return new CreditController();
        }

        protected override void Act()
        {
            result = sut.MyCredit();
        }

        [Test]
        public void should_get_the_current_user_from_the_repository()
        {
            service.AssertWasCalled(r => r.GetCreditsForCurrentUser());
        }

        [Test]
        public void should_show_the_view()
        {
            result.ShouldShowView();
        }

        [Test]
        public void should_put_the_dto_into_the_view()
        {
            sut.ViewData.Model.ShouldBeEqualTo(creditDto);                
        }

    }

    public class when_CreditController_is_told_to_edit_credits : InstanceContextSpecification<ICreditController>
    {
        private ICreditService service;
        private string userId;
        private ViewCreditDto creditDto;
        private ActionResult result;

        protected override void Arrange()
        {
            userId = "876";
            creditDto = new ViewCreditDto();
            service = RegisterDependencyInContainer<ICreditService>();
            When(service).IsToldTo(s => s.GetCreditsForUser(userId)).Return(creditDto);
        }

        protected override ICreditController CreateSystemUnderTest()
        {
            return new CreditController();
        }

        protected override void Act()
        {
            result = sut.Edit(userId);
        }

        [Test]
        public void should_get_the_credits_for_the_selected_user()
        {
            service.AssertWasCalled(s => s.GetCreditsForUser(userId));
        }

        [Test]
        public void should_put_the_result_in_the_view()
        {
            sut.ViewData.Model.ShouldBeEqualTo(creditDto);
        }

        [Test]
        public void should_show_view()
        {
            result.ShouldShowView();
        }
    }

    public class when_CreditController_is_told_to_add_credits : InstanceContextSpecification<ICreditController>
    {
        private ICreditService service;
        private AddCreditDto addCreditDto;
        private ActionResult result;

        protected override void Arrange()
        {
            addCreditDto = new AddCreditDto();
            service = RegisterDependencyInContainer<ICreditService>();
        }

        protected override ICreditController CreateSystemUnderTest()
        {
            return new CreditController();
        }

        protected override void Act()
        {
            result = sut.Update(addCreditDto);
        }

        [Test]
        public void should_tell_the_service_to_add_credits_for_the_user()
        {
            service.AssertWasCalled(s => s.AddCredit(addCreditDto));
        }

        [Test]
        public void should_redirect_to_user_list()
        {
            result.ShouldRedirectToController("User")
                .ShouldRedirectToAction("List");

        }
    }

}