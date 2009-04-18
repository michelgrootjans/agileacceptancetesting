using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Mapping
{
    public class UserToDtoMapper : IMapper<User, ViewUserDto>
    {
        public ViewUserDto Map(User user)
        {
            var dto = new ViewUserDto();

            dto.Id = user.Id.ToString();
            dto.Name = user.Name;
            dto.Email = user.Email;
            dto.Credit = user.Credit.Amount.ToString();
            
            return dto;
        }
    }
}