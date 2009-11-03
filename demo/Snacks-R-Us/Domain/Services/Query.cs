using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Services
{
    public static class Query
    {
        public static bool CurrentUser(User user)
        {
            return user.Name.Equals(Current.UserName);
        }
    }
}