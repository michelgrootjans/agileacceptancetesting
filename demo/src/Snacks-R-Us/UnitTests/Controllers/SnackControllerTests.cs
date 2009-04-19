using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.Domain.Repositories;
using Snacks_R_Us.Domain.Services;
using Snacks_R_Us.UnitTests.Utilities;
using Snacks_R_Us.WebApp.Controllers;
using Rhino.Mocks;

namespace Snacks_R_Us.UnitTests.Controllers
{
    public class when_SnackController_is_told_to_show_all_snacks : InstanceContextSpecification<ISnackController>
    {
        private ISnackService snackService;
        private ActionResult result;
        private IEnumerable<SnackDto> snacks;

        protected override void Arrange()
        {
            snacks = new List<SnackDto>();
            snackService = RegisterDependencyInContainer<ISnackService>();
            When(snackService).IsToldTo(s => s.GetAllSnacks()).Return(snacks);
        }

        protected override ISnackController CreateSystemUnderTest()
        {
            return new SnackController();
        }

        protected override void Act()
        {
            result = sut.Index();
        }

        [Test]
        public void should_tell_the_service_to_get_all_snacks()
        {
            snackService.AssertWasCalled(s => s.GetAllSnacks());
        }

        [Test]
        public void should_show_the_index()
        {
            result.ShouldShowView();
        }

        [Test]
        public void should_put_the_snacks_into_the_view()
        {
            sut.ViewData.Model.ShouldBeSameAs(snacks);
        }
    }

    public class when_SnackService_is_told_to_create_a_snack : InstanceContextSpecification<ISnackService>
    {
        private CreateSnackDto createSnackDto;
        private IRepository repository;
        private IMapper<CreateSnackDto, Snack> mapper;
        private Snack snack;

        protected override void Arrange()
        {
            createSnackDto = new CreateSnackDto();
            snack = Fixtures.Snacks.Pizza;

            repository = Dependency<IRepository>();
            mapper = RegisterDependencyInContainer<IMapper<CreateSnackDto, Snack>>();

            When(mapper).IsToldTo(m => m.Map(createSnackDto)).Return(snack);
        }

        protected override ISnackService CreateSystemUnderTest()
        {
            return new SnackService(repository);
        }

        protected override void Act()
        {
            sut.CreateSnack(createSnackDto);
        }

        [Test]
        public void should_map_the_dto_to_a_snack()
        {
            mapper.AssertWasCalled(m => m.Map(createSnackDto));
        }

        [Test]
        public void should_save_a_new_snack_to_the_repository()
        {
            repository.AssertWasCalled(r => r.Save(snack));
        }

    }

}