using System.Security.Principal;
using System.Web.Security;
using Snacks_R_Us.Domain.Entities;
using Snacks_R_Us.Domain.Extensions;
using Snacks_R_Us.Domain.Repositories;

namespace Snacks_R_Us.Domain.Services
{
    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email, params string[] roles);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        IPrincipal GetPrincipal(string userName);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly IRepository repository;

        public AccountMembershipService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool ValidateUser(string userName, string password)
        {
            var user = repository.Find<User>(u => u.Name.ToLower().Equals(userName.ToLower()) && u.Password.Equals(password));
            return user.IsNotNull();
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email, params string[] roles)
        {
            if (repository.Find<User>(u => u.Name.Equals(userName)).IsNotNull())
                return MembershipCreateStatus.DuplicateUserName;
            if (repository.Find<User>(u => u.Email.Equals(email)).IsNotNull())
                return MembershipCreateStatus.DuplicateEmail;

            repository.Save(new User(userName, password, email, roles));
            return MembershipCreateStatus.Success;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = repository.Find<User>(u => u.Name.Equals(userName) && u.Password.Equals(oldPassword));
            if (user == null) return false;

            user.ResetPasswordTo(newPassword);
            return true;
        }

        public IPrincipal GetPrincipal(string userName)
        {
            var users = repository.FindAll<User>();
            return users.Find(u => u.Name.Equals(userName)) ?? new User("Unknown user", null, null, new string[0]);
        }
    }
}