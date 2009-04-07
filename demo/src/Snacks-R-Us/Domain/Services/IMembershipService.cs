using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Security;
using Snacks_R_Us.Domain.Entities;

namespace Snacks_R_Us.Domain.Services
{
    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string  userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        IPrincipal GetPrincipal(string userName);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly List<User> users;

        public AccountMembershipService()
        {
            users = new List<User>
                        {
                            new User("pascal", "ihc", "pascal@ihc.be", "Secretary"),
                            new User("michel", "ilean", "michel@ilean.be", "Developer")
                        };
        }

        public bool ValidateUser(string userName, string password)
        {
            return users.Exists(u => u.Name.Equals(userName) && u.Password.Equals(password));
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (users.Exists(u => u.Name.Equals(userName)))
                return MembershipCreateStatus.DuplicateUserName;
            if (users.Exists(u => u.Email.Equals(email)))
                return MembershipCreateStatus.DuplicateUserName;

            users.Add(new User(userName, password, email));
            return MembershipCreateStatus.Success;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = users.Find(u => u.Name.Equals(userName) && u.Password.Equals(oldPassword));
            if (user == null) return false;

            user.ResetPasswordTo(newPassword);
            return true;}

        public IPrincipal GetPrincipal(string userName)
        {
            return users.Find(u => u.Name.Equals(userName)) ?? new User("Unknown user", null, null, new string[0]);
        }
    }
}