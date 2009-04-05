using System.Collections.Generic;
using System.Web.Security;

namespace Snacks_R_Us.Domain.Services
{
    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string  userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly List<User> users;

        public AccountMembershipService()
        {
            users = new List<User>
                        {
                            new User("pascal", "ihc", "pascal@ihc.be"),
                            new User("michel", "ilean", "michel@ilean.be")
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
    }

    public class User
    {
        private readonly string name;
        private string password;
        private readonly string email;

        public User(string name, string password, string email)
        {
            this.name = name;
            this.password = password;
            this.email = email;
        }

        public string Name 
        {
            get {return name;}
        }

        public string Password
        {
            get {return password;}
        }

        public string Email
        {
            get {return email;}
        }

        public void ResetPasswordTo(string newPassword)
        {
            password = newPassword;
        }
    }
}