using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Mapping
{
    internal class SnackToDtoMapper : IMapper<Snack, SnackDto>
    {
        public SnackDto Map(Snack snack)
        {
            var snackDto = new SnackDto();

            snackDto.Id = snack.Id.ToString();
            snackDto.Name = string.Format("{0} (€ {1})", snack.Name, snack.Price);
            snackDto.Price = snack.Price;

            return snackDto;
        }
    }
}