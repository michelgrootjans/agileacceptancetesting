using NUnit.Framework;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Mapping
{
    public class when_SnackToDtoMapper_is_told_to_map_a_snack : InstanceContextSpecification<IMapper<Snack, SnackDto>>
    {
        private Snack snack;
        private SnackDto snackDto;

        protected override void Arrange()
        {
            snack = new Snack("Pizza", 2.9);
        }

        protected override IMapper<Snack, SnackDto> CreateSystemUnderTest()
        {
            return new SnackToDtoMapper();
        }

        protected override void Act()
        {
            snackDto = sut.Map(snack);
        }

        [Test]
        public void should_map_its_ID()
        {
            snackDto.Id.ShouldBeEqualTo(snack.Id.ToString());
        }

        [Test]
        public void should_map_its_name()
        {
            snackDto.Name.ShouldBeEqualTo(snack.Name);
        }

        [Test]
        public void should_map_its_screenname()
        {
            snackDto.ScreenName.ShouldBeEqualTo(string.Format("{0} (€ {1})", snack.Name, snack.Price));
        }

        [Test]
        public void should_map_its_price()
        {
            snackDto.Price.ShouldBeEqualTo(snack.Price);
        }
    }

}