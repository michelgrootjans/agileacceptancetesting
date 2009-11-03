using NUnit.Framework;
using Snacks_R_Us.Domain;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Mapping;
using Snacks_R_Us.UnitTests.Utilities;

namespace Snacks_R_Us.UnitTests.Mapping
{
    public class when_CreateSnackDtoMapper_is_told_to_map_a_dto : InstanceContextSpecification<IMapper<CreateSnackDto, Snack>>
    {
        private Snack result;
        private CreateSnackDto dto;
        private static string snackName;
        private static double snackPrice;

        protected override void Arrange()
        {
            ApplicationStartup.InitializeMappers();

            snackName = "Pizza";
            snackPrice = 3.2;
            dto = new CreateSnackDto {Name = snackName, Price = snackPrice.ToString()};
        }

        protected override IMapper<CreateSnackDto, Snack> CreateSystemUnderTest()
        {
            return new GenericAutoMapper<CreateSnackDto, Snack>();
        }

        protected override void Act()
        {
            result = sut.Map(dto);
        }

        [Test]
        public void should_map_the_name()
        {
            result.Name.ShouldBeEqualTo(snackName);
        }

        [Test]
        public void should_map_the_price()
        {
            result.Price.ShouldBeEqualTo(snackPrice);
        }
    }

}