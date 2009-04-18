using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Mapping
{
    public class UserToCreditDtoMapper : IMapper<User, ViewCreditDto>
    {
        public ViewCreditDto Map(User user)
        {
            var dto = new ViewCreditDto();

            dto.UserId = user.Id.ToString();
            dto.UserName = user.Name;
            dto.Credit = user.Credit.Amount.ToString();

            return dto;
        }
    }
}