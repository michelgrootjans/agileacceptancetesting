using System.Collections.Generic;
using System.Security.Principal;

namespace Snacks_R_Us.Domain.Entities
{
    public class User : IPrincipal
    {
        private readonly List<string> roles;

        public User(string name, string password, string email, params string[] roles)
        {
            Name = name;
            Password = password;
            Email = email;
            this.roles = new List<string>(roles);
           
        }

        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        internal void ResetPasswordTo(string newPassword)
        {
            Password = newPassword;
        }

        public bool IsInRole(string role)
        {
            return roles.Exists(r => r.Equals(role));
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Name); }
        }
    }
}