using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Etensions;

namespace Snacks_R_Us.Domain.Mapping
{
    public class CreateSnackDtoMapper : IMapper<CreateSnackDto, Snack>
    {
        public Snack Map(CreateSnackDto createSnackDto)
        {
            return new Snack(createSnackDto.Name, createSnackDto.Price.ToDouble());
        }
    }
}