using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.UnitTests.Fixtures
{
    public static class Users
    {
        public static User JoeDeveloper
        {
            get { return new User("Joe", "h6gJKG", "joe@email.com", "Developer"); }
        }
    }
}